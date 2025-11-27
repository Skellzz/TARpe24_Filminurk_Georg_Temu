using Filminurk.ApplicationServices.Services;
using Filminurk.Core.Dto;
using Filminurk.Core.ServiceInterface;
using Filminurk.Data;
using Filminurk.Models.UserComments;
using Microsoft.AspNetCore.Mvc;

namespace Filminurk.Controllers
{
    public class UserCommentsController : Controller
    {
        private readonly FilminurkTARpe24Context _context;
        private readonly IUserCommentsServices _userCommentsServices;
        public UserCommentsController(FilminurkTARpe24Context context, IUserCommentsServices userCommentServices)
        {
            _context = context;
            _userCommentsServices = userCommentServices;
        }

        public IActionResult Index()
        {
            var result = _context.UserComments
                .Select(c => new UserCommentIndexViewModel
                {
                    CommentID = c.CommentID,
                    CommentBody = c.CommentBody,
                    IsHarmFul = (int)c.IsHarmFul,
                    CommentCreatedAt = c.CommentCreatedAt,
                }
            );
            return View(result);
        }
        [HttpGet]
        public IActionResult NewComment()
        {
            //TODO:
            UserCommentCreateViewModel newcomment = new();
            return View(newcomment);
        }
        [HttpPost, ActionName("NewComment")]
        public async Task<IActionResult> NewCommentPost(UserCommentCreateViewModel newcommentVM)
        {
            if (ModelState.IsValid)
            {

                var dto = new UserCommentDTO() { };
                dto.CommentID = (Guid)newcommentVM.CommentID;
                dto.CommentBody = newcommentVM.CommentBody;
                dto.CommenterUserID = newcommentVM.CommenterUserID;
                dto.CommentedScore = newcommentVM.CommentedScore;
                dto.CommentCreatedAt = newcommentVM.CommentCreatedAt;
                dto.CommentModifiedAt = newcommentVM.CommentModifiedAt;
                dto.CommentDeletedAt = newcommentVM.CommentDeletedAt;
                    dto.IsHelpful = 0;
                    dto.IsHarmFul = 0;

                var result = await _userCommentsServices.NewComment(dto);
            
            if (result == null)
            {
                return NotFound();
            }
            // TODO: Erista kas on kasutaja või admin!!
            // Tagastab admin-comments-index, kasutaja aga vastava filmi juurde
            return RedirectToAction(nameof(Index));
            }
            return NotFound();
        }
        [HttpGet]
        public async Task<IActionResult> DetailsAdmin(Guid id)
        {
            var requestedComment = await _userCommentsServices.DetailAsync(id);
            if (requestedComment == null) { return NotFound(); }
            var commentVM = new UserCommentIndexViewModel();

            commentVM.CommentID = requestedComment.CommentID;
            commentVM.CommentBody = requestedComment.CommentBody;
            commentVM.CommenterUserID = requestedComment.CommenterUserID;
            commentVM.CommentedScore = requestedComment.CommentedScore;
            commentVM.CommentCreatedAt = requestedComment.CommentCreatedAt;
            commentVM.CommentModifiedAt = requestedComment.CommentModifiedAt;
            commentVM.CommentDeletedAt = requestedComment.CommentDeletedAt;
            return View(commentVM);
        }
        [HttpGet]
        public async Task<IActionResult> DeleteAdmin(Guid id)
        {
            var deleteEntry = await _userCommentsServices.DetailAsync(id);
                if (deleteEntry == null) { return NotFound(); }

            var commentVM = new UserCommentIndexViewModel();
            commentVM.CommentID = deleteEntry.CommentID;
            commentVM.CommentBody = deleteEntry.CommentBody;
            commentVM.CommenterUserID = deleteEntry.CommenterUserID;
            commentVM.CommentedScore = deleteEntry.CommentedScore;
            commentVM.CommentCreatedAt = deleteEntry.CommentCreatedAt;
            commentVM.CommentModifiedAt = deleteEntry.CommentModifiedAt;
            commentVM.CommentDeletedAt = deleteEntry.CommentDeletedAt;
            return View(commentVM);
        }
        [HttpPost]
        public async Task<IActionResult> DeleteAdminPost(Guid id)
        {
            var deleteThisComment = await _userCommentsServices.Delete(id);
            if (deleteThisComment == null) { return NotFound(); }
            return RedirectToAction("Index");
        }
    }
}
