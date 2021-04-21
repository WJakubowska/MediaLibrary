using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;


namespace Aplikacja
{
    /// <summary>
    /// Class representing data tables : author.
    /// Contains information about id and name.
    /// It is related to the class Song.
    /// </summary>
    public class Author
    {
        ///<summary>
        ///Store for the AuthorId property
        ///</summary>
        [Required]
        public int AuthorId { get; set; }

        ///<summary>
        ///Store for the Name property
        ///</summary>
        [MaxLength(100)]
        [Required]
        public string Name { get; set; }

        ///<summary>
        ///List of object Song related to this class
        ///</summary>
        public virtual ObservableCollection<Song> Songs { get; set; } = new ObservableCollection<Song>();
    }
}
