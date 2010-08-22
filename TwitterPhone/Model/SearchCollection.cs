using System.Collections.Generic;
using Phone.Controls.Samples;

namespace TwitterPhone.Model
{
    public class SearchCollection : PivotItemCollectionAdaptor<SearchViewModel>
    {
        public SearchCollection() : base(new List<SearchViewModel> {new SearchViewModel {Keyword = "FrontEnd2010"}})
        {
            Items[0].Search();
        }

        protected override SearchViewModel OnGetItem(PivotItem pivot)
        {
            var searchResult = (SearchResultView) pivot.Content;
            return (SearchViewModel)searchResult.DataContext;
        }

        protected override void OnSetItem(PivotItem pivot, SearchViewModel item)
        {
            var searchResult = (SearchResultView) pivot.Content;
            searchResult.DataContext = item;
        }

        protected override void OnCreateItem(PivotItem pivot, SearchViewModel item)
        {                        
            pivot.Title = "TWITTER SEARCH";
            pivot.Header = item.Keyword.ToLower();
            pivot.Content = new SearchResultView {DataContext = item};
        }
    }
}
