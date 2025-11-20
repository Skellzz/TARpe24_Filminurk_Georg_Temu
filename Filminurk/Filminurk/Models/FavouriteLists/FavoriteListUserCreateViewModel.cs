using Filminurk.Core.Domain;
using Filminurk.Core.Dto;
using System.ComponentModel.DataAnnotations;

namespace Filminurk.Models.FavouriteLists
{
    public class FavoriteListUserCreateViewModel
    {
        [Key]
        public Guid FavouriteListID { get; set; }
        public string ListBelongsToUser { get; set; }
        public bool IsMovieOrActor { get; set; }
        public string ListName { get; set; }
        public string? ListDescription { get; set; }
        public bool IsPrviate { get; set; }
        public List<Movie>? ListOfMovies { get; set; }
        // public List<Actor>? ListOfActors { get; set; }

        /* andmebaasiomadused */
        public List<IFormFile> Files { get; set; }
        public IEnumerable<FileToDatabaseDTO> Images { get; set; } = new List<FileToDatabaseDTO>();

        public DateTime ListCreateAt { get; set; }
        public DateTime ListModifiedAt { get; set; }
        public DateTime ListDeletedAt { get; set; }
        public bool IsReported { get; set; } = false;
    }
}
