using Filminurk.Core.Domain;
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
    }
}
