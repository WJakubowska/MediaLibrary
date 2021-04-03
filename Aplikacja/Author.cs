using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Aplikacja
{
    public class Author
    {
        [Required]
        public int AuthorId { get; set; }
        [MaxLength(100)]
        [Required]
        public string Name { get; set; }
        public virtual List<Song> Songs { get; set; } = new List<Song>();
    }
}
