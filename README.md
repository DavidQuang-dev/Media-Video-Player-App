[![CI For Media-Video-Player-App](https://github.com/DavidQuang-dev/Media-Video-Player-App/actions/workflows/CI-Script.yml/badge.svg)](https://github.com/DavidQuang-dev/Media-Video-Player-App/actions/workflows/CI-Script.yml)
# Media Video Player App

Welcome to the **Media Video Player App**, a modern media player application built using WPF (Windows Presentation Foundation), integrated with Cloudinary for media storage and PostgreSQL for database management. This application provides basic functionalities of an online music and video player, allowing users to stream and manage their media files seamlessly.

## Features

- **Media Playback**: Play audio and video files with a user-friendly interface
- **Online Streaming**: Stream media files directly from Cloudinary
- **Media Management**: Add, delete, and organize media files in your library
- **User Authentication**: Secure user login and registration system
- **Playlist Creation**: Create and manage playlists for your favorite media
- **Search Functionality**: Easily search for media files by title, artist, or album
- **Responsive Design**: A clean and responsive UI designed for a smooth user experience

## Technologies Used

- **WPF (Windows Presentation Foundation)**: For building the desktop application with a rich user interface
- **Cloudinary**: For cloud-based media storage and streaming
- **PostgreSQL**: For database management, storing user data, media metadata, and playlists
- **Entity Framework**: For ORM (Object-Relational Mapping) to interact with the PostgreSQL database
- **C#**: The primary programming language used for the application logic

## Getting Started

### Prerequisites

Before you begin, ensure you have the following installed:

- [.NET SDK](https://dotnet.microsoft.com/download)
- [Visual Studio](https://visualstudio.microsoft.com/) (Recommended for WPF development)
- [PostgreSQL](https://supabase.com/))
- [Cloudinary Account](https://cloudinary.com/) (For media storage and streaming)

### Installation

1. **Clone the repository**:
   ```bash
   git clone https://github.com/DavidQuang-dev/media-video-player-app.git
   cd media-video-player-app
   ```

2. **Set up the database**:
   - Create a new PostgreSQL database on supabase
   - Update the connection string in `appsettings.json` with your database credentials

3. **Configure Cloudinary**:
   - Sign up for a Cloudinary account and obtain your API key, API secret, and cloud name
   - Update the Cloudinary settings in `appsettings.json` with your credentials

4. **Run the application**:
   - Open the solution in Visual Studio
   - Build and run the project

## Usage

1. **Login/Register**: Start by creating an account or logging in if you already have one
2. **Upload Media**: Upload your media files to Cloudinary through the app
3. **Play Media**: Browse your media library and play audio or video files
4. **Create Playlists**: Organize your media by creating custom playlists
5. **Search**: Use the search bar to quickly find specific media files

## Contributing

Contributions are welcome! If you'd like to contribute, please follow these steps:

1. Fork the repository
2. Create a new branch (`git checkout -b feature/YourFeatureName`)
3. Commit your changes (`git commit -m 'Add some feature'`)
4. Push to the branch (`git push origin feature/YourFeatureName`)
5. Open a pull request

## License

This project is licensed under the MIT License - see the LICENSE file for details.

## Acknowledgments

- Thanks to Cloudinary for providing an excellent media storage and streaming service
- Thanks to the WPF community for their continuous support and resources

## Contact

- David Quang- tranngvietquang04@gmail.com
- Project Link: [media-video-player-app](https://github.com/DavidQuang-dev/Media-Video-Player-App)
