using MediaApp.BLL.Services;
using MediaApp.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using TagLib;

namespace MediaApp
{
    public partial class DetailSong : Window
    {
        private SongService _service = new();
        private ArtistService _artistService = new();
        private AlbumService _albumService = new();
        public TbSong _editSong { get; set; }
        public DetailSong()
        {
            InitializeComponent();
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult answer = System.Windows.MessageBox.Show("Are You Sure?", "Cancel", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (answer == MessageBoxResult.Yes) 
            { 
                this.Close(); 
            }  
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            TbSong song = new();
            song.SongName = txtSongName.Text;
            song.FilePath = txtFilePath.Text;
            var file = TagLib.File.Create(txtFilePath.Text);
            TimeSpan duration = file.Properties.Duration;
            song.Duration = (Decimal) duration.TotalSeconds;
            song.ArtistId = Convert.ToInt32(ArtistCombobox.SelectedValue.ToString());
            System.Windows.MessageBox.Show(ArtistCombobox.SelectedValue.ToString());
            if (AlbumCombobox.SelectedValue == null)
            {
                song.AlbumId = null;
            } else
            {
                song.AlbumId = Convert.ToInt32(AlbumCombobox.SelectedValue.ToString());
            }
            _service.Create(song);

            MessageBoxResult messageBoxResult = System.Windows.MessageBox.Show("Song has been saved", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
            if (messageBoxResult == MessageBoxResult.OK)
            {
                this.Close();
                
            }
        }

        private void ImportFileButton_Click(object sender, RoutedEventArgs e)
        {
            var openFileDialog = new OpenFileDialog
            {
                Multiselect = true,
                Filter = "All Files (*.*)|*.* | Video Files (*.mp4)|*.mp4 | Music Files (*.mp3)|*.mp3"
            };

            if(openFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                txtFilePath.Text = openFileDialog.FileName;
                try
                {
                    var file = TagLib.File.Create(txtFilePath.Text);
                    TimeSpan duration = file.Properties.Duration;
                    txtDuration.Text = convertTimeFormat(duration.TotalSeconds);
                }
                catch (Exception ex)
                {
                    System.Windows.MessageBox.Show("Lỗi khi đọc file: " + ex.Message);
                }
            }

        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            ArtistCombobox.ItemsSource = _artistService.GetAll();

            ArtistCombobox.DisplayMemberPath = "ArtistName";
            ArtistCombobox.SelectedValuePath = "ArtistId";

            AlbumCombobox.ItemsSource = _albumService.GetAllAlbums();
            AlbumCombobox.DisplayMemberPath = "Title";
            AlbumCombobox.SelectedValuePath = "AlbumId";

            if (_editSong != null)
            {
                txtSongName.Text = _editSong.SongName;
                txtDuration.Text = _editSong.Duration.ToString();
                txtFilePath.Text = _editSong.FilePath;
                ArtistCombobox.SelectedValue = _editSong.ArtistId;
                AlbumCombobox.SelectedValue = _editSong.AlbumId;
            }
        }
        private string convertTimeFormat(double value)
        {
            TimeSpan timeSpan = TimeSpan.FromSeconds(value);
            string timeFormated = string.Format("{0}:{1:D2}", (int)timeSpan.TotalMinutes, timeSpan.Seconds);
            return timeFormated;
        }
    }
}
