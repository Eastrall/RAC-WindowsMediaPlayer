using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FirstFloor.ModernUI.Presentation;
using System.Collections.ObjectModel;
using Microsoft.Win32;
using System.IO;
using WindowsMediaPlayer.Serializer;
using FirstFloor.ModernUI.Windows.Controls;
using System.Windows.Controls;
using WindowsMediaPlayer.Pages.Music.Home;

/*--------------------------------------------------------
 * PlaylistViewModel.cs - file description
 * 
 * Version: 1.0
 * Author: Filipe
 * Created: 03/01/2015 21:44:45
 * 
 * Notes:
 * -------------------------------------------------------*/

namespace WindowsMediaPlayer.Pages.Music.Playlist
{
    public class PlaylistViewModel : NotifyPropertyChanged
    {
        #region FIELDS

        private List<PlaylistModel> playlists;

        private RelayCommand addPlaylistButton;

        #endregion

        #region PROPERTIES

        private List<PlaylistModel> Playlists
        {
            get
            {
                if (this.playlists == null)
                    this.playlists = new List<PlaylistModel>();
                return this.playlists;
            }
            set
            {
                this.playlists = value;
            }
        }

        public RelayCommand AddPlaylistButton
        {
            get
            {
                if (this.addPlaylistButton == null)
                    this.addPlaylistButton = new RelayCommand((sender) => { this.onAddPlaylist(sender); });
                return this.addPlaylistButton;
            }
        }

        #endregion

        #region CONSTRUCTORS

        /// <summary>
        /// Creates a new PlaylistViewModel instance
        /// </summary>
        public PlaylistViewModel()
        {
        }

        #endregion

        #region METHODS

        void    onAddPlaylist(Object sender)
        {
        }

        private void SavePlaylists()
        {
            XmlSerializer.Serialize<List<String>>(this.Playlists, Constants.PLAYLISTS_FILE);
        }

        private void LoadPlaylists()
        {
            if (File.Exists(Constants.MUSICS_FILE) == false)
            {
                return;
            }
            StreamReader _reader = new StreamReader(Constants.PLAYLISTS_FILE);
            if ((this.Playlists = XmlSerializer.Deserialize<List<PlaylistModel>>(_reader)) != null)
            {
                foreach (PlaylistModel playlist in this.Playlists)
                {
                    // Mettre les onglets de playlist
                }
            }
        }

        #endregion
    }
}
