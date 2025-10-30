using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Filminurk.Core.Dto
{
    public class ActorsDTO
    {
      public Guid ActorID { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public List<string> MoviesActedFor { get; set; }
        public int? PortraitID { get; set; }

        //enda tehhtud andmed

    }
}
