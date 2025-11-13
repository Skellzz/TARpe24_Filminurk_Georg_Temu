using Filminurk.Core.ServiceInterface;
using Filminurk.Data;
using Filminurk.Models.Actors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace Filminurk.Controllers
{
    public class ActorsController : Controller
    {
        private readonly FilminurkTARpe24Context _context;
        private readonly IMovieServices _movieServices;
        private readonly IFilesServices _filesServices;

        public ActorsController
            (
                FilminurkTARpe24Context context,
                IMovieServices movieServices,
                IFilesServices filesServices

            )
        {
            _context = context;
            _movieServices = movieServices;
            _filesServices = filesServices;
        }
        public IActionResult Index()
        {
            var result = _context.Actors.Select(x => new ActorsIndexViewModel
            {
                ActorID = x.ActorID,
                FirstName = x.FirstName,
                LastName = x.LastName,
                Nickname = x.Nickname,
                MoviesActedFor = x.MoviesActedFor,
                PortraitID = x.PortraitID,
                Description = x.Description,
                Rating = x.Rating,
                ParimFilm = (ParimFilm?)x.ParimFilm,

            });
            return View(result);
        }

    }
}
