using GalaSoft.MvvmLight;
using System.Collections.ObjectModel;

namespace TwitterLib
{
    public class SearchViewModel : ViewModelBase
    {
        public ObservableCollection<Tweet> Tweets { get; private set; }
        
        public SearchViewModel()
        {
            Tweets = new ObservableCollection<Tweet>();
        }
    }
}