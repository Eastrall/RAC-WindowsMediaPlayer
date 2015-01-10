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
    public class VideoModel
    {
        #region FIELDS
        #endregion

        #region PROPERTIES

        /// <summary>
        /// Gets or sets the Video name
        /// </summary>
        public String Name { get; set; }

        /// <summary>
        /// Gets or sets the video path
        /// </summary>
        public String Path { get; set; }

        #endregion

        #region CONSTRUCTORS

        /// <summary>
        /// Creates an instance of VideoModel
        /// </summary>
        public VideoModel()
        {
        }

        /// <summary>
        /// Creates an instance of VideoModel with video name and path
        /// </summary>
        /// <param name="name"></param>
        /// <param name="path"></param>
        public VideoModel(String name, String path)
        {
            this.Name = name;
            this.Path = path;
        }

        #endregion

        #region METHODS
        #endregion
    }
}
