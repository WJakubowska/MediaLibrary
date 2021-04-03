using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Aplikacja
{
    public class Song
    {
        [Required]
        public int SongId { get; set; }
        [MaxLength(100)]
        public string Title { get; set; } = string.Empty;
        public string Directory { get; set; } = string.Empty;
        [Required]
        public virtual Author Author { get; set; }
    }
}
