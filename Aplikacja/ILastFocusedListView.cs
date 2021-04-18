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
    /// List of last focused ListView
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
    /// The ILastFacusedListViewDelegate interface
    /// </summary>
    public interface ILastFocusedListViewDelegate
    {
        /// <summary>
        /// Gets the message 
        /// </summary>
        string Message { get; }
        /// <summary>
        /// Remove a record from the database
        /// </summary>
        void DeleteRecord();
    }

    /// <summary>
    /// Class representing the last song focused
    /// </summary>
    public class SongsLastFocused : ILastFocusedListViewDelegate
    {
        private MainWindow mainWindow;


        /// <summary>
        /// Gets the message 
        /// </summary>
        public string Message
        {
            get
            {
                var song = (Song)mainWindow.SongsListView.SelectedItem;
                return $"Are you sure you want to delete song: {song.Title}";
            }
        }


        /// <summary>
        ///    TU POWINIEN BYĆ OPIS  
        /// </summary>
        /// <param name="mainWindow"> The main window of the application  </param>
        public SongsLastFocused(MainWindow mainWindow)
        {
            this.mainWindow = mainWindow;
        }

        /// <summary>
        /// Remove a song from the database
        /// </summary>
        public void DeleteRecord()
        {
            var song = (Song)mainWindow.SongsListView.SelectedItem;
            mainWindow.Context.Songs.Remove(song);
        }
    }


    /// <summary>
    /// Class representing the last author focused
    /// </summary>
    public class AuthorsLastFocused : ILastFocusedListViewDelegate
    {
        private MainWindow mainWindow;

        /// <summary>
        /// Gets the message 
        /// </summary>
        public string Message
        {
            get
            {
                var author = (Author)mainWindow.AuthorsListView.SelectedItem;
                return $"Are you sure you want to delete author: {author.Name} + every song assigned to author?";
            }
        }

        /// <summary>
        ///    TU POWINIEN BYĆ OPIS  
        /// </summary>
        /// <param name="mainWindow"> The main window of the application  </param>
        public AuthorsLastFocused(MainWindow mainWindow)
        {
            this.mainWindow = mainWindow;
        }

        /// <summary>
        /// Remove a author from the database
        /// </summary>
        public void DeleteRecord()
        {
            var author = (Author)mainWindow.AuthorsListView.SelectedItem;
            mainWindow.Context.Authors.Remove(author);
        }
    }

    /// <summary>
    ///  Class representing the factory of last facused ListView
    /// </summary> 
    public class LastFocusedListViewFactory
    {
        /// <summary>
        /// The method creates a ILastFocusedListViewDelegate
        /// </summary>
        /// <param name="lastFocusedListView"> The last focused ListView </param>
        /// <param name="mainWindow"> The main window of the application </param>
        /// <returns> The listView focused </returns>
        public static ILastFocusedListViewDelegate Create(LastFocusedListView lastFocusedListView, MainWindow mainWindow)
        {
            switch (lastFocusedListView)
            {
                case LastFocusedListView.Authors:
                    return new AuthorsLastFocused(mainWindow);
                case LastFocusedListView.Songs:
                    return new SongsLastFocused(mainWindow);
                default:
                    break;
            }

            return null;
        }
    }

}
