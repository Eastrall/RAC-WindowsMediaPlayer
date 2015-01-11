using FirstFloor.ModernUI.Presentation;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using WindowsMediaPlayer.Media;

/*--------------------------------------------------------
 * AddPlaylistMusicViewModel.cs - file description
 * 
 * Version: 1.0
 * Author: Filipe
 * Created: 11/01/2015 14:54:22
 * 
 * Notes:
 * -------------------------------------------------------*/

namespace WindowsMediaPlayer.Pages.Music.Playlist.AddPlaylistMusics
{
    public class AddPlaylistMusicViewModel : NotifyPropertyChanged
    {
        #region FIELDS

        private MediaCollection<Music.Home.MusicModel> mediaMusicCollection;

        #endregion

        #region PROPERTIES

        /// <summary>
        /// Gets the selected musics
        /// </summary>
        public List<String> SelectedMusics { get; private set; }

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

        #endregion

        #region CONSTRUCTORS

        /// <summary>
        /// Creates a new AddPlaylistMusicViewModel instance
        /// </summary>
        public AddPlaylistMusicViewModel()
        {
            this.mediaMusicCollection = new MediaCollection<Home.MusicModel>(Constants.MUSICS_FILE);
            this.mediaMusicCollection.Load();
        }

        #endregion

        #region METHODS

        /// <summary>
        /// Generate selected music
        /// </summary>
        public void GenerateSelectedList()
        {
            this.SelectedMusics = new List<String>();
            this.mediaMusicCollection.Content.ToList().ForEach((music) =>
                {
                    if (music.Checked == true)
                    {
                        this.SelectedMusics.Add(music.Path);
                    }
                });
        }

        #endregion
    }
}
