using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Filminurk.Core.Domain;
using Filminurk.Core.Dto;
using Filminurk.Core.ServiceInterface;
using Filminurk.Data;
using Microsoft.EntityFrameworkCore;

namespace Filminurk.ApplicationServices.Services
{
    public class UserCommentsServices : IUserCommentsServices
    {
        private readonly FilminurkTARpe24Context _context;

        public UserCommentsServices(FilminurkTARpe24Context context)
        {
            _context = context;
        }
        public async Task<UserComment> NewComment(UserCommentDTO newcommentDTO)
        {
            UserComment domain = new UserComment();

            domain.CommentID = Guid.NewGuid();
            domain.CommentBody = newcommentDTO.CommentBody;
            domain.CommenterUserID = newcommentDTO.CommenterUserID;
            domain.CommentedScore = newcommentDTO.CommentedScore;
            domain.CommentCreatedAt = DateTime.Now;
            domain.CommentModifiedAt = DateTime.Now;
            domain.IsHarmFul = (int)newcommentDTO.IsHarmFul;
            domain.IsHelpful = (int)newcommentDTO.IsHelpful;

            await _context.UserComments.AddAsync(domain);
            await _context.SaveChangesAsync();

            return domain;
        }

        public async Task<UserComment> DetailAsync(Guid id)
        {
            var returnedComment = await _context.UserComments.FirstOrDefaultAsync(x => x.CommentID == id);
            return returnedComment;
        }

        public async Task<UserComment> Delete(Guid id)
        {
            var result = await _context.UserComments.FirstOrDefaultAsync(x =>x.CommentID == id);
            _context.UserComments.Remove(result);
            await _context.SaveChangesAsync();
            return result;
        }
    }
}
