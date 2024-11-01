using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace MediaApp
{
    /// <summary>
    /// Interaction logic for ManageArtist.xaml
    /// </summary>
    public partial class ManageArtist : Window
    {
        public ManageArtist()
        {
            InitializeComponent();
        }

        private void CreateButton_Click(object sender, RoutedEventArgs e)
        {
            DetailArtist detailArtist = new(); 
            detailArtist.ShowDialog();
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
