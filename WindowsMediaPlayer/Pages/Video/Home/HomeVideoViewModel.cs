using FirstFloor.ModernUI.Presentation;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using WindowsMediaPlayer.Serializer;

/*--------------------------------------------------------
 * HomeVideoViewModel.cs - file description
 * 
 * Version: 1.0
 * Author: Filipe
 * Created: 07/01/2015 16:38:56
 * 
 * Notes:
 * -------------------------------------------------------*/

namespace WindowsMediaPlayer.Pages.Video.Home
{
    public class HomeVideoViewModel : NotifyPropertyChanged
    {
        #region FIELDS

        private RelayCommand addVideoCommand;

        private LinkCollection videosList;

        #endregion

        #region PROPERTIES

        /// <summary>
        /// Gets or sets the VideoList links
        /// </summary>
        public LinkCollection VideosList
        {
            get
            {
                if (this.videosList == null)
                {
                    this.videosList = new LinkCollection();
                }
                return this.videosList;
            }
            set
            {
                this.videosList = value;
                this.OnPropertyChanged("VideosList");
            }
        }

        /// <summary>
        /// Add a new video
        /// </summary>
        public RelayCommand AddVideoCommand
        {
            get
            {
                if (this.addVideoCommand == null)
                {
                    this.addVideoCommand = new RelayCommand((param) => { this.AddVideoCommandAction(param); });
                }
                return this.addVideoCommand;
            }
            set
            {
                this.addVideoCommand = value;
                this.OnPropertyChanged("AddVideoCommand");
            }
        }

        #endregion

        #region CONSTRUCTORS

        public HomeVideoViewModel()
        {
            this.VideosList = new LinkCollection();
            this.LoadVideos();
        }

        #endregion

        #region METHODS

        /// <summary>
        /// Loads all videos from videos.xml
        /// </summary>
        private void LoadVideos()
        {
            List<String> _removedVideos = new List<String>();

            if (File.Exists(Constants.VIDEOS_FILE) == false)
            {
                return;
            }
            StreamReader _reader = new StreamReader(Constants.VIDEOS_FILE);
            VideoModel _videos = XmlSerializer.Deserialize<VideoModel>(_reader);

            _reader.Close();
            _reader.Dispose();

            if (_videos != null)
            {
                foreach (Video video in _videos.Videos)
                {
                    if (File.Exists(video.Path) == false)
                    {
                        _removedVideos.Add(video.Path);
                    }
                    else
                    {
                        this.AddFileToTab(video.Path);
                    }
                }
                foreach (String file in _removedVideos)
                {
                    Video _video = _videos.Videos.Find((vid => { return vid.Path == file; }));
                    _videos.Videos.Remove(_video);
                }
                if (_removedVideos.Count > 0)
                {
                    XmlSerializer.Serialize<VideoModel>(_videos, Constants.VIDEOS_FILE);
                }
            }
        }

        /// <summary>
        /// Save all videos to videos.xml
        /// </summary>
        private void SaveVideos()
        {
            VideoModel _videos = new VideoModel();

            this.VideosList.ToList().ForEach((link) =>
            {
                String _path = link.Source.OriginalString.Split(new Char[] { '#' }).Last();
                _videos.Videos.Add(new Video()
                    {
                        Path = _path,
                        Name = _path.Split('\\').Last()
                    });
            });
            XmlSerializer.Serialize<VideoModel>(_videos, Constants.VIDEOS_FILE);
        }

        /// <summary>
        /// Opens a FileDialog where the user can choose his videos
        /// </summary>
        /// <param name="param"></param>
        private void AddVideoCommandAction(Object param)
        {
            OpenFileDialog _openFile = new OpenFileDialog();

            _openFile.Filter = "Vidéos (*.AVI;*.MP4;*.DIVX;*.WMV)|*.AVI;*.MP4;*.DIVX;*.WMV";
            _openFile.Multiselect = true;

            if (_openFile.ShowDialog() == true)
            {
                foreach (String file in _openFile.FileNames)
                {
                    if (MediaPlayer.Instance.IsMedia(file) == true)
                    {
                        this.AddFileToTab(file);
                    }
                }
            }
            this.SaveVideos();
        }

        private void AddFileToTab(String path)
        {
            String _format = String.Format("/Pages/Video/Home/VideoPlayer.xaml#" + path);
            Link _link = new Link();
            _link.DisplayName = path.Split('\\').Last();
            _link.Source = new Uri(_format, UriKind.Relative);
            this.VideosList.Add(_link);
        }

        #endregion
    }
}
