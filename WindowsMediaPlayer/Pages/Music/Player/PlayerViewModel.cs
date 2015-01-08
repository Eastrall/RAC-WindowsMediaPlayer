using FirstFloor.ModernUI.Presentation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Threading;

/*--------------------------------------------------------
 * PlayerViewModel.cs - file description
 * 
 * Version: 1.0
 * Author: Filipe
 * Created: 04/01/2015 17:18:02
 * 
 * Notes:
 * -------------------------------------------------------*/

namespace WindowsMediaPlayer.Pages.Music.Player
{
    public class PlayerViewModel : NotifyPropertyChanged
    {
        #region FIELDS

        private RelayCommand playPauseCommand;
        private RelayCommand rewindCommand;
        private RelayCommand forwardCommand;

        private String musicTotalDuration;
        private String musicCurrentDuration;
        private Int32 musicCurrentPosition;
        private Int32 musicTotalDurationSeconds;

        private String playPauseIcon;

        private Boolean changingMusicPosition;

        #endregion

        #region COMMANDS

        /// <summary>
        /// Play pause command handler
        /// </summary>
        public RelayCommand PlayPauseCommand
        {
            get
            {
                if (this.playPauseCommand == null)
                {
                    this.playPauseCommand = new RelayCommand((param) => { this.PlayPauseAction(param); });
                }
                return this.playPauseCommand;
            }
        }

        /// <summary>
        /// Rewind command handler
        /// </summary>
        public RelayCommand RewindCommand
        {
            get
            {
                if (this.rewindCommand == null)
                {
                    this.rewindCommand = new RelayCommand((param) => { });
                }
                return this.rewindCommand;
            }
        }

        /// <summary>
        /// Forward command handler
        /// </summary>
        public RelayCommand ForwardCommand
        {
            get
            {
                if (this.forwardCommand == null)
                {
                    this.forwardCommand = new RelayCommand((param) => { });
                }
                return this.forwardCommand;
            }
        }

        #endregion

        #region PROPERTIES

        /// <summary>
        /// Gets or set the play or pause icon
        /// </summary>
        public String PlayPauseIcon
        {
            get
            {
                return this.playPauseIcon;
            }
            set
            {
                this.playPauseIcon = value;
                this.OnPropertyChanged("PlayPauseIcon");
            }
        }

        /// <summary>
        /// Gets or sets the music total duration as a string
        /// </summary>
        public String MusicTotalDuration
        {
            get
            {
                return this.musicTotalDuration;
            }
            set
            {
                this.musicTotalDuration = value;
                this.OnPropertyChanged("MusicTotalDuration");
            }
        }

        /// <summary>
        /// Gets or sets the music current duration as a string
        /// </summary>
        public String MusicCurrentDuration
        {
            get
            {
                return this.musicCurrentDuration;
            }
            set
            {
                this.musicCurrentDuration = value;
                this.OnPropertyChanged("MusicCurrentDuration");
            }
        }

        /// <summary>
        /// Gets or sets the music current position on the slider
        /// </summary>
        public Double MusicCurrentPosition
        {
            get
            {
                return MediaPlayer.Instance.Audio.CurrentPosition;
            }
            set
            {
                if (this.changingMusicPosition == true)
                {
                    this.musicCurrentPosition = Convert.ToInt32(value);
                    this.changingMusicPosition = false;
                }
                else
                {
                    this.musicCurrentPosition = Convert.ToInt32(value);
                    MediaPlayer.Instance.Audio.CurrentPosition = Convert.ToInt32(value);
                }
                this.OnPropertyChanged("MusicCurrentPosition");
            }
        }

        /// <summary>
        /// Gets or sets the music total duration in seconds on the slider
        /// </summary>
        public Int32 MusicTotalDurationSeconds
        {
            get
            {
                return this.musicTotalDurationSeconds;
            }
            set
            {
                this.musicTotalDurationSeconds = value;
                this.OnPropertyChanged("MusicTotalDurationSeconds");
            }
        }

        /// <summary>
        /// Gets or sets the music volume
        /// </summary>
        public Double MusicVolume
        {
            get
            {
                return MediaPlayer.Instance.Audio.Volume;
            }
            set
            {
                MediaPlayer.Instance.Audio.Volume = value;
                this.OnPropertyChanged("MusicVolume");
            }
        }

        #endregion

        #region CONSTRUCTORS

        /// <summary>
        /// Creates a new PlayerViewModel instance
        /// </summary>
        public PlayerViewModel()
        {
            this.InitializeData();

            MediaPlayer.Instance.Audio.Timer = new DispatcherTimer();
            MediaPlayer.Instance.Audio.Timer.Interval = TimeSpan.FromSeconds(1);
            MediaPlayer.Instance.Audio.Timer.Tick += TimerTick;
            MediaPlayer.Instance.Audio.Timer.Start();
        }

        #endregion

        #region METHODS

        /// <summary>
        /// Play pause action
        /// </summary>
        /// <param name="sender"></param>
        private void PlayPauseAction(Object sender)
        {
            if (MediaPlayer.Instance.Audio.HasMedia == true)
            {
                if (MediaPlayer.Instance.Audio.InPause == true)
                {
                    MediaPlayer.Instance.Audio.Play();
                    this.PlayPauseIcon = Constants.PAUSE_ICON;
                }
                else
                {
                    MediaPlayer.Instance.Audio.Pause();
                    this.PlayPauseIcon = Constants.PLAY_ICON;
                }
            }
        }

        /// <summary>
        /// Timer function
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TimerTick(object sender, EventArgs e)
        {
            this.UpdateData();
        }

        /// <summary>
        /// Initialize player data
        /// </summary>
        private void InitializeData()
        {
            this.changingMusicPosition = true;
            this.PlayPauseIcon = Constants.PLAY_ICON;
            this.MusicTotalDuration = "0:00";
            this.MusicCurrentDuration = "0:00";
            this.MusicCurrentPosition = 0;
            this.MusicTotalDurationSeconds = 1;
            this.MusicVolume = MediaPlayer.Instance.Audio.Volume;
        }

        /// <summary>
        /// Update player data each seconds
        /// </summary>
        private void UpdateData()
        {
            if (MediaPlayer.Instance.Audio.HasMedia == true)
            {
                if (MediaPlayer.Instance.Audio.InPause == true)
                {
                    this.PlayPauseIcon = Constants.PLAY_ICON;
                }
                else
                {
                    this.MusicTotalDuration = MediaPlayer.Instance.Audio.TotalDuration.ToString(@"mm\:ss");
                    this.MusicCurrentDuration = MediaPlayer.Instance.Audio.CurrentDuration.ToString(@"mm\:ss");
                    this.MusicTotalDurationSeconds = MediaPlayer.Instance.Audio.TotalSeconds;

                    if (this.changingMusicPosition == false)
                    {
                        this.changingMusicPosition = true;
                        this.MusicCurrentPosition = MediaPlayer.Instance.Audio.CurrentPosition;
                    }
                    this.PlayPauseIcon = Constants.PAUSE_ICON;
                }
                
                if (this.MusicCurrentDuration == this.musicTotalDuration)
                {
                    this.PlayPauseIcon = Constants.PLAY_ICON;
                    this.NextSong();
                }
            }
            this.MusicVolume = MediaPlayer.Instance.Audio.Volume;
        }

        /// <summary>
        /// Next song
        /// </summary>
        private void NextSong()
        {
            // TODO: random 
        }

        #endregion
    }
}
