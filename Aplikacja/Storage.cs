using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace Aplikacja
{
    interface IStorage
    {
        ReadOnlyObservableCollection<Song> GetSongs();
        ReadOnlyObservableCollection<Author> GetAuthors();
        void AddAuthor(Author author);
        void AddSong(Song song);

    }
    class RuntimeMockStorage : IStorage
    {
        private ObservableCollection<Song> songs;
        private ObservableCollection<Author> authors;

        private static readonly Author author = new Author() { Name = "John Doe" };
        private static readonly String dir = "/path/to/song";

        public RuntimeMockStorage()
        {
            authors = new ObservableCollection<Author>() { author };
            songs = new ObservableCollection<Song>()
            {
                new Song() { Title = "lofi chill", Directory = dir, SongAuthor = author },
                new Song() { Title = "generic pop song title", Directory = dir, SongAuthor = author },
                new Song() { Title = "общее название популярной песни # 2", Directory = dir, SongAuthor = author}
            };
        }

        public ReadOnlyObservableCollection<Song> GetSongs()
        {
            return new ReadOnlyObservableCollection<Song>(songs);
        }

        public ReadOnlyObservableCollection<Author> GetAuthors()
        {
            return new ReadOnlyObservableCollection<Author>(authors);
        }

        public void AddAuthor(Author author)
        {
            authors.Add(author);
        }

        public void AddSong(Song song)
        {
            if (authors.Contains(song.SongAuthor))
            {
                throw new ArgumentException($"Song author {song.SongAuthor} is not among known authors");
            }

            songs.Add(song);
        }
    }
}