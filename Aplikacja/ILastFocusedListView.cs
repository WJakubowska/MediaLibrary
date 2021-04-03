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
    public interface ILastFocusedListViewDelegate
    {
        string Message { get; }
        void DeleteRecord();
    }

    public class SongsLastFocused : ILastFocusedListViewDelegate
    {
        private MainWindow mainWindow;

        public string Message
        {
            get
            {
                var song = (Song)mainWindow.SongsListView.SelectedItem;
                return $"Are you sure you want to delete song: {song.Title}";
            }
        }

        public SongsLastFocused(MainWindow mainWindow)
        {
            this.mainWindow = mainWindow;
        }

        public void DeleteRecord()
        {
            var song = (Song)mainWindow.SongsListView.SelectedItem;
            mainWindow.Context.Songs.Remove(song);
        }
    }

    public class AuthorsLastFocused : ILastFocusedListViewDelegate
    {
        private MainWindow mainWindow;

        public string Message
        {
            get
            {
                var author = (Author)mainWindow.AuthorsListView.SelectedItem;
                return $"Are you sure you want to delete author: {author.Name} + every song assigned to author?";
            }
        }

        public AuthorsLastFocused(MainWindow mainWindow)
        {
            this.mainWindow = mainWindow;
        }   

        public void DeleteRecord()
        {
            var author = (Author)mainWindow.AuthorsListView.SelectedItem;
            mainWindow.Context.Authors.Remove(author);
        }
    }
}
