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
    /// <summary>
    /// Class checkes if directory is set
    /// </summary>
    public class DirectoryIsSet : ValidationRule
    {
        /// <summary>
        /// TU POWINIEN BYÆ OPIS 
        /// </summary>
        /// <param name="value"> TU POWINIEN BYÆ OPIS </param>
        /// <param name="cultureInfo">TU POWINIEN BYÆ OPIS </param>
        /// <returns>The result of the validation </returns>
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            if (String.IsNullOrEmpty((string)value))
            {
                return new ValidationResult(false, "Directory not set");
            }

            return new ValidationResult(true, null);
        }
    }

    /// <summary>
    /// Class checkes if title is set
    /// </summary>
    public class TitleIsSet : ValidationRule
    {
        /// <summary>
        /// TU POWINIEN BYÆ OPIS 
        /// </summary>
        /// <param name="value"> TU POWINIEN BYÆ OPIS </param>
        /// <param name="cultureInfo">TU POWINIEN BYÆ OPIS </param>
        /// <returns>The result of the validation </returns>
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

        /// <summary>
        /// TU POWINIEN BYÆ OPIS 
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        /// <summary>
        /// Gets or sets author
        /// </summary>
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

        /// <summary>
        /// Gets or sets song title 
        /// </summary>
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

        /// <summary>
        /// Gets or sets directory
        /// </summary>
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

        /// <summary>
        /// Method adds a new song dialog
        /// </summary>
        /// <param name="mainWindow">The main window of the application</param>
        public AddSongDialog(MainWindow mainWindow)
        {
            InitializeComponent();

            this.mainWindow = mainWindow;
        }

        /// <summary>
        /// Window loading method. 
        /// </summary>
        /// <param name="sender"> The source of the event. </param>
        /// <param name="e"> An object that contains no event data. </param>
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            authorsViewSource = (CollectionViewSource)FindResource(nameof(authorsViewSource));
            authorsViewSource.Source = mainWindow.AuthorsViewSource.Source;
        }

        /// <summary>
        /// If the "Choose..." button is pressed then the method is executed. 
        /// </summary>
        /// <param name="sender"> The source of the event. </param>
        /// <param name="e"> An object that contains no event data. </param>
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

        /// <summary>
        /// If the "Add" button is pressed then the method is executed. 
        /// </summary>
        /// <param name="sender"> The source of the event. </param>
        /// <param name="e"> An object that contains no event data. </param>
        private void button_add_Click(object sender, RoutedEventArgs e)
        {
            if (MyValidator.IsValid(this))
            {
                this.DialogResult = true;
            }
        }

        /// <summary>
        /// If the "Add" button is pressed then the method is executed. 
        /// </summary>
        /// <param name="sender"> The source of the event. </param>
        /// <param name="e"> An object that contains no event data. </param>
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
