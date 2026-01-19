using Filminurk.ApplicationServices.Services;
using Filminurk.Core.Domain;
using Filminurk.Core.Dto;
using Filminurk.Core.Dto.OMDbApiDTO;
using Filminurk.Core.ServiceInterface;
using Filminurk.Models.OMDbApi;
using Microsoft.AspNetCore.Mvc;
using Filminurk.Data;
using Filminurk.Models.Movies;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.IO;
using static System.Net.WebRequestMethods;
using static Azure.Core.HttpHeader;
using System.Diagnostics.CodeAnalysis;
using Newtonsoft.Json.Linq;
using System;

namespace Filminurk.Controllers
{
    public class OmdbapiController : Controller
    {
        private readonly IOMDbApiServices _omdbapiServices;
        public OmdbapiController(IOMDbApiServices omdbapiServices)
        {
            _omdbapiServices = omdbapiServices;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult FindMovie(OMDbApiSearchViewModel model)
        {
            if (ModelState.IsValid)
            {
                return Import(model.Title);
            }
            return NotFound();
        }

        [HttpGet]
        public IActionResult Import(string title)
        {
            OMDbApiMovieResultDTO dto = new();
            dto.Title = title;
            _omdbapiServices.OMDbApiResult(dto);
            OMDbApiViewModel vm = new();
            vm.Title = dto.Title;
            vm.FirstPublished = dto.FirstPublished;
            if (Genre.IsDefined(typeof(Genre), dto.Genre))
            {
                vm.Genre = (Genre)Enum.Parse(typeof(Genre), dto.Genre);
            }
            else
            {
                vm.Genre = Genre.smthElse;
            }
            vm.imdbRating = dto.imdbRating;
            vm.Actors = dto.Actors;
            vm.Producer = dto.Director;
            vm.Description = dto.Description;
            return View("Import", vm);
        }
        [HttpPost]
        public IActionResult Import(OMDbApiViewModel vm)
        {
            if (ModelState.IsValid)
            {
                var dto = new OMDbApiMovieCreateDTO()
                {
                    ID = Guid.NewGuid(),
                    Title = vm.Title,
                    Description = vm.Description,
                    Producer = vm.Producer,
                    Actors = vm.Actors.Split(new char[] { ',' }).ToList(),
                    CurrentRating = double.Parse(vm.imdbRating, System.Globalization.CultureInfo.InvariantCulture),
                    Genre = vm.Genre,
                    EntryCreatedAt = DateTime.Now,
                    EntryModifiedAt = DateTime.Now,
                };

                if (vm.FirstPublished == "N/A" || vm.FirstPublished == null)
                {
                    dto.FirstPublished = DateOnly.MinValue;
                }
                else
                {
                    dto.FirstPublished = DateOnly.Parse(vm.FirstPublished);
                }


                var result = _omdbapiServices.Create(dto);
                if (result == null)
                {
                    return NotFound();
                }
                if (!ModelState.IsValid)
                {
                    return NotFound();
                }
            }

            return RedirectToAction(nameof(Index));
        }
    }

}