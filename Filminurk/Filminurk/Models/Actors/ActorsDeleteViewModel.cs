using Filminurk.Core.Domain;

namespace Filminurk.Models.Actors
{
    public class ActorsDeleteViewModel
    {
        public Guid ID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string NickName { get; set; }
        public List<string> MoviesActedFor { get; set; }
        public string? PortraitID { get; set; }

        /* Kolm minu mõeldud asju */
        public int? ActorRating { get; set; }
        public Gender? Gender { get; set; }
        public Genre? FavoriteGenre { get; set; }
        public DateTime? EntryCreatedAt { get; set; }
        public DateTime? EntryModifiedAt { get; set; }
    }
}
