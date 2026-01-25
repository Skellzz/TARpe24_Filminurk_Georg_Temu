using Filminurk.Core.Domain;               // Genre enum (sinu projektis olemas)
using Filminurk.Core.Dto;                 // MoviesDTO (sinu projektis olemas)
using Filminurk.Core.Dto.OmdbapiDTOs;
using Filminurk.Core.ServiceInterface;
using Microsoft.Extensions.Configuration;
using System.Globalization;
using System.Text.Json;
using System.Web;

namespace Filminurk.ApplicationServices.Services
{
    public class OMDbApiServices : IOMDbApiServices
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _config;
        private readonly IMovieServices _movieServices;

        public OMDbApiServices(HttpClient httpClient, IConfiguration config, IMovieServices movieServices)
        {
            _httpClient = httpClient;
            _config = config;
            _movieServices = movieServices;
        }

        public async Task<OmdbApiMovieResultDTO?> FetchByTitleAsync(string title, CancellationToken ct = default)
        {
            if (string.IsNullOrWhiteSpace(title)) return null;

            var baseUrl = _config["OmDb:BaseUrl"]?.Trim();
            var apiKey = _config["OmDb:ApiKey"]?.Trim();

            if (string.IsNullOrWhiteSpace(baseUrl) || string.IsNullOrWhiteSpace(apiKey))
                throw new InvalidOperationException("OMDb seaded puuduvad. Kontrolli appsettings.json (OmDb:BaseUrl, OmDb:ApiKey).");

            var encodedTitle = HttpUtility.UrlEncode(title.Trim());
            var url = $"{baseUrl}?apikey={apiKey}&t={encodedTitle}&plot=full";

            using var resp = await _httpClient.GetAsync(url, ct);
            if (!resp.IsSuccessStatusCode) return null;

            var json = await resp.Content.ReadAsStringAsync(ct);

            var dto = JsonSerializer.Deserialize<OmdbApiMovieResultDTO>(
                json,
                new JsonSerializerOptions { PropertyNameCaseInsensitive = true }
            );

            // OMDb annab Response="False" kui ei leitud
            if (dto == null) return null;
            if (!string.Equals(dto.Response, "True", StringComparison.OrdinalIgnoreCase)) return dto;

            return dto;
        }

        public async Task<Guid?> ImportByTitleAsync(string title, CancellationToken ct = default)
        {
            var omdb = await FetchByTitleAsync(title, ct);
            if (omdb == null) return null;

            if (!string.Equals(omdb.Response, "True", StringComparison.OrdinalIgnoreCase))
                return null; // ei leitud vms (omdb.Error sees)

            // Map OMDb -> meie MoviesDTO
            var movieDto = MapToMoviesDto(omdb);

            // Create läbi olemasoleva IMovieServices (sama nagu MoviesController teeb)
            var created = await _movieServices.Create(movieDto);

            return created?.ID;
        }

        private static MoviesDTO MapToMoviesDto(OmdbApiMovieResultDTO omdb)
        {
            // Released -> DateOnly
            DateOnly firstPublished = DateOnly.MinValue;
            if (!string.IsNullOrWhiteSpace(omdb.Released) && !string.Equals(omdb.Released, "N/A", StringComparison.OrdinalIgnoreCase))
            {
                // OMDb kuupäev on tavaliselt "31 Mar 1999"
                if (DateOnly.TryParse(omdb.Released, new CultureInfo("en-US"), DateTimeStyles.None, out var parsed))
                    firstPublished = parsed;
            }

            // imdbRating -> double
            double rating = 0;
            if (!string.IsNullOrWhiteSpace(omdb.ImdbRating) && !string.Equals(omdb.ImdbRating, "N/A", StringComparison.OrdinalIgnoreCase))
            {
                _ = double.TryParse(omdb.ImdbRating, NumberStyles.Any, CultureInfo.InvariantCulture, out rating);
            }

            // Genre string -> meie Genre enum (võtame esimese žanri)
            var genre = Genre.Other;
            var firstGenre = (omdb.Genre ?? "").Split(',', StringSplitOptions.RemoveEmptyEntries).FirstOrDefault()?.Trim();
            if (!string.IsNullOrWhiteSpace(firstGenre) && Enum.TryParse<Genre>(firstGenre, ignoreCase: true, out var parsedGenre))
                genre = parsedGenre;

            // Actors -> List<string>
            var actors = (omdb.Actors ?? "")
                .Split(',', StringSplitOptions.RemoveEmptyEntries)
                .Select(a => a.Trim())
                .Where(a => a.Length > 0)
                .ToList();

            var dto = new MoviesDTO
            {
                ID = Guid.NewGuid(),
                Title = omdb.Title ?? "(Unknown title)",
                Description = omdb.Plot ?? "",
                FirstPublished = firstPublished,
                Director = omdb.Director ?? "",
                Actors = actors,
                CurrentRating = rating,
                Genre = genre,
                Tagline = "",               // OMDb taglinet ei anna; võid jätta tühjaks
                Warnings = "",              // kui sul vajalik, jäta tühjaks
                EntryCreatedAt = DateTime.Now,
                EntryModifiedAt = DateTime.Now
            };

            return dto;
        }
    }
}
