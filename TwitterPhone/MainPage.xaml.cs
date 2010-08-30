using System;
using System.Collections.Specialized;
using System.Windows;
using System.Windows.Controls;
using Microsoft.Phone.Shell;
using TwitterPhone.Model;

namespace TwitterPhone
{
    public partial class MainPage
    {
        public static SearchCollection SearchItems;

        public MainPage()
        {
            InitializeComponent();

            SearchItems = new SearchCollection();

            _pivot.SelectionChanged += OnSelectionChanged;
            _pivot.ItemsSource = SearchItems;
        }

        private void AddSearch(object sender, EventArgs e)
        {
            var uri = new Uri("/AddSearch.xaml", UriKind.Relative);
            NavigationService.Navigate(uri);            
        }

        private void Refresh(object sender, EventArgs e)
        {
            SearchViewModel viewModel = SearchItems[_pivot.SelectedItem];
            viewModel.Search();
        }

        private void RemoveSearch(object sender, EventArgs e)
        {
            SearchViewModel viewModel = SearchItems[_pivot.SelectedItem];
            string title = viewModel.Keyword;
            string message = "Are you sure you want to remove this search?";

            if(MessageBox.Show(message, title, MessageBoxButton.OKCancel) == MessageBoxResult.OK)
            {
                SearchItems.Remove(viewModel);
            }
        }

        private void OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            bool firstItem = (_pivot.SelectedIndex == 0);

            ((ApplicationBarIconButton) ApplicationBar.Buttons[1]).IsEnabled = !firstItem;
        }
    }
}
