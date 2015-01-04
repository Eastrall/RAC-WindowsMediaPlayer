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

        public MusicModel()
        {
        }

        public MusicModel(String path)
        {
            TagLib.File fileinfos;

            fileinfos = TagLib.File.Create(path);
            this.Title = fileinfos.Tag.Title;
            this.Artist = fileinfos.Tag.FirstAlbumArtist;
            this.Duration = fileinfos.Properties.Duration.ToString(@"mm\:ss");
            this.Album = fileinfos.Tag.Album;
        }

        #endregion

        #region METHODS
        #endregion
    }
}
