using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Data;
using System.Collections.ObjectModel;

namespace Aplikacja
{

    /// <summary>
    /// Enum signifying last focuse list view in the main window of an app.
    /// </summary>
    public enum LastFocusedListView
    {
        /// <summary>
        /// None of ListView
        /// </summary>
        None,
        /// <summary>
        /// ListView of Authors
        /// </summary>
        Authors,
        /// <summary>
        /// ListView of Songs
        /// </summary>
        Songs
    }



    /// <summary>
    /// Interface abstracting common processes related to
    /// removing Author or Song object from database.
    /// </summary>
    public interface ILastFocusedListViewDelegate
    {
        /// <summary>
        /// Message that is shown to user upon removing Song/Author.
        /// </summary>
        string Message { get; }
        /// <summary>
        /// Removes Song or Record from database.
        /// </summary>
        void DeleteRecord();
    }

    /// <summary>
    /// Class representing the last focused song.
    /// </summary>
    public class SongsLastFocused : ILastFocusedListViewDelegate
    {
        private MainWindow _mainWindow;
        private DatabaseService _service;


        /// <summary>
        /// Message shown to user.
        /// </summary>
        public string Message
        {
            get
            {
                var song = (Song)_mainWindow.SongsListView.SelectedItem;
                return $"Are you sure you want to delete song: {song.Title}";
            }
        }


        /// <summary>
        /// Ctor of SongsLastFocused.
        /// </summary>
        /// <param name="mainWindow"> The main window of the application  </param>
        /// <param name="service"> Object for accessing database </param>
        public SongsLastFocused(MainWindow mainWindow, DatabaseService service)
        {
            this._mainWindow = mainWindow;
            this._service = service;
        }

        /// <summary>
        /// Removes last focused song from database.
        /// </summary>
        public void DeleteRecord()
        {
            var song = (Song)_mainWindow.SongsListView.SelectedItem;
            _service.RemoveSong(song);
        }
    }


    /// <summary>
    /// Class representing the last focused author
    /// </summary>
    public class AuthorsLastFocused : ILastFocusedListViewDelegate
    {
        private MainWindow _mainWindow;
        private DatabaseService _service;

        /// <summary>
        /// Message shown to user.
        /// </summary>
        public string Message
        {
            get
            {
                var author = (Author)_mainWindow.AuthorsListView.SelectedItem;
                return $"Are you sure you want to delete author: {author.Name} + every song assigned to author?";
            }
        }

        /// <summary>
        /// Ctor of AuthorsLastFocused
        /// </summary>
        /// <param name="mainWindow"> The main window of the application  </param>
        public AuthorsLastFocused(MainWindow mainWindow, DatabaseService service)
        {
            this._mainWindow = mainWindow;
            this._service = service;
        }

        /// <summary>
        /// Removes an author from the database
        /// </summary>
        public void DeleteRecord()
        {
            var author = (Author)_mainWindow.AuthorsListView.SelectedItem;
            _service.RemoveAuthor(author);
        }
    }

    /// <summary>
    /// Factory for ILastFocusedListViewDelegate objects
    /// </summary>
    public class LastFocusedListViewFactory
    {
        /// <summary>
        /// The method creates a ILastFocusedListViewDelegate
        /// </summary>
        /// <param name="lastFocusedListView"> The last focused ListView </param>
        /// <param name="mainWindow"> The main window of the application </param>
        /// <returns> The listView focused </returns>
        public static ILastFocusedListViewDelegate Create(LastFocusedListView lastFocusedListView, MainWindow mainWindow, DatabaseService service)
        {
            switch (lastFocusedListView)
            {
                case LastFocusedListView.Authors:
                    return new AuthorsLastFocused(mainWindow, service);
                case LastFocusedListView.Songs:
                    return new SongsLastFocused(mainWindow, service);
                default:
                    break;
            }

            return null;
        }
    }

}
