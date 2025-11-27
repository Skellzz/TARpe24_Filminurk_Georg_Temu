using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Filminurk.Core.Domain;
using Filminurk.Core.Dto;

namespace Filminurk.Core.ServiceInterface
{
    public interface IUserCommentsServices
    {
        Task<UserComment> NewComment(UserCommentDTO newcommentDTO);
        Task<UserComment> DetailAsync(Guid id);
        Task<UserComment> Delete(Guid id);
    }
}
