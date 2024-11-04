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
        public TbArtist EditArtist { get; set; }
        public DetailArtist()
        {
            InitializeComponent();
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Are You Sure ?", "Cancel", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                this.Close();
            }
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            TbArtist artist = EditArtist ?? new();
            artist.ArtistName = txtArtistName.Text;
            artist.DataOfBirth = date.SelectedDate.HasValue ? date.SelectedDate.Value : DateTime.MinValue;
            artist.Description = txtDescription.Text;
            if (EditArtist == null)
            {
                _service.Create(artist);
            }
            else
            {
                _service.Update(artist);
            }
            MessageBox.Show("Artist has been saved", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
            this.Close();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            if (EditArtist != null)
            {
                txtArtistName.Text = EditArtist.ArtistName;
                date.Text = EditArtist.DataOfBirth.ToString();
                txtDescription.Text = EditArtist.Description;
                Header.Text = $"Update Artist {EditArtist.ArtistName}";
            }
        }
    }
}
