using FirstFloor.ModernUI.Presentation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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

        #endregion

        #region PROPERTIES

        public String PlayPauseIcon
        {
            get
            {
                if (MediaPlayer.Instance.Audio.InPause == true)
                {
                    return "F1 M 30.0833,22.1667L 50.6665,37.6043L 50.6665,38.7918L 30.0833,53.8333L 30.0833,22.1667 Z";
                }
                return "F1 M 26.9167,23.75L 33.25,23.75L 33.25,52.25L 26.9167,52.25L 26.9167,23.75 Z M 42.75,23.75L 49.0833,23.75L 49.0833,52.25L 42.75,52.25L 42.75,23.75 Z";
            }
        }

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

        public String MusicTotalDuration
        {
            get
            {
                if (MediaPlayer.Instance.Audio.HasMedia == true)
                {
                    return MediaPlayer.Instance.Audio.TotalDuration.ToString(@"mm\:ss");
                }
                return "0:00";
            }
        }

        public String MusicCurrentDuration
        {
            get
            {
                if (MediaPlayer.Instance.Audio.HasMedia == true)
                {
                    return MediaPlayer.Instance.Audio.CurrentDuration.ToString(@"mm\:ss");
                }
                return "0:00";
            }
        }

        public Boolean CanChangeMusicPosition
        {
            get
            {
                return MediaPlayer.Instance.Audio.HasMedia;
            }
        }

        public Double MusicCurrentPosition
        {
            get
            {
                if (this.CanChangeMusicPosition == false)
                {
                    return 0;
                }
                return Convert.ToDouble(MediaPlayer.Instance.Audio.TotalSeconds);
            }
            set
            {
                MediaPlayer.Instance.Audio.CurrentPosition = Convert.ToInt32(value);
                this.OnPropertyChanged("MusicCurrentPosition");
            }
        }

        public Double MusicTotalPosition
        {
            get
            {
                if (this.CanChangeMusicPosition == false)
                {
                    return 0;
                }
                return Convert.ToDouble(MediaPlayer.Instance.Audio.TotalDuration.TotalSeconds);
            }
        }

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

        public PlayerViewModel()
        {
        }

        #endregion

        #region METHODS

        private void PlayPauseAction(Object sender)
        {
            if (MediaPlayer.Instance.Audio.InPause == true)
            {
                MediaPlayer.Instance.Audio.Play();
            }
            else
            {
                MediaPlayer.Instance.Audio.Pause();
            }
        }

        #endregion
    }
}
