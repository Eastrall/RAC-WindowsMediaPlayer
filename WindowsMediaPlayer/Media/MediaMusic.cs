using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

/*--------------------------------------------------------
 * MediaMusic.cs - file description
 * 
 * Version: 1.0
 * Author: Filipe
 * Created: 03/01/2015 13:17:55
 * 
 * Notes:
 * -------------------------------------------------------*/

namespace WindowsMediaPlayer.MediaPlayer
{
    public class MediaMusic
    {
        #region FIELDS

        private System.Windows.Media.MediaPlayer mediaPlayer;

        #endregion

        #region PROPERTIES

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
        }

        /// <summary>
        /// Play the audio media
        /// </summary>
        public void Play()
        {
            if (this.HasMedia == true)
            {
                this.mediaPlayer.Play();
            }
        }

        /// <summary>
        /// Stop the audio media
        /// </summary>
        public void Stop()
        {
            if (this.HasMedia == true)
            {
                this.mediaPlayer.Stop();
            }
        }

        /// <summary>
        /// Pause the audio media
        /// </summary>
        public void Pause()
        {
            if (this.HasMedia == true)
            {
                this.mediaPlayer.Pause();
            }
        }

        #endregion
    }
}
