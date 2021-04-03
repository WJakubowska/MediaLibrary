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
        private DatebaseContext _context = new DatebaseContextFactory().CreateDbContext(new string[]{"UseSqlite"});
        public CollectionViewSource authorsViewSource { get; private set; }

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
            authorsViewSource = (CollectionViewSource)FindResource(nameof(authorsViewSource));
            authorsViewSource.Source = _context.Authors.Local.ToObservableCollection();
        }

        private void button_add_Click(object sender, RoutedEventArgs e)
        {
            var dlg = new AddSongDialog(_context);
            dlg.Owner = this;
            Nullable<bool> result = dlg.ShowDialog();

            if (result == true)
            {
                _context.Songs.Add(new Song(){ Title = dlg.SongTitle, Directory = dlg.Directory, Author = dlg.Author });
                _context.SaveChanges();
            }
        }

        private void init_db()
        {
            var author1 = _context.Authors.Add(new Author() { Name = "John Doe" }).Entity;
            var author2 = _context.Authors.Add(new Author() { Name = "Yeah Yeah Yeahs"}).Entity;


            string[] titles = new string[]{"lofi chill", "generic pop song title", "общее название популярной песни # 2"};
            foreach (var title in titles)
            {
                _context.Songs.Add(new Song() {Title = title, Directory = "Z:/path/to/song", Author = author1});
            }

            _context.Songs.Add(new Song() {Title = "head will roll", Directory="ttps://www.youtube.com/watch?v=m9k7WgIPK14", Author=author2});

            _context.SaveChanges();
        }
    }
}
