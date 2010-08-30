namespace TwitterWall
{
    public partial class MainPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        private void searchButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            ((SearchViewModel) DataContext).Search();
        }
    }
}
