using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using WindowsMediaPlayer.Serializer;

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
        #endregion

        #region PROPERTIES

        public ObservableCollection<T> Content { get; set; }

        public String Path { get; private set; }

        #endregion

        #region CONSTRUCTORS

        /// <summary>
        /// Creates a new MediaCollection
        /// </summary>
        /// <param name="mediaCollectionPath">Specifies the mediaCollectionPath where the data will be stored</param>
        public MediaCollection(String mediaCollectionPath)
        {
            this.Path = mediaCollectionPath;
            this.Content = new ObservableCollection<T>();
        }

        #endregion

        #region METHODS

        public void Load()
        {
            if (File.Exists(this.Path) == false)
            {
                return;
            }
            StreamReader _reader = new StreamReader(this.Path);

            this.Content = XmlSerializer.Deserialize<ObservableCollection<T>>(_reader);
        }

        public void Save()
        {
            XmlSerializer.Serialize<ObservableCollection<T>>(this.Content, this.Path);
        }

        #endregion
    }
}
