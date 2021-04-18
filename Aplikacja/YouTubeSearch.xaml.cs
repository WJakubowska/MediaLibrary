using Google.Apis.Services;
using Google.Apis.YouTube.v3;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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
using System.Globalization;
using System.Diagnostics;
using System.Windows.Navigation;

namespace Aplikacja
{
    /// <summary>
    /// Class representing selecting a video
    /// </summary>
    public class VideoIsSelected : ValidationRule
    {
        /// <summary>
        /// TU POWINIEN BYĆ OPIS 
        /// </summary>
        /// <param name="value"> TU POWINIEN BYĆ OPIS </param>
        /// <param name="cultureInfo">TU POWINIEN BYĆ OPIS </param>
        /// <returns>The result of the validation </returns>
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            if (value == null)
            {
                return new ValidationResult(false, "Must select");
            }

            return ValidationResult.ValidResult;
        }
    }

    /// <summary>
    /// The class responsible for connecting to the API YouTube
    /// </summary>
    public partial class YouTubeSearch : Window
    {


        private string searchTerm = "";


        /// <summary>
        /// The class representing a video in YouTube
        /// </summary>
        public class Videos 
        {
            /// <summary>
            /// Gets or sets the name of the video
            /// </summary>
            public string name { get; set; }
            /// <summary>
            ///  Gets or sets the name of the video
            /// </summary>
            public string linkYT { get; set; }
        }

        /// <summary>
        /// An object representing a video
        /// </summary>
        public Videos Video { get; set; }

        /// <summary>
        /// An object representing a list of video as ObservableCollection 
        /// </summary>
        private ObservableCollection<Videos> videos;


        /// <summary>
        /// The class constructor.
        /// </summary>
        public YouTubeSearch()
        {
            InitializeComponent();
        }


        /// <summary>
        /// If the "Search" button is pressed then the method is executed. The method starts search term on YouTube.
        /// </summary>
        /// <param name="sender"> The source of the event. </param>
        /// <param name="e"> An object that contains no event data. </param>
        private void button_search_Click(object sender, RoutedEventArgs e)
        {

            searchTerm = Term.Text;
            if (String.IsNullOrEmpty(Term.Text))
            {
                MessageBox.Show("This field cannot be empty");
            }
            else
            {
                try
                {
                    Run(searchTerm);
                }
                catch (AggregateException ex)
                {
                    foreach (var w in ex.InnerExceptions)
                    {
                        MessageBox.Show("Error: " + w.Message);
                    }


                }
            }
        }

        /// <summary>
        /// The method responsible for search term in YouTube.
        /// </summary>
        /// <param name="term"> The term we want to find in youtube  </param>
        /// <returns> List of found videos  </returns>
        [STAThread]
        
        public async Task Run(string term)
        {
            var youtubeService = new YouTubeService(new BaseClientService.Initializer()
            {
                ApiKey = "AIzaSyC0L685Ii4bJmLG_cZjsm8w9RTOZlAfQm8",
                ApplicationName = this.GetType().ToString()
            });

            SearchButton.IsEnabled = false;

            var searchListRequest = youtubeService.Search.List("snippet");
            searchListRequest.Q = term; 
            searchListRequest.MaxResults = 10;

            // Call the search.list method to retrieve results matching the specified query term.
            var searchListResponse = await searchListRequest.ExecuteAsync();

            videos = new ObservableCollection<Videos>();
            YTvideo.ItemsSource = videos;

            // Add each result to the appropriate list, and then display the lists of
            // matching videos, channels, and playlists.
            foreach (var searchResult in searchListResponse.Items)
            {
                switch (searchResult.Id.Kind)
                {
                    case "youtube#video":
                        Videos video = new()
                        {
                            name = String.Format("{0}", searchResult.Snippet.Title),
                            linkYT = String.Format("http://www.youtube.com/watch?v={0}", searchResult.Id.VideoId)
                        };
                        videos.Add(video);
                        break;


                }
            }

            SearchButton.IsEnabled = true;
        }

        /// <summary>
        /// If the "Add" button is pressed then the method is executed.
        /// </summary>
        /// <param name="sender"> The source of the event. </param>
        /// <param name="e"> An object that contains no event data. </param>
        private void Add_Button_Click(object sender, RoutedEventArgs e)
        {
            if (MyValidator.IsValid(this))
            {
                this.DialogResult = true;
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



    }
}
