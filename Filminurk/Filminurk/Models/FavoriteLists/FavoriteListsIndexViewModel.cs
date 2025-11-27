using Filminurk.Core.Domain;
using Filminurk.Models.Movies;
using System.ComponentModel.DataAnnotations;

namespace Filminurk.Models.FavoriteLists
{
    public class FavoriteListsIndexViewModel
    {
        [Key]
        public Guid? FavoriteListID { get; set; }
        public string ListBelongsToUser { get; set; }
        public bool IsMovieOrActor { get; set; }
        public string ListName { get; set; }
        public string? Description { get; set; }
        public bool? IsPrivate { get; set; }
        public List<Movie>? ListOfMovies { get; set; }
        //public List<Actors>? ListOfActors { get; set; }
        public DateTime? ListCreatedAt { get; set; }
        public DateTime? ListModifiedAt { get; set; }
        public DateTime? ListDeletedAt { get; set; }
        public bool? IsReported { get; set; } = false;
        // image model for index
        public List<FavoriteListsIndexImageViewModel> Image { get; set; } = new List<FavoriteListsIndexImageViewModel>();
    }
}
