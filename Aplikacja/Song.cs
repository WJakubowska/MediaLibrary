using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


    class Song
    {
        public int Id { get; set; }
        [MaxLength(100)]
        public string Title { get; set; } = string.Empty;
        public string Directory { get; set; } = string.Empty;
        public Author SongAuthor { get; set; }

        public int SongId { get; set; }
    }

