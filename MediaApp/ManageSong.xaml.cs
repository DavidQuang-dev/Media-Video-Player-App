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
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace MediaApp
{
    /// <summary>
    /// Interaction logic for ManageSong.xaml
    /// </summary>
    public partial class ManageSong : Window
    {
        private SongService _service = new();
        public ManageSong()
        {
            InitializeComponent();
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void CreateButton_Click(object sender, RoutedEventArgs e)
        {
            DetailSong _detailSong = new();
            _detailSong.ShowDialog();
            FillData(_service.GetAll());
        }

        private void UpdateButton_Click(object sender, RoutedEventArgs e)
        {
            TbSong? selected = SongsDataGrid.SelectedItem as TbSong;
            if (selected != null)
            {
                DetailSong _detailSong = new();
                _detailSong.EditSong = selected;
                _detailSong.ShowDialog();
                FillData(_service.GetAll());
            }
            else
            {
                MessageBoxResult message = System.Windows.MessageBox.Show("Please select a song to update", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            TbSong? selected = SongsDataGrid.SelectedItem as TbSong;
            if (selected != null)
            {
                MessageBoxResult message = System.Windows.MessageBox.Show("Are You Sure ?", "Error", MessageBoxButton.YesNo, MessageBoxImage.Warning);
                if (message == MessageBoxResult.No)
                {
                    return;
                }
                _service.Delete(selected);
                FillData(_service.GetAll());
            }
            else
            {
                MessageBoxResult message = System.Windows.MessageBox.Show("Please select a song to delete", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            SongsDataGrid.ItemsSource = _service.GetAll();
        }

        private void FillData(List<TbSong> tbSongs)
        {
            SongsDataGrid.ItemsSource = null;
            SongsDataGrid.ItemsSource = tbSongs;
        }
    }
}
