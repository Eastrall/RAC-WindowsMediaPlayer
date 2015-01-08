using FirstFloor.ModernUI.Presentation;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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
        }

        /// <summary>
        /// Save all videos to videos.xml
        /// </summary>
        private void SaveVideos()
        {
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
