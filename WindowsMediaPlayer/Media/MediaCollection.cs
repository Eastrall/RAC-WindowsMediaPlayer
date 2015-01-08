using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

/*--------------------------------------------------------
 * MediaCollection.cs - file description
 * 
 * Version: 1.0
 * Author: Filipe
 * Created: 08/01/2015 19:44:16
 * 
 * Notes:
 * -------------------------------------------------------*/

namespace WindowsMediaPlayer.Media
{
    public class MediaCollection<T>
    {
        #region FIELDS

        private List<T> listContent;

        #endregion

        #region PROPERTIES
        #endregion

        #region CONSTRUCTORS

        /// <summary>
        /// Creates a new MediaCollection
        /// </summary>
        public MediaCollection()
        {
            this.listContent = new List<T>();
        }

        #endregion

        #region METHODS
        #endregion
    }
}
