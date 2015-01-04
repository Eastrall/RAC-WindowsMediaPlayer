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
 * PlaylistContentViewModel.cs - file description
 * 
 * Version: 1.0
 * Author: Filipe
 * Created: 03/01/2015 21:44:45
 * 
 * Notes:
 * -------------------------------------------------------*/

namespace WindowsMediaPlayer.Pages.Music.Playlist
{
    public class PlaylistContentViewModel : NotifyPropertyChanged
    {
        #region FIELDS

        private ObservableCollection<MusicModel> currentMusics;

        #endregion

        #region PROPERTIES

        public ObservableCollection<MusicModel> CurrentMusics
        {
            get
            {
                if (this.currentMusics == null)
                    this.currentMusics = new ObservableCollection<MusicModel>();
                return this.currentMusics;
            }
            set
            {
                this.currentMusics = value;
                this.OnPropertyChanged("CurrentMusics");
            }
        }

        #endregion

        #region CONSTRUCTORS

        /// <summary>
        /// Creates a new PlaylistContentViewModel instance
        /// </summary>
        public PlaylistContentViewModel()
        {
            this.CurrentMusics.Add(new MusicModel() { 
            Title = "fsdfsdf",
            Artist = "dsfdsfsd",
            Duration = "fsdfsdf",
            Album = "dfsdfsd",
            Path = "dfdf"
            });
        }

        #endregion

        #region METHODS

        //private void LoadPlaylist()
        //{
        //    if (File.Exists(Constants.MUSICS_FILE) == false)
        //    {
        //        return;
        //    }
        //    StreamReader _reader = new StreamReader(Constants.PLAYLISTS_FILE);
        //    List<PlaylistModel> _playlists;
        //    if ((_playlists = XmlSerializer.Deserialize<Dictionary<String, List<String>>>(_reader)) != null)
        //    {
        //        this.CurrentMusics.Clear();
        //        foreach (KeyValuePair<String, List<String>> playlist in this.Playlists)
        //        {
        //            // Mettre les onglets de playlist
        //        }
        //    }
        //}

        #endregion
    }
}
