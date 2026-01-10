using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Filminurk.Core.Domain;
using Microsoft.AspNetCore.Http;
// siin on ActorsDTO
namespace Filminurk.Core.Dto
{
    public class ActorsDTO
    {
        public Guid? ID { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? NickName { get; set; }
        public List<string>? MoviesActedFor { get; set; }
        public Guid? PortraitID { get; set; }

        public List<IFormFile>? Files { get; set; }
        public IEnumerable<FileToApiDTO>? Images { get; set; } = new List<FileToApiDTO>();

        
        public int? ActorRating { get; set; }
        public Gender? Gender { get; set; }
        public Genre? MostActedGenre { get; set; }
        public DateTime? EntryCreatedAt { get; set; }
        public DateTime? EntryModifiedAt { get; set; }
    }
}
