using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;


namespace Aplikacja
{
    public class Author
    {
        [Required]
        public int AuthorId { get; set; }
        [MaxLength(100)]
        [Required]
        public string Name { get; set; }
        public virtual ObservableCollection<Song> Songs { get; set; } = new ObservableCollection<Song>();
    }
}
