using Filminurk.Core.Dto;
using Filminurk.Core.Domain;
using Filminurk.Data;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Filminurk.Core.ServiceInterface;
using Microsoft.EntityFrameworkCore;


namespace Filminurk.ApplicationServices.Services
{
    public class FilesServices : IFilesServices
    {
        private readonly IHostEnvironment _webHost;
        private readonly FilminurkTARpe24Context _context;

        public FilesServices(IHostEnvironment webHost, FilminurkTARpe24Context context)
        {
            _webHost = webHost;
            _context = context;
        }

        public void FilesToApi(MoviesDTO dto, Movie domain)
        { 
            if (dto.Files != null && dto.Files.Count > 0)
            {
                if (Directory.Exists(_webHost.ContentRootPath + "\\wwroot\\multibleFileUpload\\"))
                {
                   Directory.CreateDirectory(_webHost.ContentRootPath + "\\wwroot\\multibleFileUpload\\");
                }

                foreach (var file in dto.Files)
                { 
                    string uploadsFolder = Path.Combine(_webHost.ContentRootPath, "wwroot", "multibleFileUpload");
                    string uniqueFileName = Guid.NewGuid().ToString() + "_" + file.FileName;
                    string filePath = Path.Combine(uploadsFolder, uniqueFileName);

                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    { 
                        file.CopyTo(fileStream);
                        FileToApi path = new FileToApi()
                        {
                            ImageID = Guid.NewGuid(),
                            ExsistingFilePath = uniqueFileName,
                            MovieID = domain.ID,
                        };

                        _context.FilesToApi.AddAsync(path);
                    }
                }
                 

            }
        }

        public async Task<FileToApi> RemoveImageFromApi(FileToApiDTO dto)
        {
           var imageID = await _context.FilesToApi.FirstOrDefaultAsync(x => x.ImageID == dto.ImageID);

            var filePath = _webHost.ContentRootPath + "\\wwroot\\multibleFileUpload\\" + imageID.ExsistingFilePath;

            if (File.Exists(filePath))
            {
                File.Delete(filePath);
            }

            _context.FilesToApi.Remove(imageID);
            await _context.SaveChangesAsync();

            return null;
        }

        public Task<List<FileToApi>> RemoveImageFromApi(FileToApi[] dto)
        {
            throw new NotImplementedException();
        }

        public async Task<List<FileToApi>> RemoveImagesFromApi(FileToApiDTO[] dtos) 
        {
            foreach (var dto in dtos)
            {
                RemoveImageFromApi(dto);
            }
            return null;
        }
    }
}
