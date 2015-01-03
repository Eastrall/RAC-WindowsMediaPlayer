using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Media;

/*--------------------------------------------------------
 * AppearanceModel.cs - file description
 * 
 * Version: 1.0
 * Author: Filipe
 * Created: 03/01/2015 21:13:03
 * 
 * Notes:
 * -------------------------------------------------------*/

namespace WindowsMediaPlayer.Pages.Settings
{
    public class AppearanceModel
    {
        #region FIELDS

        public Color AmbiantColor { get; set; }

        public String Theme { get; set; }

        public String FontSize { get; set; }

        #endregion

        #region CONSTRUCTORS

        /// <summary>
        /// Creates a new AppearanceModel
        /// </summary>
        public AppearanceModel()
        {
        }

        #endregion

        #region METHODS

        #endregion
    }
}
