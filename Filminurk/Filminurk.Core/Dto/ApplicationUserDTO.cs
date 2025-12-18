using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Filminurk.Core.Dto
{
    public class ApplicationUserDTO
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public bool ProfileType { get; set; }
        public List<Guid>? FavouriteListIDs { get; set; }
        public List<Guid>? CommentIDs { get; set; }
        public string? AvatarImageID { get; set; }
        public string DisplayName { get; set; }

        public int? KuiPaljuSigmasiÕeldud { get; set; } = 0;
        public string? luuletus { get; set; }
    }
}
