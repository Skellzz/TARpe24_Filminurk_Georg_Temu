using Filminurk.Core.Domain;
using Filminurk.Core.ServiceInterface;
using Filminurk.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        public async Task<FavouriteList> DetailsAsync(Guid id)
        { 
            var result = await _context.FavouriteLists
                .FirstOrDefaultAsync(x => x.FavouriteListID == id);
            return result;
        }

        public async Task<FavouriteList> Create(FavouriteList dto,List<Movie>
            selectedMovies)
        {
            FavouriteList newList = new();
            newList.FavouriteListID = Guid.NewGuid();
            newList.ListName = dto.ListName;
            newList.ListDescription = dto.ListDescription;
            newList.ListCreateAt = dto.ListCreateAt;
            newList.ListModifiedAt = dto.ListModifiedAt;
            newList.ListDeletedAt = dto.ListDeletedAt;
            newList.ListOfMovies = selectedMovies;
            await _context.FavouriteLists.AddAsync(newList);
            await _context.SaveChangesAsync();

            //foreach (var movieid in selectedMovies)
            //{
            //  _context.FavouriteLists.Entry
            //}
            return newList;

        }
    }
}
