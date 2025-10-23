using Filminurk.Core.Domain;

namespace Filminurk.Models.Movies
{
    public class MoviesIndexViewModel
    {
        public Guid ID { get; set; }
        public string Title { get; set; }
        public DateOnly FirstPublished { get; set; }
        public double? CurrentRating { get; set; }
        public string? CountryOfOrigin { get; set; }
        public MovieGenre? MovieGenre { get; set; }
    }
}
