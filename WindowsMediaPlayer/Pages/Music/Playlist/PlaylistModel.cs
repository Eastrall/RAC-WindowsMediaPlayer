using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

/*--------------------------------------------------------
 * PlaylistModel.cs - file description
 * 
 * Version: 1.0
 * Author: Filipe
 * Created: 04/01/2015 12:01:20
 * 
 * Notes:
 * -------------------------------------------------------*/

namespace WindowsMediaPlayer.Pages.Music.Playlist
{
    public class PlaylistModel
    {
        #region FIELDS

        public String Name { get; set; }

        public List<String> Musics { get; set; }

        #endregion

        #region CONSTRUCTORS

        public PlaylistModel()
        {
        }

        #endregion

        #region METHODS
        #endregion
    }
}
