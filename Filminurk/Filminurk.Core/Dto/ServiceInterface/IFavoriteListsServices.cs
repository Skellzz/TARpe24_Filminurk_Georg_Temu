using Filminurk.Core.Domain;
using Filminurk.Core.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Filminurk.Core.ServiceInterface
{
    public interface IFavoriteListsServices
    {
        Task<FavouriteList> DetailsAsync(Guid id);
        Task<FavouriteList> Create(FavouriteListDTO dto /*List<Movie> selectedMovies*/);
    }
}
