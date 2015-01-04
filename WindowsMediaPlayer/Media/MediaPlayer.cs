using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WindowsMediaPlayer.Media;

/*--------------------------------------------------------
 * MediaPlayer.cs - file description
 * 
 * Version: 1.0
 * Author: Filipe
 * Created: 03/01/2015 20:57:48
 * 
 * Notes:
 * -------------------------------------------------------*/

namespace WindowsMediaPlayer
{
    public class MediaPlayer
    {
        #region SINGLETON

        /// <summary>
        /// Gets the MediaPlayer instance
        /// </summary>
        public static MediaPlayer Instance
        {
            get
            {
                lock (lockMedia)
                {
                    if (mediaPlayer == null)
                    {
                        mediaPlayer = new MediaPlayer();
                    }
                    return mediaPlayer;
                }
            }
        }

        private static MediaPlayer mediaPlayer = null;
        private static Object lockMedia = new Object();

        #endregion

        #region FIELDS

        /// <summary>
        /// Gets the MediaMusic instance
        /// </summary>
        public MediaMusic Audio { get; private set; }

        #endregion

        #region CONSTRUCTORS

        /// <summary>
        /// Creates a MediaPlayer instance
        /// </summary>
        public MediaPlayer()
        {
            this.Audio = new MediaMusic();
        }

        #endregion

        #region METHODS
        #endregion
    }
}
