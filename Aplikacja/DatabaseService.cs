using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Collections.ObjectModel;

namespace Aplikacja
{
    /// <summary>
    /// Object for accessing database context
    /// </summary>
    public class DatabaseService : IAuthorUniquenessChecker
    {
        private DatebaseContext _context;
        
        /// <summary>
        /// Public default ctor, initalizes database context
        /// </summary>
        public DatabaseService()
        {
            this._context = new DatebaseContext();
        }

        /// <summary>
        /// Implemetation of IAuthorUniquenessChecker interface
        /// </summary>
        /// <param name="authorName">Name that needs to be checked against db</param>
        /// <returns>Bool indicating if author name is already used in db</returns>
        public bool IsAuthorNameUnique(string authorName)
        {
            return this._context.Authors.Where(a => a.Name == authorName).Count() == 0;
        }

        /// <summary>
        /// Helper function for disposing of db context after application exit
        /// </summary>
        public void Dispose()
        {
            this._context.Dispose();
        }
        
        /// <summary>
        /// Helper functions that preloads entities for MVVVM
        /// </summary>
        public void PreloadEntities()
        {
            _context.Authors.Load();
            _context.Songs.Load();
        }
        
        /// <summary>
        /// Helper function returning Author collection as observable
        /// </summary>
        /// <returns>Observable Author collection</returns>
        public ObservableCollection<Author> AuthorsAsObservable()
        {
            return _context.Authors.Local.ToObservableCollection();
        }

        /// <summary>
        /// Helper function returning Song collection as observable
        /// </summary>
        /// <returns>Observable Song collection</returns>
        public ObservableCollection<Song> SongsAsObservable()
        {
            return _context.Songs.Local.ToObservableCollection();
        }

        /// <summary>
        /// Helper function that saves all changes to db
        /// </summary>
        public void Commit()
        {
            _context.SaveChanges();
        }

        /// <summary>
        /// Helper fucntion that adds song to a db
        /// </summary>
        /// <param name="song">Song to be added</param>
        public void AddSong(Song song)
        {
            _context.Songs.Add(song);
        }

        /// <summary>
        /// Helper fucntion that adds author to a db
        /// </summary>
        /// <param name="author">Author to be added</param>
        public void AddAuthor(Author author)
        {
            _context.Authors.Add(author);
        }

        /// <summary>
        /// Helper function that removes a Song from database
        /// </summary>
        /// <param name="song">Song to be removed</param>
        public void RemoveSong(Song song)
        {
            _context.Songs.Remove(song);
        }

        /// <summary>
        /// Helpers function that removes an Author from database
        /// </summary>
        /// <param name="author">Author to be removed</param>
        public void RemoveAuthor(Author author)
        {
            _context.Authors.Remove(author);
        }
    }
}
