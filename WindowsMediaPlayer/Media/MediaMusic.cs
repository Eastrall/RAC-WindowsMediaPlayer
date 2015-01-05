using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Timers;
using System.Windows.Threading;

/*--------------------------------------------------------
 * MediaMusic.cs - file description
 * 
 * Version: 1.0
 * Author: Filipe
 * Created: 03/01/2015 13:17:55
 * 
 * Notes:
 * -------------------------------------------------------*/

namespace WindowsMediaPlayer.Media
{
    public class MediaMusic
    {
        #region FIELDS

        private System.Windows.Media.MediaPlayer mediaPlayer;

        #endregion

        #region PROPERTIES

        /// <summary>
        /// Media Timer
        /// </summary>
        public DispatcherTimer Timer { get; set; }

        /// <summary>
        /// Gets the confirmation if there is any media element in the media player
        /// </summary>
        public Boolean HasMedia
        {
            get
            {
                return this.mediaPlayer.Source != null && this.mediaPlayer.HasAudio == true;
            }
        }

        /// <summary>
        /// Gets the music state
        /// </summary>
        public Boolean Ready { get; private set; }

        /// <summary>
        /// Check if the music is paused or not
        /// </summary>
        public Boolean InPause { get; private set; }

        /// <summary>
        /// Gets or sets the volume of the media player
        /// </summary>
        public Double Volume
        {
            get
            {
                return this.mediaPlayer.Volume;
            }
            set
            {
                this.mediaPlayer.Volume = value;
            }
        }

        /// <summary>
        /// Gets the total duration
        /// </summary>
        public TimeSpan TotalDuration
        {
            get
            {
                if (this.mediaPlayer.NaturalDuration.HasTimeSpan == true)
                {
                    return this.mediaPlayer.NaturalDuration.TimeSpan;
                }
                return new TimeSpan();
            }
        }

        /// <summary>
        /// Gets the current duration as TimeSpan
        /// </summary>
        public TimeSpan CurrentDuration
        {
            get
            {
                if (this.mediaPlayer.HasAudio == true)
                {
                    return this.mediaPlayer.Position;
                }
                return new TimeSpan();
            }
        }

        /// <summary>
        /// Gets or sets the current position of the media element
        /// </summary>
        public Int32 CurrentPosition
        {
            get
            {
                return Convert.ToInt32(this.mediaPlayer.Position.TotalSeconds);
            }
            set
            {
                this.mediaPlayer.Position = new TimeSpan(0, 0, value);
            }
        }

        /// <summary>
        /// Gets the seconds of the media element
        /// </summary>
        public Int32 TotalSeconds
        {
            get
            {
                return Convert.ToInt32(this.TotalDuration.TotalSeconds);
            }
        }

        #endregion

        #region CONSTRUCTORS

        /// <summary>
        /// Creates new a MediaMusic instance
        /// </summary>
        public MediaMusic()
        {
            this.mediaPlayer = new System.Windows.Media.MediaPlayer();
            this.mediaPlayer.MediaOpened += (sender, e) =>
            {
                this.Ready = true;
            };
            this.InPause = true;
        }

        #endregion

        #region METHODS

        /// <summary>
        /// Load an audio media
        /// </summary>
        /// <param name="path">Media audio path</param>
        public void Load(String path)
        {
            this.mediaPlayer.Open(new Uri(path));
            this.InPause = true;
        }

        /// <summary>
        /// Play the audio media
        /// </summary>
        public void Play()
        {
            if (this.InPause == true)
            {
                this.InPause = false;
                this.mediaPlayer.Play();
            }
        }

        /// <summary>
        /// Stop the audio media
        /// </summary>
        public void Stop()
        {
            if (this.InPause == false)
            {
                this.mediaPlayer.Stop();
                this.InPause = true;
            }
        }

        /// <summary>
        /// Pause the audio media
        /// </summary>
        public void Pause()
        {
            if (this.InPause == false)
            {
                this.mediaPlayer.Pause();
                this.InPause = true;
            }
        }

        #endregion
    }
}
