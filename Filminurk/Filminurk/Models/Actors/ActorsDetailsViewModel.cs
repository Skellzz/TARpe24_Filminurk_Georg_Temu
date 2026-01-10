using Filminurk.Core.Domain;
//Siin on ActorsDetailsViewModel
namespace Filminurk.Models.Actors
{
    public class ActorsDetailsViewModel
    {
        public Guid ID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string NickName { get; set; }
        public List<string> MoviesActedFor { get; set; }
        public string? PortraitID { get; set; }

       
        public int? ActorRating { get; set; }
        public Gender? Gender { get; set; }
        public Genre? MostActedGenre { get; set; }
        public DateTime? EntryCreatedAt { get; set; }
        public DateTime? EntryModifiedAt { get; set; }
    }
}
