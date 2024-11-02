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
    /// Interaction logic for AlbumManage.xaml
    /// </summary>
    public partial class AlbumManage : Window
    {
        private AlbumService _albumService = new();
        public AlbumManage()
        {
            InitializeComponent();
        }

        private void CreateButton_Click(object sender, RoutedEventArgs e)
        {
            AlbumDetail albumDetail = new AlbumDetail();
            albumDetail.ShowDialog();
            Helper(_albumService.GetAllAlbums());
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            AlbumDataGrid.ItemsSource = _albumService.GetAllAlbums();
        }

        private void UpdateButton_Click(object sender, RoutedEventArgs e)
        {
            if (AlbumDataGrid.SelectedItem == null)
            {
                MessageBox.Show("Vui lòng chọn album trước khi cập nhật");
                return;
            } 

            AlbumDetail albumDetail = new AlbumDetail();
            albumDetail.EditOne = AlbumDataGrid.SelectedItem as TbAlbum;
            albumDetail.ShowDialog();
            Helper(_albumService.GetAllAlbums());
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            var selectedAlbum = AlbumDataGrid.SelectedItem as TbAlbum;
            if (selectedAlbum == null)
                MessageBox.Show("Vui lòng chọn 1 album để xóa", "Quit");
            else
            {
                MessageBoxResult result = MessageBox.Show("Are you sure to delete album : " + selectedAlbum.Title, "Delete", MessageBoxButton.YesNo);
                if (result == MessageBoxResult.No)
                    return;
                else
                {
                    _albumService.DeleteAlbum(AlbumDataGrid.SelectedItem as TbAlbum);
                    Helper(_albumService.GetAllAlbums());
                }
            }
        }

        private void Helper(List<TbAlbum> arr)
        {
            if (arr != null)
            {
                AlbumDataGrid.ItemsSource = null;
                AlbumDataGrid.ItemsSource = arr;
            }
        }
    }
}
