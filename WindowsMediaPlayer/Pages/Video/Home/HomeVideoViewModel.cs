using FirstFloor.ModernUI.Presentation;
using FirstFloor.ModernUI.Windows.Controls;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using WindowsMediaPlayer.Media;
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

        private MediaCollection<VideoModel> mediaCollection;

        private RelayCommand addVideoCommand;

        private RelayCommand addVideoDropCommand;

        private RelayCommand deleteVideoCommand;

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

        /// <summary>
        /// Add a new video by drop
        /// </summary>
        public RelayCommand AddVideoDropCommand
        {
            get
            {
                if (this.addVideoDropCommand == null)
                {
                    this.addVideoDropCommand = new RelayCommand((param) => { this.AddVideoDropCommandAction(param); });
                }
                return this.addVideoDropCommand;
            }
            set
            {
                this.addVideoDropCommand = value;
                this.OnPropertyChanged("AddVideoDropCommand");
            }
        }

        /// <summary>
        /// Delete a video
        /// </summary>
        public RelayCommand DeleteVideoCommand
        {
            get
            {
                if (this.deleteVideoCommand == null)
                {
                    this.deleteVideoCommand = new RelayCommand((param) => { this.DeleteVideoCommandAction(param); });
                }
                return this.deleteVideoCommand;
            }
            set
            {
                this.deleteVideoCommand = value;
                this.OnPropertyChanged("DeleteVideoCommand");
            }
        }

        #endregion

        #region CONSTRUCTORS

        /// <summary>
        /// Creates an instance of HomeVideoViewModel
        /// </summary>
        public HomeVideoViewModel()
        {
            this.VideosList = new LinkCollection();
            this.mediaCollection = new MediaCollection<VideoModel>(Constants.VIDEOS_FILE);
            this.mediaCollection.Load();
            this.InitializeTabs();
        }

        #endregion

        #region METHODS

        /// <summary>
        /// Initialize tab links
        /// </summary>
        private void InitializeTabs()
        {
            if (this.mediaCollection.Content != null)
            {
                foreach (VideoModel video in this.mediaCollection.Content)
                {
                    if (File.Exists(video.Path) == true)
                    {
                        this.AddFileToTab(video.Path);
                    }
                    else
                    {
                        ModernDialog _dialog = new ModernDialog()
                        {
                            Title = "Information",
                            Content = String.Format("Impossible de charger la vidéo '{0}'.", video.Path)
                        };
                        _dialog.Buttons = new Button[] { _dialog.OkButton };
                        _dialog.ShowDialog();
                    }
                }
            }
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
                    this.AddVideo(file);
                }
            }
            this.mediaCollection.Save();
        }

        /// <summary>
        /// Add a video when user drop it on the video panel
        /// </summary>
        /// <param name="param"></param>
        private void AddVideoDropCommandAction(Object param)
        {
            DragEventArgs _eventArgs = param as DragEventArgs;

            if (_eventArgs.Data.GetDataPresent(DataFormats.FileDrop, false) == true)
            {
                String[] _files = _eventArgs.Data.GetData(DataFormats.FileDrop) as String[];
                foreach (String file in _files)
                {
                    this.AddVideo(file);
                }
            }
        }

        private void DeleteVideoCommandAction(Object param)
        {
            String _videoPath = (param as Uri).OriginalString.Split('#').Last();
            VideoModel _video = this.mediaCollection.Content.ToList().Find((video) => { return video.Path == _videoPath; });
            Link _link = this.VideosList.ToList().Find((link) => { return link.DisplayName == _video.Name; });

            ModernDialog _dlg = new ModernDialog
            {
                Title = "Confirmation",
                Content = "Voulez-vous vraiment supprimer '" + _video.Name + "' ?"
            };
            _dlg.Buttons = new Button[] { _dlg.YesButton, _dlg.NoButton };
            if (_dlg.ShowDialog() == true)
            {
                this.mediaCollection.Content.Remove(_video);
                this.mediaCollection.Save();
                this.VideosList.Remove(_link);
            }
        }

        /// <summary>
        /// Add a video the MediaCollection
        /// </summary>
        /// <param name="path">Video path</param>
        private void AddVideo(String path)
        {
            if (this.mediaCollection.Content.FirstOrDefault((videoPath) => { return path == videoPath.Path; }) == null)
            {
                if (MediaPlayer.Instance.IsMedia(path) == true)
                {
                    this.AddFileToTab(path);
                    this.mediaCollection.Content.Add(new VideoModel(path.Split('\\').Last(), path));
                }
            }
            else
            {
                ModernDialog _dialog = new ModernDialog()
                {
                    Title = "Information",
                    Content = String.Format("La vidéo '{0}' n'a pas été ajouté car elle existe déjà dans la bibliotheque.", path)
                };
                _dialog.Buttons = new Button[] { _dialog.OkButton };
                _dialog.ShowDialog();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="path"></param>
        private void AddFileToTab(String path)
        {
            String _format = String.Format("/Pages/Video/Player/VideoPlayer.xaml#" + path);
            Link _link = new Link();
            _link.DisplayName = path.Split('\\').Last();
            _link.Source = new Uri(_format, UriKind.Relative);
            this.VideosList.Add(_link);
        }

        #endregion
    }
}
