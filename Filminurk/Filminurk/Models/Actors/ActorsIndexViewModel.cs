using Filminurk.Core.Domain;
//Siin on ActorsIndexViewModel
namespace Filminurk.Models.Actors
{
    public class ActorsIndexViewModel
    {
        public Guid ID { get; set; }
        public string FirstName { get; set; }
        public string? LastName { get; set; }
        public string? NickName { get; set; }

 
        public int? ActorRating { get; set; }
        public Gender? Gender { get; set; }
        public Genre? MostActedGenre { get; set; }
    }
}
