using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Filminurk.Core.Dto.OMDbApiDTO
{
    public class OMDbApiMovieResultDTO
    {
        public string Title { get; set; }
        public string FirstPublished { get; set; } = string.Empty;
        public string Genre { get; set; }
        public string Director { get; set; }
        public string Actors { get; set; }
        public string Description { get; set; }
        public string imdbRating { get; set; }
    }
}
