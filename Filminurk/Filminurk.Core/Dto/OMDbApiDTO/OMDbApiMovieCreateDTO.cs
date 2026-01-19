using Filminurk.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Filminurk.Core.Dto.OMDbApiDTO
{
    public class OMDbApiMovieCreateDTO
    {
        public Guid? ID { get; set; }
        public string Title { get; set; }
        public DateOnly? FirstPublished { get; set; }
        public Genre Genre { get; set; }
        public string Producer { get; set; }
        public List<string>? Actors { get; set; }
        public string? Description { get; set; }
        public double? CurrentRating { get; set; }
        public DateTime? EntryCreatedAt { get; set; }
        public DateTime? EntryModifiedAt { get; set; }
    }
}
