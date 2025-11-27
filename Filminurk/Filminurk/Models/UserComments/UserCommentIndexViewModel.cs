using System.ComponentModel.DataAnnotations;

namespace Filminurk.Models.UserComments
{
    public class UserCommentIndexViewModel
    {
        [Key]
        public Guid CommentID { get; set; }
        public string? CommenterUserID { get; set; }
        public string CommentBody { get; set; }
        public int? CommentedScore { get; set; }
        public int? IsHelpful { get; set; }
        public int IsHarmFul { get; set; }
        public DateTime CommentCreatedAt { get; set; }
        public DateTime? CommentModifiedAt { get; set; }
        public DateTime? CommentDeletedAt { get; set; }
    }
}
