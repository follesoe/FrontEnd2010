using System.Collections.Generic;
using TwitterLib;

namespace TwitterWall
{
    public partial class MainPage
    {
        public MainPage()
        {
            InitializeComponent();

            var twitterSearch = new TwitterSearch();
            twitterSearch.Search("frontend2010", Done);
        }

        private void Done(List<Tweet> tweets)
        {
            
        }
    }
}
