using System.Windows;
using TwitterPhone.Model;

namespace TwitterPhone
{
    public partial class AddSearch
    {
        public AddSearch()
        {
            InitializeComponent();
        }

        private void Add(object sender, RoutedEventArgs e)
        {
            if(_keyword.Text.Length > 0)
            {              
                NavigationService.GoBack();
                GalaSoft.MvvmLight.Messaging.Messenger.Default.Send(new AddSearchMessage { Keyword = _keyword.Text });
            }
            else
            {
                MessageBox.Show("You need to enter a search keyword", "Error", MessageBoxButton.OK);
            }
        }

        private void Cancel(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }
    }
}