using System;
using System.Windows;
using System.Windows.Controls;
using Microsoft.Phone.Controls;
using TwitterPhone.Model;

namespace TwitterPhone
{
    public partial class MainPage
    {
        public SearchViewModel SelectedSearch
        {
            get
            {
                var pivotItem = (PivotItem)_searchPivot.SelectedItem;
                var view = (SearchResultView)pivotItem.Content;
                return (SearchViewModel)view.DataContext;
            }
        }

        public MainPage()
        {
            InitializeComponent();

            Loaded += OnLoaded;
            _searchPivot.SelectionChanged += OnSelectionChanged;

            GalaSoft.MvvmLight.Messaging.Messenger.Default.Register<AddSearchMessage>(this, OnAddNewSearch);
        }

        private void OnAddNewSearch(AddSearchMessage message)
        {
            var view = new SearchResultView();
            ((SearchViewModel) view.DataContext).Keyword = message.Keyword;
            ((SearchViewModel) view.DataContext).Search();

            var pivotItem = new PivotItem();
            pivotItem.Header = message.Keyword;
            pivotItem.Content = view;
            _searchPivot.Items.Add(pivotItem);
        }

        private void OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(_remove != null)
                _remove.IsEnabled = (_searchPivot.SelectedItem != _nnugSearch);
        }

        private void OnLoaded(object sender, RoutedEventArgs e)
        {
            var vm = (SearchViewModel) _nnugSearch.DataContext;
            vm.Keyword = "nnug";
            vm.Search();
        }

        private void RefreshSearch(object sender, EventArgs e)
        {
            SelectedSearch.Search();
        }

        private void AddSearch(object sender, EventArgs e)
        {
            var uri = new Uri("/AddSearch.xaml", UriKind.Relative);
            NavigationService.Navigate(uri);            
        }    

        private void RemoveSearch(object sender, EventArgs e)
        {
            string message = "Are you sure you want to remove this search?";

            if(MessageBox.Show(message, SelectedSearch.Keyword, MessageBoxButton.OKCancel) == MessageBoxResult.OK)
            {
                _searchPivot.Items.Remove(_searchPivot.SelectedItem);
            }
        }
    }
}
