
using System.Windows;


namespace MediaApp
{
    /// <summary>
    /// Interaction logic for AlbumDetail.xaml
    /// </summary>
    public partial class AlbumDetail : Window
    {   
        public AlbumDetail()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Are you sure ?", "Cancel", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes) {
                this.Close();
            }
        }
    }
}
