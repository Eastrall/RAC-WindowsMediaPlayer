using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

/*--------------------------------------------------------
 * Constants.cs - file description
 * 
 * Version: 1.0
 * Author: Filipe
 * Created: 03/01/2015 21:23:20
 * 
 * Notes:
 * -------------------------------------------------------*/

namespace WindowsMediaPlayer
{
    public class Constants
    {
        public const String CONFIGURATION_FILE = "Data\\config.xml";
        public const String MUSICS_FILE = "Data\\musics.xml";
        public const String PLAYLISTS_FILE = "Data\\playlists.xml";
        public const String VIDEOS_FILE = "Data\\videos.xml";

        public static readonly String[] MEDIA_EXTENSIONS = new String[] {
                ".PNG", ".JPG", ".JPEG", ".BMP", ".GIF", //etc
                ".WAV", ".MID", ".MIDI", ".WMA", ".MP3", ".OGG", ".RMA", //etc
                ".AVI", ".MP4", ".DIVX", ".WMV", //etc
            };

        public const String PLAY_ICON = "F1 M 30.0833,22.1667L 50.6665,37.6043L 50.6665,38.7918L 30.0833,53.8333L 30.0833,22.1667 Z";
        public const String PAUSE_ICON = "F1 M 26.9167,23.75L 33.25,23.75L 33.25,52.25L 26.9167,52.25L 26.9167,23.75 Z M 42.75,23.75L 49.0833,23.75L 49.0833,52.25L 42.75,52.25L 42.75,23.75 Z";

    }
}
