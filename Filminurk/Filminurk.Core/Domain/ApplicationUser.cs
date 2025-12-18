using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace Filminurk.Core.Domain
{
    public class ApplicationUser : IdentityUser
    {
        public List<Guid>? FavouriteListIDs { get; set; }
        public List<Guid>? CommentIDs { get; set; }
        public string AvatarImageID { get; set; }
        public string DisplayName { get; set; }
        public bool ProfileType { get; set; }//false = user, true = admin

        /* 2 õpilase poolt väljamõeldud andmevälja */

        public int? KuiPaljuSigmasiÕeldud { get; set; } = 0;

        public string? luuletus { get; set; }


    }
}
