using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;

namespace TwitterWall
{
    public class SearchViewModel : ViewModelBase
    {        
        public ObservableCollection<Tweet> Tweets { get; private set; }
        public RelayCommand SearchCommand { get; private set; }
        private readonly TwitterSearch _search;
        private string _keyword;
        
        public string Keyword
        {
            get { return _keyword; }
            set
            {
                _keyword = value; 
                RaisePropertyChanged("Keyword");
            }

        }        
        
        public SearchViewModel()
        {
            _search = new TwitterSearch();
            Tweets = new ObservableCollection<Tweet>();
            SearchCommand = new RelayCommand(Search);            
        }

        public void Search()
        {
            if(!string.IsNullOrEmpty(Keyword))
            {
                _search.Search(Keyword, AddTweets);
            }
        }

        private int counter = 0;

        public void AddRandom()
        {
            counter++;
            var t = new Tweet();
            t.Text = "Hello World! " + counter;
            Tweets.Insert(0, t);
        }

        private void AddTweets(List<Tweet> newTweets)
        {
            Deployment.Current.Dispatcher.BeginInvoke(() => newTweets.ForEach(t => Tweets.Insert(0, t)));
        }
    }
}