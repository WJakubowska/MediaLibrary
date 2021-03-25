using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Aplikacja
{
    class Author
    {
        public int Id { get; set; }
        [MaxLength(100)] // nazwa maks 100 znakow
        public string Name { get; set; } = string.Empty;

        public List<Song> Songs { get; set; } = new List<Song>();
    }

}