using Filminurk.Core.Domain;
using Filminurk.Core.Dto.OMDbApiDTO;
using Filminurk.Core.Dto.OmdbapiDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Filminurk.Core.ServiceInterface
{
    public interface IOMDbApiServices
    {
        Task<OmdbApiMovieResultDTO> OMDbApiResult(OmdbApiMovieResultDTO dto);
        Movie Create(OMDbApiMovieCreateDTO dto);
        Task<OmdbApiMovieResultDTO?> FetchByTitleAsync(string title, CancellationToken ct = default);
        Task<Guid?> ImportByTitleAsync(string title, CancellationToken ct = default);
    }
}
