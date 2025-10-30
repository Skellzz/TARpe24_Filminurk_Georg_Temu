using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace Filminurk.Models.UserComments
{
    public class UserCommentsIndexViewModel
    {
        [Key]
        public Guid CommentID { get; set; }
        public string? CommenterUserID { get; set; }
        public string CommentBody { get; set; }
        public int CommentScore { get; set; }
        public int IsHarmful { get; set; } //👍
        public int IsNotHelpful { get; set; } //��
        public DateTime CommentCreatedAt { get; set; }
        public DateTime CommentModifiedAt { get; set; }
        public DateTime CommentDeletedAt { get; set; }
    }
}
