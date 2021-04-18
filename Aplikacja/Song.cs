using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace Aplikacja
{
    /// <summary>
    /// Class representing data tables : Song. 
    /// Constains infomration about id, title, directory and author. 
    /// It is related to the class Author. 
    /// </summary>
    public class Song
    {
        ///<summary>
        ///Store for the SongId property. This field is required.
        ///</summary>
        [Required]
        public int SongId { get; set; }


        ///<summary>
        ///Store for the Title property. The maximum length of a title is 100 characters
        ///</summary>
        [MaxLength(100)]
        public string Title { get; set; } = string.Empty;

        ///<summary>
        ///Store for the Directory property
        ///</summary>
        public string Directory { get; set; } = string.Empty;

        ///<summary>
        ///Store for the Author property. This field is required.
        ///</summary>
        [Required]
        public virtual Author Author { get; set; }
    }
}
