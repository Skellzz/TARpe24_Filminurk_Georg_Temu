using Filminurk.Core.ServiceInterface;
using Filminurk.Data;
using Filminurk.Models.Movies;
using Microsoft.AspNetCore.Mvc;

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
    }
    public IActionResult Index()
        {
            var result = _context.Movies.Select(x => new MoviesIndexViewModel
            {
                ID = x.ID,
                Title = x.Title,
                FirstPublished = x.FirstPublished,
                CurrentRating = x.CurrentRating,
                CountryOfOrigin = x.CountryOfOrigin,
                MovieGenre = x.MovieGenre,

            });
            return View(result);
        }

    }
}
