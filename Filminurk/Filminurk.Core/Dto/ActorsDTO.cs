using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Filminurk.Core.Dto
{
    public enum ParimFilm
    {
        VelecoPastor,
        Dursticpark,
        Tuhkatriinu,
        Matrix,
        Inception,
        Cars3,
        Oilmaailm

    }
    public class ActorsDTO
    {
        public Guid ActorID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string? Nickname { get; set; }
        public List<string> MoviesActedFor { get; set; }
        public int? PortraitID { get; set; }

        // Enda loodud andmed

        public string? Description { get; set; }
        public int? Rating { get; set; }
        public ParimFilm? ParimFilm { get; set; }
    }
}
