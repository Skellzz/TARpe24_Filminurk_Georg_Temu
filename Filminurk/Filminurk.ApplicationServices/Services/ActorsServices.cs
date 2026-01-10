using Filminurk.Core.Domain;
using Filminurk.Core.Dto;
using Filminurk.Core.ServiceInterface;
using Filminurk.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//Siin on ActorsServices 
namespace Filminurk.ApplicationServices.Services
{
    public class ActorsServices : IActorsServices
    {
        private readonly FilminurkTARpe24Context _context;
        private readonly IFilesServices _filesServices;

        public ActorsServices
            (
            FilminurkTARpe24Context context,
            IFilesServices filesServices 
            )
        {
            _context = context;
            _filesServices = filesServices; 
        }

        public async Task<Actors> Create(ActorsDTO dto)
        {
            Actors actors = new Actors();
            actors.ID = (Guid)dto.ID;
            actors.FirstName = dto.FirstName;
            actors.LastName = dto.LastName;
            actors.NickName = dto.NickName;
            actors.MoviesActedFor = dto.MoviesActedFor;
            actors.PortraitID = dto.PortraitID.ToString();
            actors.ActorRating = dto.ActorRating;
            actors.Gender = dto.Gender;
            actors.MostActedGenre = dto.MostActedGenre;
            actors.EntryCreatedAt = DateTime.Now;
            actors.EntryModifiedAt = DateTime.Now;
            //_filesServices.FilesToApi(dto, actors);

            await _context.Actors.AddAsync(actors);
            await _context.SaveChangesAsync();

            return actors;
        }
        public async Task<Actors> DetailsAsync(Guid id)
        {
            var result = await _context.Actors.FirstOrDefaultAsync(x => x.ID == id);
            return result;
        }

        public async Task<Actors> Update(ActorsDTO dto)
        {
            Actors actors = new Actors();

            actors.ID = (Guid)dto.ID;
            actors.FirstName = dto.FirstName;
            actors.LastName = dto.LastName;
            actors.NickName = dto.NickName;
            actors.MoviesActedFor = dto.MoviesActedFor;
            actors.PortraitID = dto.PortraitID.ToString();
            actors.ActorRating = dto.ActorRating;
            actors.Gender = dto.Gender;
            actors.MostActedGenre = dto.MostActedGenre;
            actors.EntryCreatedAt = DateTime.Now;
            actors.EntryModifiedAt = DateTime.Now;
            //_filesServices.FilesToApi(dto, actors);

            _context.Actors.Update(actors);
            await _context.SaveChangesAsync();
            return actors;
        }

        public async Task<Actors> Delete(Guid id)
        {

            var result = await _context.Actors
                .FirstOrDefaultAsync(m => m.ID == id);

            /*var images = await _context.FilesToApi
                .Where(x => x.ID == id)
                .Select(y => new FileToApiDTO
                {
                    ImageID = y.ImageID,
                    MovieID = y.MovieID,
                    FilePath = y.ExistingFilePath
                }).ToArrayAsync();*/

            //await _filesServices.RemoveImagesFromApi(images);
            _context.Actors.Remove(result);
            await _context.SaveChangesAsync();

            return result;
        }
    }
}