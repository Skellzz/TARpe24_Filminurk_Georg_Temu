using Filminurk.Core.Domain;
//Siin on MoviesCreateUpdateViewModel
namespace Filminurk.Models.Movies
{
    public class MoviesCreateUpdateViewModel
    {
        public Guid ID { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateOnly FirstPublished { get; set; }
        public string Director { get; set; }
        public List<string>? Actors { get; set; }
        public double? CurrentRating { get; set; }

        public List<IFormFile> Files { get; set; }
        public List<ImageViewModel> Images { get; set; } = new List<ImageViewModel>();

        public Genre? Genre { get; set; }
        public string? Tagline { get; set; }
        public string? Warnings { get; set; }
        /* Andmebaasi jaoks vajalikud */
        public DateTime? EntryCreatedAt { get; set; }
        public DateTime? EntryModifiedAt { get; set; }

    }
}
