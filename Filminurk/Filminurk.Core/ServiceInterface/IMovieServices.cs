using Filminurk.Core.Domain;
using Filminurk.Core.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Filminurk.Core.ServiceInterface
{
    public interface IMovieServices // see on interface. asub .core/serviceinterface
    {
        Task<Movie> Create(MoviesDTO dto);
        Task<Movie> Delete(Guid id);
        Task<Movie> DetailsAsync(Guid id);
        Task<Movie> Update(MoviesDTO dto);
    }
}
