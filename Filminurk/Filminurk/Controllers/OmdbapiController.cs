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

        [HttpGet]
        public IActionResult Index()
        {
            return View(new OMDbApiSearchViewModel());
        }

        [HttpPost]
        public async Task<IActionResult> FindMovie(OMDbApiSearchViewModel model, CancellationToken ct)
        {
            if (!ModelState.IsValid)
                return View("Index", model);

            var createdId = await _omdbapiServices.ImportByTitleAsync(model.Title, ct);

            if (createdId == null)
            {
                ModelState.AddModelError("", "Filmi ei leitud OMDb-st või import ebaõnnestus.");
                return View("Index", model);
            }

            return RedirectToAction("Details", "Movies", new { id = createdId.Value });
        }
    }
}
