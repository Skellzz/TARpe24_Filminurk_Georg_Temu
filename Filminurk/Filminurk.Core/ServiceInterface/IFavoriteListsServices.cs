using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Filminurk.Core.Domain;
using Filminurk.Core.Dto;

namespace Filminurk.Core.ServiceInterface
{
    public interface IFavoriteListsServices
    {
        Task<FavoriteList> DetailsAsync(Guid id);
        Task<FavoriteList> Create(FavoriteListDTO dto);
        Task<FavoriteList> Update(FavoriteListDTO updatedList, string typeOfMethod);
    }
}
