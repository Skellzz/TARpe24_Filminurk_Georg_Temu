using Filminurk.Core.Domain;
using Filminurk.Core.Dto;

namespace Filminurk.Models.Actors
{
    public class ActorsCreateUpdateViewModel
    {
        public Guid ID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string NickName { get; set; }
        public List<string> MoviesActedFor { get; set; }
        public string? PortraitID { get; set; }
        public List<IFormFile>? Files { get; set; }
        public IEnumerable<FileToApiDTO>? Images { get; set; } = new List<FileToApiDTO>();

     
        public int? ActorRating { get; set; }
        public Gender? Gender { get; set; }
        public Genre? MostActedGenre { get; set; }
        public DateTime? EntryCreatedAt { get; set; }
        public DateTime? EntryModifiedAt { get; set; }
    }
}
