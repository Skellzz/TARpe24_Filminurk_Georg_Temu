using Filminurk.Core.Domain;

namespace Filminurk.Models.OMDbApi
{
    public class OMDbApiViewModel
    {
        public string Title { get; set; }
        public string FirstPublished { get; set; }
        public Genre Genre { get; set; }
        public string Producer { get; set; }
        public string Actors { get; set; }
        public string Description { get; set; }
        public string imdbRating { get; set; }

    }
}
