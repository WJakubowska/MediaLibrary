using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace Aplikacja
{
    public class DatebaseContext : DbContext
    {
        public DbSet<Author> Authors { get; set; }
        public DbSet<Song> Songs { get; set; }




        public DatebaseContext()
        {
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=MediaDB");
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Author>(entity => {
                entity.HasIndex(e => e.Name).IsUnique();
            });
        }
    }



}
