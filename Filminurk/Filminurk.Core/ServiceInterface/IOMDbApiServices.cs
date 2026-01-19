using Filminurk.Core.Domain;
using Filminurk.Core.Dto.OMDbApiDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Filminurk.Core.ServiceInterface
{
    public interface IOMDbApiServices
    {
        Task<OMDbApiMovieResultDTO> OMDbApiResult(OMDbApiMovieResultDTO dto);
        Movie Create(OMDbApiMovieCreateDTO dto);
    }
}
