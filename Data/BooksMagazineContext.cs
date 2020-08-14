using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using BooksMagazine.Models.HomeModels;
using BooksMagazine.Models.AboutModels;
using BooksMagazine.Models;

namespace BooksMagazine.Data
{
    public class BooksMagazineContext : DbContext
    {
        public BooksMagazineContext (DbContextOptions<BooksMagazineContext> options)
            : base(options)
        {
        }

        public DbSet<BooksMagazine.Models.HomeModels.Home> Home { get; set; }

        public DbSet<BooksMagazine.Models.HomeModels.CarouselItem> CarouselItem { get; set; }

        public DbSet<BooksMagazine.Models.HomeModels.Welcome> Welcome { get; set; }

        public DbSet<BooksMagazine.Models.HomeModels.Topic> Topic { get; set; }

        public DbSet<BooksMagazine.Models.AboutModels.About> About { get; set; }

        public DbSet<BooksMagazine.Models.AboutModels.AboutUs> AboutUs { get; set; }

        public DbSet<BooksMagazine.Models.AboutModels.Team> Team { get; set; }

        public DbSet<BooksMagazine.Models.Book> Book { get; set; }

        public DbSet<BooksMagazine.Models.ContactUs> ContactUs { get; set; }

        public DbSet<BooksMagazine.Models.Admin> Admin { get; set; }

        public DbSet<BooksMagazine.Models.TokenParamater> TokenParamater { get; set; }
    }
}
