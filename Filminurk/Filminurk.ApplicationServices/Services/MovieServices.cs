/*using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Filminurk.Core.Domain;
using Filminurk.Core.Dto;
using Filminurk.Core.ServiceInterface;
using Filminurk.Data;
using Microsoft.EntityFrameworkCore;

namespace Filminurk.ApplicationServices.Services
{
    public class MovieServices : IMovieServices
    {
        private readonly FilminurkTARpe24Context _context;
        private readonly IFilesServices _filesServices; // failid
        public MovieServices(FilminurkTARpe24Context context) 
        {
            _context = context;
        }

        public async Task<Movie> Create(MoviesDTO dto)
        {
            
            Movie movie = new Movie();
            movie.ID=Guid.NewGuid();
            movie.Title = dto.Title;
            movie.Description = dto.Description;
            movie.Warnings = dto.Warnings;
            movie.FirstPublished = (DateOnly)dto.FirstPublished;
            movie.Actors = dto.Actors;
            movie.Genre = dto.Genre;
            movie.Director = dto.Director;
            movie.Tagline = dto.Tagline;
            movie.EntryCreatedAt = DateTime.Now;
            movie.EntryModifiedAt = DateTime.Now;
            if (dto.Images != null)
            {
                _filesServices.FilesToApi(dto, movie);
            await _context.Movies.AddAsync(movie);
            await _context.SaveChangesAsync();
            return movie;
            }
            return null;
            
        }
        public async Task<Movie> DetailsAsync(Guid id)
        {
            var result = await _context.Movies.FirstOrDefaultAsync(x => x.ID==id);
            return result;
        }
        public async Task<Movie> Update(MoviesDTO dto)
        {
            Movie movie = new Movie();
            movie.ID = (Guid)dto.ID;
            movie.Title = dto.Title;
            movie.Description = dto.Description;
            movie.Warnings = dto.Warnings;
            movie.FirstPublished = (DateOnly)dto.FirstPublished;
            movie.Actors = dto.Actors;
            movie.Genre = dto.Genre;
            movie.Director = dto.Director;
            movie.Tagline = dto.Tagline;
            movie.EntryCreatedAt = DateTime.Now;
            movie.EntryModifiedAt = DateTime.Now;

            _context.Movies.Update(movie);
            await _context.SaveChangesAsync();
            return movie;
        }

        public async Task<Movie> Delete(Guid ID)
        {
            var result = await _context.Movies.FirstOrDefaultAsync(m => m.ID == ID);
            var images = await _context.FileToApi.Where(x => x.MovieID == ID).Select(y => new FileToApiDTO { ImageID = y.ImageID, MovieID = y.MovieID, FilePath = y.ExistingFilePath }).ToArrayAsync();
            _context.Movies.Remove(result);
            await _context.SaveChangesAsync();
            return result;
        }
    }
}  */
﻿using Filminurk.Core.Domain;
using Filminurk.Core.Dto;
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
    public class MovieServices : IMovieServices
    {
        private readonly FilminurkTARpe24Context _context;
        private readonly IFilesServices _filesServices; // failid

        public MovieServices
            (
            FilminurkTARpe24Context context,
            IFilesServices filesServices // failid
            )
        {
            _context = context;
            _filesServices = filesServices; // failid
        }

        public async Task<Movie> Create(MoviesDTO dto)
        {
            Movie movie = new Movie();
            movie.ID = (Guid)dto.ID;
            movie.Title = dto.Title;
            movie.Description = dto.Description;
            movie.Warnings = dto.Warnings;
            movie.FirstPublished = (DateOnly)dto.FirstPublished;
            movie.Actors = dto.Actors;
            movie.Genre = dto.Genre;
            movie.Director = dto.Director;
            movie.Tagline = dto.Tagline;
            movie.EntryCreatedAt = DateTime.Now;
            movie.EntryModifiedAt = DateTime.Now;
            _filesServices.FilesToApi(dto, movie);

            await _context.Movies.AddAsync(movie);
            await _context.SaveChangesAsync();

            return movie;
        }
        public async Task<Movie> DetailsAsync(Guid id)
        {
            var result = await _context.Movies.FirstOrDefaultAsync(x => x.ID == id);
            return result;
        }

        public async Task<Movie> Update(MoviesDTO dto)
        {
            Movie movie = new Movie();

            movie.ID = (Guid)dto.ID;
            movie.Title = dto.Title;
            movie.Description = dto.Description;
            movie.Warnings = dto.Warnings;
            movie.FirstPublished = (DateOnly)dto.FirstPublished;
            movie.Actors = dto.Actors;
            movie.Genre = dto.Genre;
            movie.Director = dto.Director;
            movie.Tagline = dto.Tagline;
            movie.EntryCreatedAt = DateTime.Now;
            movie.EntryModifiedAt = DateTime.Now;
            _filesServices.FilesToApi(dto, movie);

            _context.Movies.Update(movie);
            await _context.SaveChangesAsync();
            return movie;
        }

        public async Task<Movie> Delete(Guid id)
        {

            var result = await _context.Movies
                .FirstOrDefaultAsync(m => m.ID == id);

            var images = await _context.FilesToApi
                .Where(x => x.MovieID == id)
                .Select(y => new FileToApiDTO
                {
                    ImageID = y.ImageID,
                    MovieID = y.MovieID,
                    FilePath = y.ExistingFilePath
                }).ToArrayAsync();

            await _filesServices.RemoveImagesFromApi(images);
            _context.Movies.Remove(result);
            await _context.SaveChangesAsync();

            return result;
        }
    }
}