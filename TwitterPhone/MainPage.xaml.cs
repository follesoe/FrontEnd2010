using System;
using System.Windows;

namespace TwitterPhone
{
    public partial class MainPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        private void AddSearch(object sender, EventArgs e)
        {
            var uri = new Uri("/AddSearch.xaml", UriKind.Relative);
            NavigationService.Navigate(uri);            
        }    

        private void RemoveSearch(object sender, EventArgs e)
        {
            string message = "Are you sure you want to remove this search?";

            if(MessageBox.Show(message, "tittel", MessageBoxButton.OKCancel) == MessageBoxResult.OK)
            {
                
            }
        }
    }
}
