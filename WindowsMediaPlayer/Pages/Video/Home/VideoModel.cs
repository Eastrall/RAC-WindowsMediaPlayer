using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

/*--------------------------------------------------------
 * VideoModel.cs - file description
 * 
 * Version: 1.0
 * Author: Filipe
 * Created: 09/01/2015 11:36:20
 * 
 * Notes:
 * -------------------------------------------------------*/

namespace WindowsMediaPlayer.Pages.Video.Home
{
    public struct Video
    {
        public String Name;
        public String Path;
    }

    public class VideoModel
    {
        #region FIELDS
        #endregion

        #region PROPERTIES

        public List<Video> Videos;

        #endregion

        #region CONSTRUCTORS

        public VideoModel()
        {
            this.Videos = new List<Video>();
        }

        #endregion

        #region METHODS
        #endregion
    }
}
