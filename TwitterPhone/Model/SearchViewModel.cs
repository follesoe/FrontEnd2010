using System.Collections.Generic;
using System.Collections.ObjectModel;
using GalaSoft.MvvmLight;

namespace TwitterPhone.Model
{
    public class SearchViewModel : ViewModelBase
    {        
        public ObservableCollection<Tweet> Tweets { get; private set; }
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
        }

        public void Search()
        {
            if(!string.IsNullOrEmpty(Keyword))
            {
                _search.Search(Keyword, AddTweets);
            }
        }

        private void AddTweets(List<Tweet> newTweets)
        {
            newTweets.ForEach(t => Tweets.Insert(0, t));
        }
    }
}