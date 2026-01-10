using Filminurk.Core.Domain;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Filminurk.Data
{
    public class FilminurkTARpe24Context : IdentityDbContext<ApplicationUser>
    {
        public FilminurkTARpe24Context(DbContextOptions<FilminurkTARpe24Context> options) : base(options) { }
        public DbSet<Movie> Movies { get; set; }
        public DbSet<FileToApi> FilesToApi { get; set; }
        public DbSet<UserComment> UserComments { get; set; }
        public DbSet<Actors> Actors { get; set; }
        public DbSet<FavoriteList> FavoriteLists { get; set; }
        public DbSet<FileToDatabase> FilesToDatabase { get; set; }
        //siin on Actor 
        public DbSet<Actors> Actor { get; set; }
        public DbSet<IdentityRole> IdentityRoles { get; set; }

    }
}