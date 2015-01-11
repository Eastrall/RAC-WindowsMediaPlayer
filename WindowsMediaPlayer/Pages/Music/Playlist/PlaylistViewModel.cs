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
using WindowsMediaPlayer.Media;
using System.Windows.Threading;

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

        private MediaCollection<PlaylistModel> mediaCollection;

        private RelayCommand addPlaylistCommand;

        private RelayCommand deletePlaylistCommand;

        private LinkCollection playlistLinks;

        #endregion

        #region PROPERTIES

        /// <summary>
        /// Add a new playlist
        /// </summary>
        public RelayCommand AddPlaylistCommand
        {
            get
            {
                if (this.addPlaylistCommand == null)
                {
                    this.addPlaylistCommand = new RelayCommand((param) => { this.AddPlaylistCommndAction(param); });
                }
                return this.addPlaylistCommand;
            }
        }

        /// <summary>
        /// Delete a playlist
        /// </summary>
        public RelayCommand DeletePlaylistCommand
        {
            get
            {
                if (this.deletePlaylistCommand == null)
                {
                    this.deletePlaylistCommand = new RelayCommand((param) => { this.DeletePlaylistCommandAction(param); });
                }
                return this.deletePlaylistCommand;
            }
            set
            {
                this.deletePlaylistCommand = value;
                this.OnPropertyChanged("DeletePlaylistCommand");
            }
        }

        /// <summary>
        /// Gets or sets the playlist tab links
        /// </summary>
        public LinkCollection PlaylistLinks
        {
            get
            {
                if (this.playlistLinks == null)
                {
                    this.playlistLinks = new LinkCollection();
                }
                return this.playlistLinks;
            }
            set
            {
                this.playlistLinks = value;
                this.OnPropertyChanged("PlaylistLinks");
            }
        }

        #endregion

        #region CONSTRUCTORS

        /// <summary>
        /// Creates a new PlaylistViewModel instance
        /// </summary>
        public PlaylistViewModel()
        {
            this.mediaCollection = new MediaCollection<PlaylistModel>(Constants.PLAYLISTS_FILE);
            this.UpdateLinks();
        }

        #endregion

        #region METHODS

        /// <summary>
        /// Add a new playlist
        /// </summary>
        /// <param name="param"></param>
        private void AddPlaylistCommndAction(Object param)
        {
            AddPlaylist.AddPlaylist a = new AddPlaylist.AddPlaylist();
            a.ShowDialog();
            this.UpdateLinks();
        }

        /// <summary>
        /// Delete a selected playlist
        /// </summary>
        /// <param name="param"></param>
        private void DeletePlaylistCommandAction(Object param)
        {
            String _playlistName = (param as Uri).OriginalString.Split('#').Last();
            PlaylistModel _playlist = this.mediaCollection.Content.ToList().Find((playlist) => { return playlist.Name == _playlistName; });
            Link _link = this.PlaylistLinks.ToList().Find((link) => { return link.DisplayName == _playlistName; });

            ModernDialog _dlg = new ModernDialog
            {
                Title = "Confirmation",
                Content = "Voulez-vous vraiment supprimer '" + _playlistName + "' ?"
            };

            _dlg.Buttons = new Button[] { _dlg.YesButton, _dlg.NoButton };
            if (_dlg.ShowDialog() == true)
            {
                if (_playlist != null)
                {
                    this.mediaCollection.Content.Remove(_playlist);
                    this.mediaCollection.Save();
                    this.PlaylistLinks.Remove(_link);
                }
            }
        }

        /// <summary>
        /// Update the tab links
        /// </summary>
        private void UpdateLinks()
        {
            this.PlaylistLinks.Clear();
            this.mediaCollection.Load();
            this.mediaCollection.Content.ToList().ForEach((playlist) =>
            {
                this.PlaylistLinks.Add(new Link()
                {
                    DisplayName = playlist.Name,
                    Source = new Uri("/Pages/Music/Playlist/PlaylistContent/PlaylistContent.xaml#" + playlist.Name, UriKind.Relative)
                });
            });
        }

        #endregion
    }
}
