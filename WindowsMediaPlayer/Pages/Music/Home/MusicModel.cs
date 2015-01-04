using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

/*--------------------------------------------------------
 * MusicModel.cs - file description
 * 
 * Version: 1.0
 * Author: Filipe
 * Created: 04/01/2015 12:01:20
 * 
 * Notes:
 * -------------------------------------------------------*/

namespace WindowsMediaPlayer.Pages.Music.Home
{
    public class MusicModel
    {
        #region FIELDS

        public String Title { get; set; }

        public String Artist { get; set; }

        public String Duration { get; set; }

        public String Album { get; set; }

        #endregion

        #region CONSTRUCTORS

        public MusicModel()
        {
        }

        #endregion

        #region METHODS
        #endregion
    }
}
