using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FirstFloor.ModernUI.Presentation;
using System.Windows.Controls;

/*--------------------------------------------------------
 * VideoPlayerViewModel.cs - file description
 * 
 * Version: 1.0
 * Author: Filipe
 * Created: 08/01/2015 16:46:51
 * 
 * Notes:
 * -------------------------------------------------------*/

namespace WindowsMediaPlayer.Pages.Video.Home
{
    public class VideoPlayerViewModel : NotifyPropertyChanged
    {
        #region FIELDS

        private String playPauseIcon;
        private String source;

        private RelayCommand playPauseCommand;

        private Boolean playing;

        #endregion

        #region PROPERTIES

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

        public String Source
        {
            get
            {
                return source;
            }
            set
            {
                this.source = value;
                this.OnPropertyChanged("Source");
            }
        }

        public RelayCommand PlayPauseCommand
        {
            get
            {
                if (this.playPauseCommand == null)
                {
                    this.playPauseCommand = new RelayCommand((param) => { this.PlayPauseCommandAction(param); });
                }
                return this.playPauseCommand;
            }
            set
            {
                this.playPauseCommand = value;
                this.OnPropertyChanged("PlayPauseCommand");
            }
        }

        #endregion

        #region CONSTRUCTORS

        public VideoPlayerViewModel()
        {
            this.PlayPauseIcon = Constants.PAUSE_ICON;
            this.playing = true;
        }

        #endregion

        #region METHODS

        private void PlayPauseCommandAction(Object param)
        {
            MediaElement _mediaElement = param as MediaElement;

            if (this.playing == true)
            {
                _mediaElement.Pause();
                this.playing = false;
                this.PlayPauseIcon = Constants.PLAY_ICON;
            }
            else
            {
                _mediaElement.Play();
                this.playing = true;
                this.PlayPauseIcon = Constants.PAUSE_ICON;
            }
        }

        public void SetMediaElementState(MediaElement mediaElement)
        {

        }

        #endregion
    }
}
