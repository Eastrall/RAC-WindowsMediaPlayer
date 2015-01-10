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

        public String Path { get; set; }

        #endregion

        #region CONSTRUCTORS

        /// <summary>
        /// Creates a new MusicModel instance
        /// </summary>
        public MusicModel()
        {
        }

        /// <summary>
        /// Creates a new MusicModel instance with a path
        /// </summary>
        /// <param name="path"></param>
        public MusicModel(String path)
        {
            TagLib.File fileinfos;

            fileinfos = TagLib.File.Create(path);
            this.Title = fileinfos.Tag.Title;
            if (String.IsNullOrEmpty(this.Title) == true)
            {
                this.Title = path.Split('\\').Last();
            }
            this.Artist = fileinfos.Tag.FirstAlbumArtist;
            if (String.IsNullOrEmpty(this.Artist) == true)
            {
                this.Artist = "Artiste inconnu";
            }
            this.Duration = fileinfos.Properties.Duration.ToString(@"mm\:ss");
            this.Album = fileinfos.Tag.Album;
            if (String.IsNullOrEmpty(this.Album) == true)
            {
                this.Album = "Album inconnu";
            }
            this.Path = path;
        }

        #endregion

        #region METHODS
        #endregion
    }
}
