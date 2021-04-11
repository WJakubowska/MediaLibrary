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

        private DatebaseContext _context = new DatebaseContext();


        private LastFocusedListView lastFocused = LastFocusedListView.None;

        public CollectionViewSource AuthorsViewSource { get; private set; }
        public DatebaseContext Context { get => _context; }

        public MainWindow()
        {
            InitializeComponent();
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

        private void processAddSongDialogResult(Nullable<bool> result, AddSongDialog dlg)
        {
            if (result == true)
            {
                _context.Songs.Add(new Song() { Title = dlg.SongTitle, Directory = dlg.Directory, Author = dlg.Author });
                _context.SaveChanges();
            }
        }

        private void button_add_Click(object sender, RoutedEventArgs e)
        {
            var dlg = new AddSongDialog(this);
            dlg.Owner = this;

            processAddSongDialogResult(dlg.ShowDialog(), dlg);
        }

        private void button_yt_Click(object sender, RoutedEventArgs e)
        {
            var youtubeDlg = new YouTubeSearch();
            youtubeDlg.Owner = this;
            Nullable<bool> result = youtubeDlg.ShowDialog();

            if (result == true)
            {
                var addSongDlg = new AddSongDialog(this);
                addSongDlg.Owner = this;
                addSongDlg.SongTitle = youtubeDlg.Video.name;
                addSongDlg.Directory = youtubeDlg.Video.linkYT;

                processAddSongDialogResult(addSongDlg.ShowDialog(), addSongDlg);
            }
        }

        private void button_remove_Click(object sender, RoutedEventArgs e)
        {
            if (lastFocused == LastFocusedListView.None)
            {
                return;
            }

            var lastFocusedListViewDelegate = LastFocusedListViewFactory.Create(lastFocused, this);

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

        private void init_db()
        {
            var author1 = _context.Authors.Add(new Author() { Name = "John Doe" }).Entity;
            var author2 = _context.Authors.Add(new Author() { Name = "Yeah Yeah Yeahs" }).Entity;


            string[] titles = new string[] { "lofi chill", "generic pop song title", "общее название популярной песни # 2" };
            foreach (var title in titles)
            {
                _context.Songs.Add(new Song() { Title = title, Directory = "Z:/path/to/song", Author = author1 });
            }

            _context.Songs.Add(new Song() { Title = "head will roll", Directory = "ttps://www.youtube.com/watch?v=m9k7WgIPK14", Author = author2 });

            _context.SaveChanges();
        }
    }
}
