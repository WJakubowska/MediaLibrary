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
        private ObservableCollection<Author> authors;


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

        public AddSongDialog(DatebaseContext context)
        {
            InitializeComponent();

            this.authors = context.Authors.Local.ToObservableCollection();
            this.DataContext = this;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            authorsViewSource = (CollectionViewSource)FindResource(nameof(authorsViewSource));
            authorsViewSource.Source = authors;
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

        bool IsValid(DependencyObject node)
        {
            if (node != null)
            {
                if (Validation.GetHasError(node))
                {
                    if (node is IInputElement)
                    {
                        Keyboard.Focus((IInputElement) node);
                    }
                    return false;
                }
            }

            foreach (var subnode in LogicalTreeHelper.GetChildren(node))
            {
                if (subnode is DependencyObject)
                {
                    if (!IsValid((DependencyObject)subnode))
                    {
                        return false;
                    }
                }
            }

            return true;
        }

        private void button_add_Click(object sender, RoutedEventArgs e)
        {
            if (IsValid(this))
            {
                this.DialogResult = true;
            }
        }

        private void button_cancel_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
        }


    }
}
