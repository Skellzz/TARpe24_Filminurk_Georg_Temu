using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Text.Json.Serialization;

namespace Filminurk.Core.Dto.OmdbapiDTOs
{
    public class OmdbApiMovieResultDTO
    {
        [JsonPropertyName("Response")]
        public string? Response { get; set; } 
        [JsonPropertyName("Error")]
        public string? Error { get; set; }

        [JsonPropertyName("Title")]
        public string? Title { get; set; }

        [JsonPropertyName("Released")]
        public string? Released { get; set; } 

        [JsonPropertyName("Genre")]
        public string? Genre { get; set; }

        [JsonPropertyName("Director")]
        public string? Director { get; set; }

        [JsonPropertyName("Actors")]
        public string? Actors { get; set; }

        [JsonPropertyName("Plot")]
        public string? Plot { get; set; }

        [JsonPropertyName("imdbRating")]
        public string? ImdbRating { get; set; }
    }
}
