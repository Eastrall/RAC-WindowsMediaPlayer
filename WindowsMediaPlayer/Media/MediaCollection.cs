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

        public delegate void MediaCollectionEventHandler(Object sender);

        #endregion

        #region PROPERTIES

        /// <summary>
        /// Gets or sets the MediaCollection<T> content
        /// </summary>
        public ObservableCollection<T> Content { get; set; }

        /// <summary>
        /// Gets the MediaCollection<T> path
        /// </summary>
        public String Path { get; private set; }

        public event MediaCollectionEventHandler OnLoaded;

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

        /// <summary>
        /// Load a MediaCollection<T> from the mediaCollectionPath
        /// </summary>
        public void Load()
        {
            if (File.Exists(this.Path) == false)
            {
                return;
            }
            using (StreamReader _reader = new StreamReader(this.Path))
            {
                this.Content = XmlSerializer.Deserialize<ObservableCollection<T>>(_reader);
            }
            if (this.OnLoaded != null)
            {
                this.OnLoaded(this);
            }
        }

        /// <summary>
        /// Save a MediaCollection<T> in the mediaCollectionPath
        /// </summary>
        public void Save()
        {
            XmlSerializer.Serialize<ObservableCollection<T>>(this.Content, this.Path);
        }

        #endregion
    }
}
