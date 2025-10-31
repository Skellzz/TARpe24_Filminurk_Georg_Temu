using Filminurk.Core.ServiceInterface;
using Filminurk.Data;
using Filminurk.Models.Movies;
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
            var result = _context.Actors.Select(u => new ActorsIndexViewModel
            {
                ActorID = u.ActorID,
                FirstName = u.FirstName,
                LastName = u.LastName,
                Nickname = u.Nickname,
                MoviesActedFor = u.MoviesActedFor,
                PortraitID = u.PortraitID,
                Description = u.Description,
                Rating = u.Rating,
                ParimFilm = u.ParimFilm

            });
            return View(result);
        }

    }
}
