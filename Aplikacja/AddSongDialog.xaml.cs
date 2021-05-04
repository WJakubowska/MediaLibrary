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
    /// Class validating directory passed by user
    /// </summary>
    public class DirectoryIsSet : ValidationRule
    {
        /// <summary>
        /// This validates directory string as passed by the user.
        /// </summary>
        /// <param name="value"> Directory string </param>
        /// <param name="cultureInfo"></param>
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
    /// Class validating title passed by user
    /// </summary>
    public class TitleIsSet : ValidationRule
    {
        /// <summary>
        /// This validates title string as passed by the user.
        /// </summary>
        /// <param name="value"> Title string </param>
        /// <param name="cultureInfo"></param>
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
        private DatabaseService service;

        /// <summary>
        /// Event emitted when property has changed
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
        /// <param name="service">The main window of the application</param>
        public AddSongDialog(MainWindow mainWindow, DatabaseService service)
        {
            InitializeComponent();

            this.mainWindow = mainWindow;
            this.service = service;
        }

        /// <summary>
        /// Method called upon complete load of a dialog.
        ///
        /// Links Authors from mainWindow into own CollectionViewSource.
        /// </summary>
        /// <param name="sender"> The source of the event. </param>
        /// <param name="e"> An object that contains event data. </param>
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            authorsViewSource = (CollectionViewSource)FindResource(nameof(authorsViewSource));
            authorsViewSource.Source = mainWindow.AuthorsViewSource.Source;
        }

        /// <summary>
        /// Directory dialog callback.
        /// </summary>
        /// <param name="sender"> The source of the event. </param>
        /// <param name="e"> An object that contains event data. </param>
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
        /// Add button callback.
        ///
        /// If all validations have passed, this ends the dialog.
        /// </summary>
        /// <param name="sender"> The source of the event. </param>
        /// <param name="e"> An object that contains event data. </param>
        private void button_add_Click(object sender, RoutedEventArgs e)
        {
            if (MyValidator.IsValid(this))
            {
                this.DialogResult = true;
            }
        }

        /// <summary>
        /// Add author button callback.
        ///
        /// This launches new dialog that can be used to add an Author to database.
        /// </summary>
        /// <param name="sender"> The source of the event. </param>
        /// <param name="e"> An object that contains event data. </param>
        private void button_add_author_Click(object sender, RoutedEventArgs e)
        {
            var dlg = new AddAuthorDialog(this.mainWindow, service);
            dlg.Owner = this;
            Nullable<bool> result = dlg.ShowDialog();

            if (result == true)
            {
                var new_author = new Author() { Name = dlg.AuthorName };
                service.AddAuthor(new_author);
                combox_authors.SelectedItem = new_author;
            }
        }
    }
}
