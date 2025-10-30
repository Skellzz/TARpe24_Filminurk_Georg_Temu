using Filminurk.Core.Dto;
using Filminurk.Core.ServiceInterface;
using Filminurk.Data;
using Filminurk.Models.Movies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Filminurk.Controllers
{
    public class MoviesController : Controller
    {
        private readonly FilminurkTARpe24Context _context;
        private readonly IMovieServices _movieServices;
        private readonly IFilesServices _filesServices;

        public MoviesController
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
        [HttpGet]
        public IActionResult Create()
        {
            MoviesCreateUpdateViewModel result = new();
            return View("CreateUpdate", result);
        }
        [HttpPost]
        public async Task<IActionResult> Create(MoviesCreateUpdateViewModel vm)
        {
            if (ModelState.IsValid)
            {
                var dto = new MoviesDTO()
                {
                    ID = vm.ID,
                    Title = vm.Title,
                    Description = vm.Description,
                    FirstPublished = vm.FirstPublished,
                    Director = vm.Director,
                    Actors = vm.Actors,
                    CurrentRating = vm.CurrentRating,
                    CountryOfOrigin = vm.CountryOfOrigin,
                    MovieGenre = vm.MovieGenre,
                    SubGenre = vm.SubGenre,
                    EntryCreatedAt = vm.EntryCreatedAt,
                    EntryModifiedAt = vm.EntryModifiedAt,
                    Files = vm.Files,
                    FileToApiDTOs = vm.Images
                    .Select(x => new FileToApiDTO
                    {
                        ImageID = x.ImageID,
                        FilePath = x.FilePath,
                        MovieID = x.MovieID,
                        IsPoster = x.IsPoster,


                    }).ToArray()
                };
                var result = await _movieServices.Create(dto);
                if (result == null)
                {
                    return NotFound();
                }
                if (!ModelState.IsValid)
                {
                    return NotFound();
                }   
                return RedirectToAction(nameof(Index));
            }

            return RedirectToAction(nameof(Index));

        }

        [HttpGet]
        public async Task<IActionResult> Details(Guid id)
        {
            var movie = await _movieServices.DetailsAsync(id);

            if (movie == null)
            {
                return NotFound();
            }
            ImageViewModel[] Images = await FileFromDatabase(id);

            var vm = new MoviesDetailsViewModel();

            vm.ID = movie.ID;
            vm.Title = movie.Title;
            vm.Description = movie.Description;
            vm.FirstPublished = movie.FirstPublished;
            vm.CurrentRating = movie.CurrentRating;
            vm.CountryOfOrigin = movie.CountryOfOrigin;
            vm.MovieGenre = movie.MovieGenre;
            vm.SubGenre = movie.SubGenre;
            vm.EntryCreatedAt = movie.EntryCreatedAt;
            vm.EntryModifiedAt = movie.EntryModifiedAt;
            vm.Director = movie.Director;
            vm.Actors = movie.Actors;
            vm.Images.AddRange(Images);

            return View(vm);
        }



        [HttpGet]
        public async Task<IActionResult> Update(Guid id)
        {
            var movie = await _movieServices.DetailsAsync(id);

            if (movie == null)
            {
                return NotFound();
            }
            
            var images = await _context.FilesToApi
                .Where(x => x.MovieID == id)
                .Select(y => new ImageViewModel
                {
                   FilePath = y.ExsistingFilePath,
                   ImageID = id,

                }).ToArrayAsync();

            var vm = new MoviesCreateUpdateViewModel();
            vm.ID = movie.ID;
            vm.Title = movie.Title;
            vm.Description = movie.Description;
            vm.FirstPublished = movie.FirstPublished;
            vm.CurrentRating = movie.CurrentRating;
            vm.CountryOfOrigin = movie.CountryOfOrigin;
            vm.MovieGenre = movie.MovieGenre;
            vm.SubGenre = movie.SubGenre;
            vm.EntryCreatedAt = movie.EntryCreatedAt;
            vm.EntryModifiedAt = movie.EntryModifiedAt;
            vm.Director = movie.Director;
            vm.Actors = movie.Actors;
            vm.Images.AddRange(images);

            return View("CreateUpdate",vm);
        }

        [HttpPost]
        public async Task<IActionResult> Update(MoviesCreateUpdateViewModel vm)
        {
            var dto = new MoviesDTO()
            {
                ID = vm.ID,
                Title = vm.Title,
                Description = vm.Description,
                FirstPublished = vm.FirstPublished,
                CurrentRating = vm.CurrentRating,
                CountryOfOrigin = vm.CountryOfOrigin,
                MovieGenre = vm.MovieGenre,
                SubGenre = vm.SubGenre,
                EntryCreatedAt = vm.EntryCreatedAt,
                EntryModifiedAt = vm.EntryModifiedAt,
                Director = vm.Director,
                Actors = vm.Actors,
                Files = vm.Files,
                FileToApiDTOs = vm.Images
                .Select(x => new FileToApiDTO
                {
                    ImageID = x.ImageID,
                    FilePath = x.FilePath,
                    MovieID = x.MovieID,
                    
                }).ToArray()
            };

            var result = await _movieServices.Update(dto);
            if (result == null)
            {
                return NotFound();
            }
            return RedirectToAction(nameof(Index));

        }
        
        
        

        [HttpGet]
        public async Task<IActionResult> Delete(Guid id)
        {
            var movie = await _movieServices.DetailsAsync(id);

            if (movie == null)
            {
                return NotFound();
            }
            var images = await _context.FilesToApi
                .Where(x => x.MovieID == id)
                .Select(y => new ImageViewModel
                {
                    FilePath = y.ExsistingFilePath,
                    ImageID = y.ImageID,
                }).ToArrayAsync();

            var vm = new MoviesDeleteViewModel();
            vm.ID = movie.ID;
            vm.Title = movie.Title;
            vm.Description = movie.Description;
            vm.FirstPublished = movie.FirstPublished;
            vm.CurrentRating = movie.CurrentRating;
            vm.CountryOfOrigin = movie.CountryOfOrigin;
            vm.MovieGenre = movie.MovieGenre;
            vm.SubGenre = movie.SubGenre;
            vm.EntryCreatedAt = movie.EntryCreatedAt;
            vm.EntryModifiedAt = movie.EntryModifiedAt;
            vm.Director = movie.Director;
            vm.Actors = movie.Actors;
            vm.Images.AddRange(images);


            return View(vm);

        }
        [HttpPost]
        public async Task<IActionResult> DeleteConfirmation(Guid id)
        {
            var movie = await _movieServices.Delete(id);
            if (movie == null)
            {
                return NotFound();
            }
            return RedirectToAction(nameof(Index));
        }
        private async Task<ImageViewModel[]> FileFromDatabase(Guid id)
        {
            return await _context.FilesToApi
                 .Where(x => x.MovieID == id)
                 .Select(y => new ImageViewModel
                 {
                     ImageID = y.ImageID,
                     MovieID = y.MovieID,
                     IsPoster = y.IsPoster,
                     FilePath = y.ExsistingFilePath
                 }).ToArrayAsync();
        }
    }
}
