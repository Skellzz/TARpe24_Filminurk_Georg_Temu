using Filminurk.Core.Domain;

namespace Filminurk.Models.Movies
{
    public class MoviesCreateUpdateViewModel
    {
        public Guid? ID { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateOnly FirstPublished { get; set; }

        public string Director { get; set; }
        public List<string>? Actors { get; set; }
        public double? CurrentRating { get; set; }

        /* Kaasasolevate piltide andmeomadused */
       
        public List<string>? ImageUrls { get; set; }
        public List<IFormFile> Files { get; set; }
        public List<ImageViewModel> Images { get; set; } = new List<ImageViewModel>();

        public string? CountryOfOrigin { get; set; }
        public MovieGenre? MovieGenre { get; set; }
        public MovieGenre? SubGenre { get; set; }

        public DateTime? EntryCreatedAt { get; set; }
        public DateTime? EntryModifiedAt { get; set; }
    }
}
