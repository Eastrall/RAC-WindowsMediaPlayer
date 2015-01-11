using FirstFloor.ModernUI.Presentation;
using FirstFloor.ModernUI.Windows.Controls;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using WindowsMediaPlayer.Media;

/*--------------------------------------------------------
 * AddPlaylistViewModel.cs - file description
 * 
 * Version: 1.0
 * Author: Filipe
 * Created: 11/01/2015 12:03:45
 * 
 * Notes:
 * -------------------------------------------------------*/

namespace WindowsMediaPlayer.Pages.Music.Playlist.AddPlaylist
{
    public class AddPlaylistViewModel : NotifyPropertyChanged
    {
        #region FIELDS

        private MediaCollection<Music.Home.MusicModel> mediaMusicCollection;

        private String name;

        #endregion

        #region PROPERTIES

        /// <summary>
        /// Gets or sets the music collection
        /// </summary>
        public ObservableCollection<Music.Home.MusicModel> Musics
        {
            get
            {
                return this.mediaMusicCollection.Content;
            }
            set
            {
                this.mediaMusicCollection.Content = value;
                this.OnPropertyChanged("Musics");
            }
        }

        /// <summary>
        /// Gets or sets the playlist name
        /// </summary>
        public String Name
        {
            get
            {
                return this.name;
            }
            set
            {
                this.name = value;
                this.OnPropertyChanged("Name");
            }
        }

        #endregion

        #region CONSTRUCTORS

        /// <summary>
        /// Creates a new AddPlaylistViewModel instance
        /// </summary>
        public AddPlaylistViewModel()
        {
            this.mediaMusicCollection = new MediaCollection<Home.MusicModel>(Constants.MUSICS_FILE);
            this.mediaMusicCollection.Load();
        }

        #endregion

        #region METHODS

        /// <summary>
        /// Save the playlist
        /// </summary>
        public void SavePlaylist()
        {
            MediaCollection<PlaylistModel> _playlistCollection = new MediaCollection<PlaylistModel>(Constants.PLAYLISTS_FILE);
            List<String> _selectedMusicPaths = new List<String>();

            if (String.IsNullOrEmpty(this.Name) == true)
            {
                var _dlg = new ModernDialog
                {
                    Title = "Erreur",
                    Content = "Vous devez donner un nom à votre playlist"
                };
                _dlg.Buttons = new Button[] { _dlg.OkButton };
                _dlg.ShowDialog();
                return;
            }
            this.mediaMusicCollection.Content.ToList().ForEach((music) =>
            {
                if (music.Checked == true)
                {
                    _selectedMusicPaths.Add(music.Path);
                }
            });
            _playlistCollection.Load();
            if (_playlistCollection.Content.ToList().FirstOrDefault((playlist) => { return playlist.Name == this.Name; }) != null)
            {
                var _dlg = new ModernDialog
                {
                    Title = "Erreur",
                    Content = "Une playlist du même nom existe déjà. Veuillez changer le nom."
                };
                _dlg.Buttons = new Button[] { _dlg.OkButton };
                _dlg.ShowDialog();
                return;
            }
            _playlistCollection.Content.Add(new PlaylistModel()
                {
                    Name = this.Name,
                    Musics = _selectedMusicPaths
                });
            _playlistCollection.Save();
        }

        #endregion
    }
}
