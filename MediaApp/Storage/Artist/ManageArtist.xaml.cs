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
    /// Interaction logic for ManageArtist.xaml
    /// </summary>
    public partial class ManageArtist : Window
    {
        private ArtistService _service = new ();
        private SongService songService = new();
        public ManageArtist()
        {
            InitializeComponent();
        }
        private void Border_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ButtonState == MouseButtonState.Pressed)
            {
                DragMove();
            }
        }
        private void CreateButton_Click(object sender, RoutedEventArgs e)
        {
            DetailArtist detailArtist = new(); 
            detailArtist.ShowDialog();
            FillData(_service.GetAll());
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            ArtistsDataGrid.ItemsSource = _service.GetAll();
        }

        private void UpdateButton_Click(object sender, RoutedEventArgs e)
        {
            TbArtist? selected = ArtistsDataGrid.SelectedItem as TbArtist;
            if (selected == null)
            {
                MessageBoxResult messageBoxResult = MessageBox.Show("Please select an artist", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                if (messageBoxResult == MessageBoxResult.OK)
                {
                    return;
                }
            }

            DetailArtist detailArtist = new();
            detailArtist.EditArtist = selected;
            detailArtist.ShowDialog();
            FillData(_service.GetAll());
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            TbArtist? selected = ArtistsDataGrid.SelectedItem as TbArtist;
            if (selected == null)
            {
                MessageBoxResult messageBoxResult = MessageBox.Show("Please select an artist", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                if(messageBoxResult == MessageBoxResult.OK)
                {
                    return;
                }
            }

            MessageBoxResult result = MessageBox.Show("Are You Sure ?", "Delete", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                _service.Delete(selected);
                FillData(_service.GetAll());
            }
        }

        private void FillData(List<TbArtist> tbArtists)
        {
            ArtistsDataGrid.ItemsSource = null;
            ArtistsDataGrid.ItemsSource = tbArtists;
        }
    }
}
