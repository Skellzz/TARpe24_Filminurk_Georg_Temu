using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Filminurk.Core.Domain;
using Filminurk.Core.Dto;
// siin on IActorsServices
namespace Filminurk.Core.ServiceInterface
{
    public interface IActorsServices
    {
        Task<Actors> Create(ActorsDTO dto);
        Task<Actors> Update(ActorsDTO dto);
        Task<Actors> Delete(Guid id);
        Task<Actors> DetailsAsync(Guid id);
    }
}
