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
using System.Windows.Shapes;
using System.Collections.ObjectModel;
using Microsoft.EntityFrameworkCore;

namespace Aplikacja
{
    /// <summary>
    /// Interaction logic for AddSongDialog.xaml
    /// </summary>
    public partial class AddSongDialog : Window
    {
        public Author Author { get; set; }
        public String SongTitle { get; set; }
        public String Directory { get; set; }

        private CollectionViewSource authorsViewSource;
        private DatebaseContext _context;

        public AddSongDialog(DatebaseContext context)
        {
            InitializeComponent();

            _context = context;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            _context.Authors.Load();
            authorsViewSource = (CollectionViewSource)FindResource(nameof(authorsViewSource));
            authorsViewSource.Source = _context.Authors.Local.ToObservableCollection();
        }

        private void choose_button_Click(object sender, RoutedEventArgs e)
        {
            var dlg = new Microsoft.Win32.OpenFileDialog();
            dlg.FileName = "Song";

            Nullable<bool> result = dlg.ShowDialog();

            if (result == true)
            {
                this.Directory = dlg.FileName;
                textbox_song_directory.Text = dlg.FileName;
            }
        }

        private bool IsValid()
        {
            if (combox_authors.SelectedItem == null)
            {
                return false;
            }

            if (String.IsNullOrEmpty(textbox_song_name.Text))
            {
                return false;
            }

            if (String.IsNullOrEmpty(this.Directory))
            {
                return false;
            }

            return true;
        }

        private void button_add_Click(object sender, RoutedEventArgs e)
        {
            if (this.IsValid())
            {
                this.Author = (Author)combox_authors.SelectedItem;
                this.SongTitle = textbox_song_name.Text;

                this.DialogResult = true;
            }
        }

        private void button_cancel_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
        }


    }
}
