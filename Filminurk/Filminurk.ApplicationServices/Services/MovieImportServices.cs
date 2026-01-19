using System;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using Filminurk.Core.Dto;
using Filminurk.Core.ServiceInterface;
using Microsoft.Extensions.Configuration;

namespace Filminurk.ApplicationServices.Services
{
    public class MovieImportService : IMovieImportService
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;
        private readonly IMovieServices _movieServices;

        public MovieImportService(
            HttpClient httpClient,
            IConfiguration configuration,
            IMovieServices movieServices)
        {
            _httpClient = httpClient;
            _configuration = configuration;
            _movieServices = movieServices;
        }

        public async Task<MoviesDTO> ImportMovieByTitleAsync(string title)
        {
            try
            {
                var apiKey = _configuration["OMDbApi:ApiKey"];
                var url = $"http://www.omdbapi.com/?t={Uri.EscapeDataString(title)}&apikey={apiKey}";

                var response = await _httpClient.GetAsync(url);
                response.EnsureSuccessStatusCode();

                var jsonResponse = await response.Content.ReadAsStringAsync();
                var omdbMovie = JsonSerializer.Deserialize<OMDbMovieDto>(jsonResponse);

                if (omdbMovie == null || omdbMovie.Response != "True")
                {
                    throw new Exception($"Film '{title}' ei leitud OMDb-s");
                }

                // Teisenda OMDb andmed teie MovieDTO-ks
                var movieDto = ConvertToMovieDTO(omdbMovie);

                // Salvesta andmebaasi (kui IMovieServices on olemas)
                return await _moviesServices.Create(movieDto);
            }
            catch (Exception ex)
            {
                // Logi viga
                throw new Exception($"Filmi importimine ebaõnnestus: {ex.Message}", ex);
            }
        }

        private MoviesDTO ConvertToMovieDTO(OMDbMovieDto omdbMovie)
        {
            // Parse release year
            int? releaseYear = null;
            if (int.TryParse(omdbMovie.Year, out int year))
            {
                releaseYear = year;
            }

            // Parse rating
            decimal? rating = null;
            if (decimal.TryParse(omdbMovie.ImdbRating, out decimal r))
            {
                rating = r;
            }

            // Split actors string into list
            var actorsList = omdbMovie.Actors?
                .Split(',', StringSplitOptions.RemoveEmptyEntries)
                .Select(a => a.Trim())
                .ToList();

            return new MoviesDTO
            {
                Title = omdbMovie.Title,
                Description = omdbMovie.Plot,
                FirstPublished = omdbMovie,
                Director = omdbMovie.Director,
                Actors = actorsList,
                Genre = omdbMovie.Genre,
                CurrentRating = omdbMovie.Rating,
                PosterUrl = omdbMovie.Poster,
                ImdbId = omdbMovie.ImdbId
            };
        }
    }
}