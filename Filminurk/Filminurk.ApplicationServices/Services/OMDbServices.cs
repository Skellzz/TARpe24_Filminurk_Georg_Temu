using Filminurk.Core.Domain;              
using Filminurk.Core.Dto;                 
using Filminurk.Core.Dto.OMDbApiDTO;
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

          
            if (dto == null) return null;
            if (!string.Equals(dto.Response, "True", StringComparison.OrdinalIgnoreCase)) return dto;

            return dto;
        }

        public async Task<Guid?> ImportByTitleAsync(string title, CancellationToken ct = default)
        {
            var omdb = await FetchByTitleAsync(title, ct);
            if (omdb == null) return null;

            if (!string.Equals(omdb.Response, "True", StringComparison.OrdinalIgnoreCase))
                return null; 

            var movieDto = MapToMoviesDto(omdb);

           
            var created = await _movieServices.Create(movieDto);

            return created?.ID;
        }

        private static MoviesDTO MapToMoviesDto(OmdbApiMovieResultDTO omdb)
        {
            DateOnly firstPublished = DateOnly.MinValue;
            if (!string.IsNullOrWhiteSpace(omdb.Released) && !string.Equals(omdb.Released, "N/A", StringComparison.OrdinalIgnoreCase))
            {
                if (DateOnly.TryParse(omdb.Released, new CultureInfo("en-US"), DateTimeStyles.None, out var parsed))
                    firstPublished = parsed;
            }

           
            double rating = 0;
            if (!string.IsNullOrWhiteSpace(omdb.ImdbRating) && !string.Equals(omdb.ImdbRating, "N/A", StringComparison.OrdinalIgnoreCase))
            {
                _ = double.TryParse(omdb.ImdbRating, NumberStyles.Any, CultureInfo.InvariantCulture, out rating);
            }

          
            var genre = Genre.Other;
            var firstGenre = (omdb.Genre ?? "").Split(',', StringSplitOptions.RemoveEmptyEntries).FirstOrDefault()?.Trim();
            if (!string.IsNullOrWhiteSpace(firstGenre) && Enum.TryParse<Genre>(firstGenre, ignoreCase: true, out var parsedGenre))
                genre = parsedGenre;

          
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
                Tagline = "",               
                Warnings = "",              
                EntryCreatedAt = DateTime.Now,
                EntryModifiedAt = DateTime.Now
            };

            return dto;
        }

        Task<OmdbApiMovieResultDTO> IOMDbApiServices.OMDbApiResult(OmdbApiMovieResultDTO dto)
        {
            throw new NotImplementedException();
        }

        Movie IOMDbApiServices.Create(OMDbApiMovieCreateDTO dto)
        {
            throw new NotImplementedException();
        }
    }
}
