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

namespace Aplikacja
{
    public class VideoIsSelected : ValidationRule
    {
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
    /// Logika interakcji dla klasy YouTubeSearch.xaml
    /// </summary>
    public partial class YouTubeSearch : Window
    {


        private string searchTerm = "";
    

        public class Videos 
        {
            public string name { get; set; }
            public string linkYT { get; set; }
        }

        public Videos Video { get; set; }

        private ObservableCollection<Videos> videos;
 



        public YouTubeSearch()
        {
            InitializeComponent();
        }

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
                        Videos video = new Videos();
                        video.name = String.Format("{0}", searchResult.Snippet.Title);
                        video.linkYT = String.Format("https://www.youtube.com/watch?v={0}", searchResult.Id.VideoId);
                        videos.Add(video);
                        break;


                }
            }

            SearchButton.IsEnabled = true;
        }

        private void Add_Button_Click(object sender, RoutedEventArgs e)
        {
            if (MyValidator.IsValid(this))
            {
                this.DialogResult = true;
            }
        }
    }
}
