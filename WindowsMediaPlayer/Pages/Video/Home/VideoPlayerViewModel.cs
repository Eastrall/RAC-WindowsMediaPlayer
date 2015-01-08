using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FirstFloor.ModernUI.Presentation;

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

        #endregion

        #region CONSTRUCTORS

        public VideoPlayerViewModel()
        {
            this.PlayPauseIcon = Constants.PLAY_ICON;
        }

        #endregion

        #region METHODS
        #endregion
    }
}
