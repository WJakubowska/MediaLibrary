using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace Aplikacja
{

  


    class DatebaseContext : DbContext
    {
        public DbSet<Author> Authors { get; set; }
        public DbSet<Song> Songs { get; set; }

        public DatebaseContext(DbContextOptions<DatebaseContext> options)
            : base(options)
        {

        }
    }

    class DatebaseContextFactory : IDesignTimeDbContextFactory<DatebaseContext>
    {
        public DatebaseContext CreateDbContext(string[] args)
        {
            var configuration = new ConfigurationBuilder().AddJsonFile("appsettings.json")
                                                          .Build();
            var optionsBuilder = new DbContextOptionsBuilder<DatebaseContext>();
            optionsBuilder
                .UseSqlServer(configuration["ConnectionStrings:DefaultConnection"]);

            return new DatebaseContext(optionsBuilder.Options);
        }
    }

}