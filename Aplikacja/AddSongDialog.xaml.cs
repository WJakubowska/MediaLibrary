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
using System.Globalization;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Aplikacja
{
    public class DirectoryIsSet : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            if (String.IsNullOrEmpty((string)value))
            {
                return new ValidationResult(false, "Directory not set");
            }

            return new ValidationResult(true, null);
        }
    }

    public class TitleIsSet : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            if (String.IsNullOrEmpty((string)value))
            {
                return new ValidationResult(false, "Title not set");
            }

            return new ValidationResult(true, null);
        }
    }

    /// <summary>
    /// Interaction logic for AddSongDialog.xaml
    /// </summary>
    public partial class AddSongDialog : Window, INotifyPropertyChanged
    {
        private Author author;
        private String songTitle = String.Empty;
        private String directory = String.Empty;
        private CollectionViewSource authorsViewSource;
        private MainWindow mainWindow;


        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public Author Author
        {
            get => this.author;

            set
            {
                if (value != this.author)
                {
                    this.author = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public String SongTitle
        {
            get => this.songTitle;

            set
            {
                if (value != this.songTitle)
                {
                    this.songTitle = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public String Directory
        {
            get => this.directory;

            set
            {
                if (value != this.directory)
                {
                    this.directory = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public AddSongDialog(MainWindow mainWindow)
        {
            InitializeComponent();

            this.mainWindow = mainWindow;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            authorsViewSource = (CollectionViewSource)FindResource(nameof(authorsViewSource));
            authorsViewSource.Source = mainWindow.AuthorsViewSource.Source;
        }

        private void choose_button_Click(object sender, RoutedEventArgs e)
        {
            var dlg = new Microsoft.Win32.OpenFileDialog();
            dlg.FileName = "Song";

            Nullable<bool> result = dlg.ShowDialog();

            if (result == true)
            {
                this.Directory = dlg.FileName;
            }
        }

        private void button_add_Click(object sender, RoutedEventArgs e)
        {
            if (MyValidator.IsValid(this))
            {
                this.DialogResult = true;
            }
        }

        private void button_add_author_Click(object sender, RoutedEventArgs e)
        {
            var dlg = new AddAuthorDialog(this.mainWindow);
            dlg.Owner = this;
            Nullable<bool> result = dlg.ShowDialog();

            if (result == true)
            {
                var new_author = new Author() { Name = dlg.AuthorName };
                mainWindow.Context.Authors.Add(new_author);
                combox_authors.SelectedItem = new_author;
            }
        }
    }
}
