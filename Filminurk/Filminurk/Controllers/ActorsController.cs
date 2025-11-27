using Filminurk.ApplicationServices.Services;
using Filminurk.Core.Domain;
using Filminurk.Core.Dto;
using Filminurk.Core.ServiceInterface;
using Filminurk.Data;
using Filminurk.Data.Migrations;
using Filminurk.Models.Actors;
using Filminurk.Models.Movies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Filminurk.Controllers
{
    public class ActorsController : Controller
    {
        private readonly FilminurkTARpe24Context _context;
        private readonly IActorsServices _actorsServices;
        private readonly IFilesServices _fileServices;
        public ActorsController(FilminurkTARpe24Context context, IActorsServices actorsServices, IFilesServices fileServices)
        {
            _context = context;
            _actorsServices = actorsServices;
            _fileServices = fileServices;
        }
        public IActionResult Index()
        {
            var result = _context.Actors.Select(x => new ActorsIndexViewModel
            {                                                                       
                ID = x.ID,                                                          
                FirstName = x.FirstName,                                            
                LastName = x.LastName,                                              
                NickName = x.NickName,                                              
                ActorRating = x.ActorRating,                                        
                Gender = x.Gender,                                                  
                FavoriteGenre = x.FavoriteGenre                                     
            });                                                                     
            return View(result);                                                    
        }                                                                           
        [HttpGet]                                                                   
        public IActionResult Create()                                               
        {                                                                           
            ActorsCreateUpdateViewModel result = new();                             
            return View("CreateUpdate", result);                                    
        }                                                                           
        [HttpPost]                                                                  
        public async Task<IActionResult> Create(ActorsCreateUpdateViewModel vm)     
        {
            var dto = new ActorsDTO();
            if (ModelState.IsValid)
            {
                
                /*{
                    ID = vm.ID,
                    FirstName = vm.FirstName,
                    LastName = vm.LastName,
                    NickName = vm.NickName,
                    MoviesActedFor = vm.MoviesActedFor,
                    ActorRating = vm.ActorRating,
                    Gender = vm.Gender,
                    FavoriteGenre = vm.FavoriteGenre,
                    Files = vm.Files,
                    Images = vm.Images
                    .Select(x => new FileToApiDTO
                    {
                        ImageID = x.ImageID,
                        FilePath = x.FilePath,
                        MovieID = x.MovieID,
                        IsPoster = x.IsPoster,
                    }).ToArray(),
                    PortraitID = Guid.Parse(vm.PortraitID)
                    /*.Select(x => new FileToApiDTO
                    {
                        ImageID = x.ImageID,
                        FilePath = x.FilePath,
                        MovieID = x.MovieID,
                        IsPoster = x.IsPoster,
                    }).ToString()
                };        */
                dto.ID = vm.ID;
                dto.FirstName = vm.FirstName;
                dto.LastName = vm.LastName;
                dto.NickName = vm.NickName;
                dto.MoviesActedFor = vm.MoviesActedFor;
                dto.ActorRating = vm.ActorRating;
                dto.Gender = vm.Gender;
                dto.FavoriteGenre = vm.FavoriteGenre;
                dto.Files = vm.Files;
                /*dto.PortraitID = Guid.Parse(vm.PortraitID);
                dto.Images = vm.Images.Select(x => new FileToApiDTO
                {
                    ImageID = x.ImageID,
                    FilePath = x.FilePath,
                    MovieID = x.MovieID,
                    IsPoster = x.IsPoster,
                }).ToArray();*/
            }
                var result = await _actorsServices.Create(dto);
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

        [HttpGet]
        public async Task<IActionResult> Details(Guid id)
        {
            var actors = await _actorsServices.DetailsAsync(id);
            if (actors == null)
            {
                return NotFound();
            }
            //ImageViewModel[] images = await FileFromDatabase(id);
            var vm = new ActorsDetailsViewModel();
            vm.ID = actors.ID;
            vm.FirstName = actors.FirstName;
            vm.LastName = actors.LastName;
            vm.NickName = actors.NickName;
            vm.MoviesActedFor = actors.MoviesActedFor;
            vm.ActorRating = actors.ActorRating;
            vm.Gender = actors.Gender;
            vm.FavoriteGenre = actors.FavoriteGenre;
            vm.PortraitID = actors.PortraitID;
            vm.EntryCreatedAt = actors.EntryCreatedAt;
            vm.EntryModifiedAt = actors.EntryModifiedAt;
            return View(vm);
        }

        [HttpGet]
        public async Task<IActionResult> Update(Guid id)
        {
            var actors = await _actorsServices.DetailsAsync(id);
            if (actors == null)
            {
                return NotFound();
            }
            var vm = new ActorsCreateUpdateViewModel();

            vm.ID = actors.ID;
            vm.FirstName = actors.FirstName;
            vm.LastName = actors.LastName;
            vm.NickName = actors.NickName;
            vm.MoviesActedFor = actors.MoviesActedFor;
            vm.ActorRating = actors.ActorRating;
            vm.Gender = actors.Gender;
            vm.FavoriteGenre = actors.FavoriteGenre;
            vm.PortraitID = actors.PortraitID?.ToString();
            vm.EntryCreatedAt = actors.EntryCreatedAt;
            vm.EntryModifiedAt = actors.EntryModifiedAt;
            return View("CreateUpdate", vm);
        }
        [HttpPost]
        public async Task<IActionResult> Update(ActorsCreateUpdateViewModel vm)
        {
            var dto = new ActorsDTO()
            {
                ID = vm.ID,
                FirstName = vm.FirstName,
                LastName = vm.LastName,
                NickName = vm.NickName,
                MoviesActedFor = vm.MoviesActedFor,
                ActorRating = vm.ActorRating,
                Gender = vm.Gender,
                FavoriteGenre = vm.FavoriteGenre,
                PortraitID = string.IsNullOrEmpty(vm.PortraitID)
                ? (Guid?)null
                : Guid.Parse(vm.PortraitID) 
                /* ma ei olnud kindel miks see ei lubanud update..*/
                /*.Select(x => new FileToApiDTO
                {
                    ImageID = x.ImageID,
                    FilePath = x.FilePath,
                    MovieID = x.MovieID,
                    IsPoster = x.IsPoster,
                }).ToString()*/
            };
            var result = await _actorsServices.Update(dto);
            if (result == null)
            {
                return NotFound();
            }
            return RedirectToAction(nameof(Index));
        }
        [HttpGet]
        public async Task<IActionResult> Delete(Guid ID)
        {
            var actors = await _actorsServices.DetailsAsync(ID);
            if (actors == null)
            {
                return NotFound();
            }

            //var images = await _context.FilesToApi.Where(x => x.MovieID == ID).Select(y => new ImageViewModel { FilePath = y.ExistingFilePath, ImageID = y.ImageID }).ToArrayAsync();
            var vm = new ActorsDeleteViewModel();
            vm.ID = actors.ID;
            vm.FirstName = actors.FirstName;
            vm.LastName = actors.LastName;
            vm.NickName = actors.NickName;
            vm.MoviesActedFor = actors.MoviesActedFor;
            vm.ActorRating = actors.ActorRating;
            vm.Gender = actors.Gender;
            vm.FavoriteGenre = actors.FavoriteGenre;
            vm.PortraitID = actors.PortraitID;
            vm.EntryCreatedAt = actors.EntryCreatedAt;
            vm.EntryModifiedAt = actors.EntryModifiedAt;

            return View(vm);

        }
        [HttpPost]
        public async Task<IActionResult> DeleteConfirmation(Guid ID)
        {
            var movie = await _actorsServices.Delete(ID);
            if (movie == null) { return NotFound(); }
            return RedirectToAction(nameof(Index));

        }
    }
}
