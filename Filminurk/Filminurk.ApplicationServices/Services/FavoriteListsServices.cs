using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Threading.Tasks;
using Filminurk.Core.Domain;
using Filminurk.Core.Dto;
using Filminurk.Core.ServiceInterface;
using Filminurk.Data;
using Microsoft.EntityFrameworkCore;

namespace Filminurk.ApplicationServices.Services
{
    public class FavoriteListsServices : IFavoriteListsServices
    {
        private readonly FilminurkTARpe24Context _context;
        private readonly IFilesServices _filesServices;

        public FavoriteListsServices(FilminurkTARpe24Context context, IFilesServices filesServices)
        {
            _context = context;
            _filesServices = filesServices;
        }
        public async Task<FavoriteList> DetailsAsync(Guid id)
        {
            var result = await _context.FavoriteLists.AsNoTracking().FirstOrDefaultAsync(x => x.FavoriteListID == id);
            return result;
        }
        public async Task<FavoriteList> Create(FavoriteListDTO dto/*, List<Movie> selectedMovies*/)
        {
            FavoriteList newList = new();
            newList.FavoriteListID = dto.FavoriteListID;
            newList.ListName = dto.ListName;
            newList.Description = dto.Description;
            newList.IsPrivate = dto.IsPrivate;
            newList.ListCreatedAt = dto.ListCreatedAt;
            newList.ListDeletedAt = dto.ListDeletedAt;
            newList.ListModifiedAt = dto.ListModifiedAt;
            newList.ListOfMovies = dto.ListOfMovies;
            newList.ListBelongsToUser = dto.ListBelongsToUser;
            await _context.FavoriteLists.AddAsync(newList);
            await _context.SaveChangesAsync();
            /*foreach (var movie in selectedMovies)
            {
                _context.Entry(movie).Property(p => p.ID);
            } */
            return newList;
        }
        public async Task<FavoriteList> Update(FavoriteListDTO updatedList, string typeOfMethod)
        {

            FavoriteList updatedListInDB = new();

            updatedListInDB.FavoriteListID = 
                updatedList.FavoriteListID;
            updatedListInDB.ListBelongsToUser = 
                updatedList.ListBelongsToUser;
            updatedListInDB.IsMovieOrActor = 
                updatedList.IsMovieOrActor;
            updatedListInDB.ListName = 
                updatedList.ListName;
            updatedListInDB.Description =
                updatedList.Description;
            updatedListInDB.IsPrivate = 
                updatedList.IsPrivate;
            updatedListInDB.ListOfMovies =
                updatedList.ListOfMovies;
            updatedListInDB.ListDeletedAt =
                updatedList.ListDeletedAt;
            updatedListInDB.ListModifiedAt =
                updatedList.ListModifiedAt;
            if (typeOfMethod == "Delete")
            {
                _context.FavoriteLists.Attach(updatedListInDB);
                _context.Entry(updatedListInDB).Property(l => l.ListDeletedAt).IsModified = true;
                _context.Entry(updatedListInDB).Property(l => l.ListModifiedAt).IsModified = true;
            }
            else if (typeOfMethod == "Private")
            {
                _context.FavoriteLists.Attach(updatedListInDB);
                _context.Entry(updatedListInDB).Property(l => l.IsPrivate).IsModified = true;
            }
                _context.FavoriteLists.Update(updatedListInDB);
            _context.Entry(updatedListInDB).Property(l => l.ListModifiedAt).IsModified = true;
            await _context.SaveChangesAsync();
            return updatedListInDB;


        }
    }
}
