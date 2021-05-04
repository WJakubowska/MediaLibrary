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
using System.Diagnostics;

namespace Aplikacja
{



    /// <summary>
    /// Class representing the main application window
    /// </summary>
    public partial class MainWindow : Window
    {
        /// <summary>
        /// Object for accessing the database
        /// </summary>
        private DatabaseService _service = new DatabaseService();

        /// <summary>
        /// Last focused list view
        /// </summary>
        private LastFocusedListView lastFocused = LastFocusedListView.None;

        /// <summary>
        /// CollectionViewSource bound to Authors list.
        /// </summary>
        public CollectionViewSource AuthorsViewSource { get; private set; }


        /// <summary>
        /// The class constructor.
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();
        }


        /// <summary>
        /// Close window callback.
        ///
        /// This method is responsible for disposing of database context.
        /// </summary>
        /// <param name="e">Closing event. </param>
        protected override void OnClosing(CancelEventArgs e)
        {
            _service.Dispose();
            base.OnClosing(e);
        }


        /// <summary>
        /// Callback for loaded window.
        /// </summary>
        /// <param name="sender"> The source of the event. </param>
        /// <param name="e"> An object that contains no event data. </param>
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            _service.PreloadEntities();
            AuthorsViewSource = (CollectionViewSource)FindResource(nameof(AuthorsViewSource));
            AuthorsViewSource.Source = _service.AuthorsAsObservable();
        }

        /// <summary>
        /// Method that processes result of AddSongDialog
        /// </summary>
        /// <param name="result"> Result of AddSongDialog.ShowDialog() call</param>
        /// <param name="dlg">AddSongDialog that was called</param>
        private void processAddSongDialogResult(Nullable<bool> result, AddSongDialog dlg)
        {
            if (result == true)
            {
                _service.AddSong(new Song() { Title = dlg.SongTitle, Directory = dlg.Directory, Author = dlg.Author });
                _service.Commit();
            }
        }

        /// <summary>
        /// Callback for the "Add" button
        /// </summary>
        /// <param name="sender"> The source of the event. </param>
        /// <param name="e"> An object that contains event data. </param>
        private void button_add_Click(object sender, RoutedEventArgs e)
        {
            var dlg = new AddSongDialog(this, _service);
            dlg.Owner = this;

            processAddSongDialogResult(dlg.ShowDialog(), dlg);
        }

        /// <summary>
        /// Callback for "YT Search" button
        /// </summary>
        /// <param name="sender"> The source of the event. </param>
        /// <param name="e"> An object that contains event data. </param>
        private void button_yt_Click(object sender, RoutedEventArgs e)
        {
            var youtubeDlg = new YouTubeSearch();
            youtubeDlg.Owner = this;
            Nullable<bool> result = youtubeDlg.ShowDialog();

            if (result == true)
            {
                var addSongDlg = new AddSongDialog(this, _service);
                addSongDlg.Owner = this;
                addSongDlg.SongTitle = youtubeDlg.Video.name;
                addSongDlg.Directory = youtubeDlg.Video.linkYT;

                processAddSongDialogResult(addSongDlg.ShowDialog(), addSongDlg);
            }
        }

        /// <summary>
        /// Callback for "Remove" button
        /// </summary>
        /// <param name="sender"> The source of the event. </param>
        /// <param name="e"> An object that contains event data. </param>
        private void button_remove_Click(object sender, RoutedEventArgs e)
        {
            if (lastFocused == LastFocusedListView.None)
            {
                return;
            }

            var lastFocusedListViewDelegate = LastFocusedListViewFactory.Create(lastFocused, this, _service);

            string messageBoxText = lastFocusedListViewDelegate.Message;
            string caption = "Aplikacja";
            MessageBoxButton button = MessageBoxButton.YesNo;
            MessageBoxImage icon = MessageBoxImage.Warning;

            var result = MessageBox.Show(messageBoxText, caption, button, icon);

            if (result == MessageBoxResult.Yes)
            {
                lastFocusedListViewDelegate.DeleteRecord();
                _service.Commit();
            }
        }


        /// <summary>
        ///  If the hyperlink is pressed then the method is executed.
        /// </summary>
        /// <param name="sender">  The source of the event. </param>
        /// <param name="e"> An object that contains no event data.</param>
        private void Hyperlink_OnRequestNavigate(object sender, RequestNavigateEventArgs e)
        {
            var urlPart = ((Hyperlink)sender).NavigateUri;
            var fullUrl = string.Format("{0}", urlPart);
            Process.Start(new ProcessStartInfo(fullUrl) { UseShellExecute = true });
            e.Handled = true;
        }


        /// <summary>
        /// Callback for focusing ListView of Authors
        /// </summary>
        /// <param name="sender">  The source of the event. </param>
        /// <param name="e"> An object that contains event data.</param>
        private void AuthorsListView_GotFocus(object sender, RoutedEventArgs e)
        {
            lastFocused = LastFocusedListView.Authors;
        }

        /// <summary>
        /// Callback for focusing ListView of Songs
        /// </summary>
        /// <param name="sender">  The source of the event. </param>
        /// <param name="e"> An object that contains no event data.</param>
        private void SongsListView_GotFocus(object sender, RoutedEventArgs e)
        {
            lastFocused = LastFocusedListView.Songs;
        }
    }
}
