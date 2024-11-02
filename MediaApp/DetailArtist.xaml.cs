using MediaApp.BLL.Services;
using MediaApp.DAL.Entities;
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
    /// Interaction logic for DetailArtist.xaml
    /// </summary>
    public partial class DetailArtist : Window
    {
        private readonly ArtistService _service = new();
        public TbArtist _editArtist { get; set; }
        public DetailArtist()
        {
            InitializeComponent();
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Are You Sure ?", "Cancel", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if(result == MessageBoxResult.Yes)
            {
                this.Close();
            }
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            TbArtist artist = new () 
            {
                ArtistName = txtArtistName.Text,
                DataOfBirth = date.DisplayDate,
                Description = txtDescription.Text
            };
            _service.Create(artist);
            MessageBoxResult result = MessageBox.Show("Artist Created", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
            if (result == MessageBoxResult.OK)
            {
                txtArtistName.Text = "";
                date.DisplayDate = DateTime.Now;
                txtDescription.Text = "";
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            if(_editArtist != null)
            {
                txtArtistName.Text = _editArtist.ArtistName;
                date.Text = _editArtist.DataOfBirth.Date.ToString();
                txtDescription.Text = _editArtist.Description;
            }
        }
    }
}
