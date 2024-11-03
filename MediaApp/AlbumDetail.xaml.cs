
using MediaApp.BLL.Services;
using MediaApp.DAL.Entities;
using Microsoft.Win32;
using System.Windows;


namespace MediaApp
{
    /// <summary>
    /// Interaction logic for AlbumDetail.xaml
    /// </summary>
    public partial class AlbumDetail : Window
    {
        public TbAlbum EditOne { get; set; }
        private AlbumService _service = new();
        private SongService _songService = new();
        private ArtistService _artistService = new();
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
            if (result == MessageBoxResult.Yes)
            {
                this.Close();
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            //List<TbArtist> artists = new List<TbArtist>();
            //DateOnly dateOnly = new DateOnly(2004, 10, 26);
            //artists.Add(new TbArtist { ArtistId = 1, ArtistName = "Blackpro", DataOfBirth = dateOnly, Description = "Mô tả" });
            //artists.Add(new TbArtist { ArtistId = 2, ArtistName = "Toan", DataOfBirth = dateOnly, Description = "Mô tả 1" });
            //artists.Add(new TbArtist { ArtistId = 3, ArtistName = "Hehe", DataOfBirth = dateOnly, Description = "Mô tả 2" });

            //List<TbSong> songs = new List<TbSong>();
            //songs.Add(new TbSong { SongId = 1, SongName = "Bài ca tuổi trẻ", Duration = 230, FilePath = "File path 1" });
            //songs.Add(new TbSong { SongId = 2, SongName = "Bầu trời mới", Duration = 270, FilePath = "File path 1" });
            //songs.Add(new TbSong { SongId = 3, SongName = "Phải chăng em đã yêu", Duration = 210, FilePath = "File path 1" });
            ArtistComboBox.ItemsSource = _artistService.GetAll();
            ArtistComboBox.DisplayMemberPath = "ArtistName";
            ArtistComboBox.SelectedValuePath = "ArtistId";
            SongComboBox.ItemsSource = _songService.GetAll();
            SongComboBox.DisplayMemberPath = "SongName";
            SongComboBox.SelectedValuePath = "SongId";
            if (EditOne == null)
                TitleTextBlock.Text = "Add Album";
            else
            {
                TitleTextBlock.Text = "Edit Album";
                TitleTextBox.Text = EditOne.Title;
                FilePathTextBox.Text = EditOne.CoverImage;
                ArtistComboBox.SelectedValue = EditOne.ArtistId;
                //SongDataGrid.ItemsSource = _songService.GetSongsByAlbum(EditOne);
                foreach (TbSong song in _songService.GetSongsByAlbum(EditOne))
                {
                    SongDataGrid.Items.Add(song);
                }
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {

        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            TbAlbum tbAlbum = new();
            tbAlbum.Title = TitleTextBox.Text;
            tbAlbum.CoverImage = FilePathTextBox.Text;
            tbAlbum.ArtistId = int.Parse(ArtistComboBox.SelectedValue.ToString());

            if (EditOne == null)
            {
                _service.CreateAlbum(tbAlbum);
                MessageBox.Show($"Thêm album {TitleTextBox.Text} thành công !", "Add album", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                _service.UpdateAlbum(tbAlbum);
            }
            this.Close();
        }

        private void BrowseButton_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();

            if (openFileDialog.ShowDialog() == true)
            {
                FilePathTextBox.Text = openFileDialog.FileName;
            }
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            if (SongComboBox != null)
            {
                if (!SongDataGrid.Items.Contains((TbSong) SongComboBox.SelectedItem))
                    SongDataGrid.Items.Add(SongComboBox.SelectedItem);
            }
            else
            {
                MessageBox.Show("Vui lòng chọn bài hát trước khi thêm vào");
            }
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            if (SongDataGrid.SelectedItem == null)
                MessageBox.Show("Vui lòng chọn bài hát trước khi xóa !!");
            else
            {
                TbSong tbSong = SongDataGrid.SelectedItem as TbSong;
                tbSong.AlbumId = null;
                _songService.Update(tbSong);
                SongDataGrid.Items.Remove(SongDataGrid.SelectedItem);
                SongComboBox.ItemsSource = _songService.GetAllSongs();
            }

        }
    }
}
