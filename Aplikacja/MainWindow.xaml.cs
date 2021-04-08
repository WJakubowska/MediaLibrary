using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Collections.ObjectModel;
using System.ComponentModel;
using Microsoft.EntityFrameworkCore;

namespace Aplikacja
{


    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        enum LastFocusedListView
        {
            None,
            Authors,
            Songs
        }

        private DatebaseContext _context = new DatebaseContext();
 

        private LastFocusedListView lastFocused = LastFocusedListView.None;

        public CollectionViewSource AuthorsViewSource { get; private set; }
        public DatebaseContext Context { get => _context; }

        public MainWindow()
        {
            InitializeComponent();

            // Used for quick model changes without Migrations
            //_context.Database.EnsureDeleted();
            //_context.Database.EnsureCreated();
            //init_db();
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            _context.Dispose();
            base.OnClosing(e);
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            _context.Database.EnsureCreated();
            _context.Authors.Load();
            _context.Songs.Load();
            AuthorsViewSource = (CollectionViewSource)FindResource(nameof(AuthorsViewSource));
            AuthorsViewSource.Source = _context.Authors.Local.ToObservableCollection();
        }

        private void button_add_Click(object sender, RoutedEventArgs e)
        {
            var dlg = new AddSongDialog(this);
            dlg.Owner = this;
            Nullable<bool> result = dlg.ShowDialog();

            if (result == true)
            {
                _context.Songs.Add(new Song(){ Title = dlg.SongTitle, Directory = dlg.Directory, Author = dlg.Author });
                _context.SaveChanges();
            }
        }

        private void button_remove_Click(object sender, RoutedEventArgs e)
        {
            if (lastFocused == LastFocusedListView.None)
            {
                return;
            }
            
            var lastFocusedListViewDelegate = CreateListViewDelegate();

            string messageBoxText = lastFocusedListViewDelegate.Message;
            string caption = "Aplikacja";
            MessageBoxButton button = MessageBoxButton.YesNo;
            MessageBoxImage icon = MessageBoxImage.Warning;

            var result = MessageBox.Show(messageBoxText, caption, button, icon);

            if (result == MessageBoxResult.Yes)
            {
                lastFocusedListViewDelegate.DeleteRecord();
                _context.SaveChanges();
            }
        }

        private void AuthorsListView_GotFocus(object sender, RoutedEventArgs e)
        {
            lastFocused = LastFocusedListView.Authors;
        }

        private void SongsListView_GotFocus(object sender, RoutedEventArgs e)
        {
            lastFocused = LastFocusedListView.Songs;
        }



        private ILastFocusedListViewDelegate CreateListViewDelegate()
        {
            switch (lastFocused)
            {
                case LastFocusedListView.Authors:
                    return new AuthorsLastFocused(this);
                case LastFocusedListView.Songs:
                    return new SongsLastFocused(this);
                default:
                    break;
            }

            return null;
        }
    }
}
