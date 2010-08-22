using System.Windows.Controls;
using TwitterPhone.Model;

namespace TwitterPhone
{
    public partial class SearchResultView
    {
        public SearchViewModel ViewModel
        {
            get { return (SearchViewModel) DataContext; }
            set { DataContext = value; }
        }

        public SearchResultView()
        {
            InitializeComponent();
        }
    }
}
