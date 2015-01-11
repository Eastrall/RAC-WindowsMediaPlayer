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
        #region STATIC

        public static Boolean NeedMusicUpdate = false;

        #endregion

        #region FIELDS

        private ObservableCollection<MusicModel> currentMusics;

        private MediaCollection<PlaylistModel> mediaCollection;

        private RelayCommand playMusicCommand;

        private RelayCommand deleteMusicCommand;

        private RelayCommand addPlaylistMusicCommand;

        private String source;

        private Int32 selectedIndex;

        #endregion

        #region PROPERTIES

        /// <summary>
        /// Currents music collection
        /// </summary>
        public ObservableCollection<MusicModel> CurrentMusics
        {
            get
            {
                if (this.currentMusics == null)
                {
                    this.currentMusics = new ObservableCollection<MusicModel>();
                }
                return this.currentMusics;
            }
            set
            {
                this.currentMusics = value;
                this.OnPropertyChanged("CurrentMusics");
            }
        }

        /// <summary>
        /// Play music command
        /// </summary>
        public RelayCommand PlayMusicCommand
        {
            get
            {
                if (this.playMusicCommand == null)
                {
                    this.playMusicCommand = new RelayCommand((param) => { this.PlayMusicCommandAction(param); });
                }
                return this.playMusicCommand;
            }
            set
            {
                this.playMusicCommand = value;
                this.OnPropertyChanged("PlayMusicCommand");
            }
        }

        /// <summary>
        /// Delete music command
        /// </summary>
        public RelayCommand DeleteMusicCommand
        {
            get
            {
                if (this.deleteMusicCommand == null)
                {
                    this.deleteMusicCommand = new RelayCommand((param) => { this.DeleteMusicCommandAction(param); });
                }
                return this.deleteMusicCommand;
            }
            set
            {
                this.deleteMusicCommand = value;
                this.OnPropertyChanged("DeleteMusicCommand");
            }
        }

        /// <summary>
        /// Add musics to playlist
        /// </summary>
        public RelayCommand AddPlaylistMusicCommand
        {
            get
            {
                if (this.addPlaylistMusicCommand == null)
                {
                    this.addPlaylistMusicCommand = new RelayCommand((param) => { this.AddPlaylistMusicCommandAction(param); });
                }
                return this.addPlaylistMusicCommand;
            }
            set
            {
                this.addPlaylistMusicCommand = value;
                this.OnPropertyChanged("AddPlaylistMusicCommand");
            }
        }

        /// <summary>
        /// Gets or sets the current music source
        /// </summary>
        public String Source
        {
            get
            {
                return this.source;
            }
            set
            {
                this.source = value;
                this.UpdatePlaylistContent();
            }
        }

        /// <summary>
        /// Gets or sets the current music index on the DataGrid
        /// </summary>
        public Int32 SelectedIndex
        {
            get
            {
                return this.selectedIndex;
            }
            set
            {
                this.selectedIndex = value;
                this.OnPropertyChanged("SelectedIndex");
            }
        }

        #endregion

        #region CONSTRUCTORS

        /// <summary>
        /// Creates a new PlaylistContentViewModel instance
        /// </summary>
        public PlaylistContentViewModel()
        {
            this.mediaCollection = new MediaCollection<PlaylistModel>(Constants.PLAYLISTS_FILE);

            // Initialize update timer
            DispatcherTimer _timer = new DispatcherTimer();
            _timer.Interval = TimeSpan.FromSeconds(0.1);
            _timer.Tick += TimerTick;
            _timer.Start();
        }

        #endregion

        #region METHODS

        /// <summary>
        /// Update playlist content
        /// </summary>
        private void UpdatePlaylistContent()
        {
            this.CurrentMusics.Clear();
            this.mediaCollection.Load();
            this.mediaCollection.Content.ToList().ForEach((playlist) =>
                {
                    if (playlist.Name == this.Source)
                    {
                        playlist.Musics.ForEach((musicPath) =>
                        {
                            this.CurrentMusics.Add(new MusicModel(musicPath));
                        });
                    }
                });
        }

        /// <summary>
        /// Play music action
        /// </summary>
        /// <param name="param"></param>
        private void PlayMusicCommandAction(Object param)
        {
            MusicModel _music = param as MusicModel;

            this.SelectedIndex = this.currentMusics.ToList().FindIndex((music) => { return music.Path == _music.Path; });
            MediaPlayer.Instance.CurrentPlaylist = this.Source;
            MediaPlayer.Instance.Audio.Load(_music.Path);
            MediaPlayer.Instance.Audio.Play();
        }

        /// <summary>
        /// Delete a music from the playlist
        /// </summary>
        /// <param name="param"></param>
        private void DeleteMusicCommandAction(Object param)
        {
            MusicModel _music = param as MusicModel;

            ModernDialog _dlg = new ModernDialog
            {
                Title = "Confirmation",
                Content = "Voulez-vous vraiment supprimer '" + _music.Title + "' de la playlist '" + this.Source + "' ?"
            };
            _dlg.Buttons = new Button[] { _dlg.YesButton, _dlg.NoButton };
            if (_dlg.ShowDialog() == true)
            {
                this.mediaCollection.Load();
                this.mediaCollection.Content.ToList().ForEach((playlist) =>
                {
                    if (playlist.Name == this.Source)
                    {
                        playlist.Musics.Remove(_music.Path);
                    }
                });
                this.mediaCollection.Save();
                this.UpdatePlaylistContent();
            }
        }

        /// <summary>
        /// Add a music to the playlist
        /// </summary>
        /// <param name="param"></param>
        private void AddPlaylistMusicCommandAction(Object param)
        {
            AddPlaylistMusics.AddPlaylistMusics _addMusicToPlayList = new AddPlaylistMusics.AddPlaylistMusics();
            if (_addMusicToPlayList.ShowDialog() == true)
            {
                List<String> _newMusics = _addMusicToPlayList.SelectedMusics;
                this.mediaCollection.Content.ToList().ForEach((playlist) =>
                {
                    if (playlist.Name == this.Source)
                    {
                        playlist.Musics.AddRange(_newMusics);
                    }
                });
                this.mediaCollection.Save();
                this.UpdatePlaylistContent();
            }
        }

        /// <summary>
        /// Update the datagrid cursor position
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void TimerTick(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(MediaPlayer.Instance.Audio.Source) == false && NeedMusicUpdate == true)
            {
                NeedMusicUpdate = false;
                this.SelectedIndex = this.CurrentMusics.ToList().FindIndex((music) => { return music.Path == MediaPlayer.Instance.Audio.Source; });
            }
        }

        #endregion
    }
}
