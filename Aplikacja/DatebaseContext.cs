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
    /// <summary>
    /// Class which is responsible for interacting with the entity model and the database.
    /// </summary>

    public class DatebaseContext : DbContext
    {
        /// <summary>
        /// Creates an instance of a DbSet called Authors
        /// </summary>
        public DbSet<Author> Authors { get; set; }

        /// <summary>
        /// Creates an instance of a DbSet called Songs
        /// </summary>
        public DbSet<Song> Songs { get; set; }

        /// <summary>
        /// The class constructor.
        /// </summary>
        public DatebaseContext()
        {
            Database.EnsureCreated();
        }

        /// <summary>
        /// This method configure the database to be used for this context
        /// </summary>
        /// <param name="optionsBuilder"> A builder used to create or modify options for this context </param>
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=MediaDB");
        }

        /// <summary>
        /// The method creates a base model 
        /// </summary>
        /// <param name="builder">The builder that defines the model for the context being created.</param>
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Author>(entity => {
                entity.HasIndex(e => e.Name).IsUnique();
            });
        }
    }



}
