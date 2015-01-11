using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FirstFloor.ModernUI.Presentation;
using System.Collections.ObjectModel;
using Microsoft.Win32;
using System.IO;
using WindowsMediaPlayer.Serializer;
using FirstFloor.ModernUI.Windows.Controls;
using System.Windows.Controls;
using System.Threading;
using System.Windows;
using WindowsMediaPlayer.Media;
using System.Windows.Threading;

/*--------------------------------------------------------
 * HomeViewModel.cs - file description
 * 
 * Version: 1.0
 * Author: Filipe
 * Created: 03/01/2015 21:44:45
 * 
 * Notes:
 * -------------------------------------------------------*/

namespace WindowsMediaPlayer.Pages.Music.Home
{
    public class HomeViewModel : NotifyPropertyChanged
    {
        #region STATIC

        public static Boolean NeedMusicUpdate = false;

        #endregion

        #region FIELDS

        private MediaCollection<MusicModel> mediaCollection;

        private RelayCommand addMusicButtonCommand;

        private RelayCommand addMusicDropCommand;

        private RelayCommand deleteMusicCommand;

        private RelayCommand playMusicCommand;

        private Int32 selectedIndex;

        #endregion

        #region PROPERTIES

        /// <summary>
        /// Gets or sets the Musics
        /// </summary>
        public ObservableCollection<MusicModel> Musics
        {
            get
            {
                return this.mediaCollection.Content;
            }
            set
            {
                this.mediaCollection.Content = value;
                this.OnPropertyChanged("Musics");
            }
        }

        /// <summary>
        /// Add music by button command
        /// </summary>
        public RelayCommand AddMusicButtonCommand
        {
            get
            {
                if (this.addMusicButtonCommand == null)
                {
                    this.addMusicButtonCommand = new RelayCommand((sender) => { this.AddMusicButtonCommandAction(sender); });
                }
                return this.addMusicButtonCommand;
            }
        }

        /// <summary>
        /// Add music by drop command
        /// </summary>
        public RelayCommand AddMusicDropCommand
        {
            get
            {
                if (this.addMusicDropCommand == null)
                {
                    this.addMusicDropCommand = new RelayCommand((param) => { this.AddMusicDropCommandAction(param); });
                }
                return this.addMusicDropCommand;
            }
        }

        /// <summary>
        /// Delete a music
        /// </summary>
        public RelayCommand DeleteMusicCommand
        {
            get
            {
                if (this.deleteMusicCommand == null)
                {
                    this.deleteMusicCommand = new RelayCommand((param) => { this.DeleteMusicCommandAction(param); });
                }
                return this.deleteMusicCommand;
            }
            set
            {
                this.deleteMusicCommand = value;
                this.OnPropertyChanged("DeleteMusicCommand");
            }
        }

        /// <summary>
        /// Play music command
        /// </summary>
        public RelayCommand PlayMusicCommand
        {
            get
            {
                if (this.playMusicCommand == null)
                {
                    this.playMusicCommand = new RelayCommand((param) => { this.PlayMusicCommandAction(param); });
                }
                return this.playMusicCommand;
            }
        }

        /// <summary>
        /// Gets or sets the Current music index on the datagrid
        /// </summary>
        public Int32 SelectedIndex
        {
            get
            {
                return this.selectedIndex;
            }
            set
            {
                this.selectedIndex = value;
                this.OnPropertyChanged("SelectedIndex");
            }
        }

        #endregion

        #region CONSTRUCTORS

        /// <summary>
        /// Creates a new HomeViewModel instance
        /// </summary>
        public HomeViewModel()
        {
            this.SelectedIndex = -1;
            this.mediaCollection = new MediaCollection<MusicModel>(Constants.MUSICS_FILE);
            this.mediaCollection.OnLoaded += mediaCollection_OnLoaded;
            this.mediaCollection.Load();

            // Initialize update timer
            DispatcherTimer _timer = new DispatcherTimer();
            _timer.Interval = TimeSpan.FromSeconds(0.1);
            _timer.Tick += TimerTick;
            _timer.Start();
        }

        #endregion

        #region METHODS

        /// <summary>
        /// Add a music to the media library when user clicks on the "Add" button
        /// </summary>
        /// <param name="sender"></param>
        private void AddMusicButtonCommandAction(Object sender)
        {
            OpenFileDialog _openFileDialog = new OpenFileDialog();

            _openFileDialog.Title = "Selectionnez les musiques à ajouter";
            _openFileDialog.Multiselect = true;
            _openFileDialog.Filter = "Musiques (*.mp3;*.wav)|*.mp3;*.wav";
            this.mediaCollection.Load();
            if (_openFileDialog.ShowDialog() == true)
            {
                foreach (String path in _openFileDialog.FileNames)
                {
                    if (File.Exists(path) == true)
                    {
                        this.AddMusic(path);
                    }
                }
                this.mediaCollection.Save();
            }
        }

        /// <summary>
        /// Add a music to the media library when user drops musics in the panel
        /// </summary>
        /// <param name="param"></param>
        private void AddMusicDropCommandAction(Object param)
        {
            DragEventArgs _eventArgs = param as DragEventArgs;

            if (_eventArgs.Data.GetDataPresent(DataFormats.FileDrop, false) == true)
            {
                this.mediaCollection.Load();
                String[] _files = _eventArgs.Data.GetData(DataFormats.FileDrop) as String[];
                foreach (String path in _files)
                {
                    if (File.Exists(path) == true)
                    {
                        this.AddMusic(path);
                    }
                }
                this.mediaCollection.Save();
            }
        }

        /// <summary>
        /// Add a music to the media library
        /// </summary>
        /// <param name="path"></param>
        private void AddMusic(String path)
        {
            MusicModel _toAdd = null;

            try
            {
                _toAdd = new MusicModel(path);
                if (File.Exists(path) == false)
                {
                    return;
                }
                if (this.Musics != null)
                {
                    if (this.Musics.FirstOrDefault((musicPath) => { return musicPath.Path == path; }) == null)
                    {
                        this.Musics.Add(_toAdd);
                        this.OnPropertyChanged("Musics");
                    }
                }
            }
            catch (Exception e)
            {
                var _dlg = new ModernDialog
                {
                    Title = "Erreur",
                    Content = "Impossible d'ajouter le fichier audio " + path.ToString().Split('\\').Last().ToString() + " car celui-ci est corrompu.\nErreur: " + e.Message
                };
                _dlg.Buttons = new Button[] { _dlg.OkButton };
                _dlg.ShowDialog();
            }
        }

        /// <summary>
        /// Play music command action
        /// </summary>
        /// <param name="param"></param>
        private void PlayMusicCommandAction(Object param)
        {
            MusicModel _music = param as MusicModel;

            this.SelectedIndex = this.mediaCollection.Content.ToList().FindIndex((music) => { return music.Path == _music.Path; });
            MediaPlayer.Instance.CurrentPlaylist = null;
            MediaPlayer.Instance.Audio.Load(_music.Path);
            MediaPlayer.Instance.Audio.Play();
        }

        /// <summary>
        /// Delete a music from the global playlist
        /// </summary>
        /// <param name="param"></param>
        private void DeleteMusicCommandAction(Object param)
        {
            MusicModel _music = param as MusicModel;

            ModernDialog _dlg = new ModernDialog
            {
                Title = "Confirmation",
                Content = "Voulez-vous vraiment supprimer '" + _music.Title + "' ?"
            };
            _dlg.Buttons = new Button[] { _dlg.YesButton, _dlg.NoButton };
            if (_dlg.ShowDialog() == true)
            {
                this.Musics.Remove(_music);
                this.mediaCollection.Save();
            }
        }

        /// <summary>
        /// Event fired when media collection finish loading data
        /// </summary>
        /// <param name="sender"></param>
        private void mediaCollection_OnLoaded(Object sender)
        {
            if (this.mediaCollection.Content != null)
            {
                foreach (MusicModel music in this.mediaCollection.Content)
                {
                    music.RefreshData();
                }
            }
        }

        /// <summary>
        /// Update the datagrid cursor position
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void TimerTick(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(MediaPlayer.Instance.Audio.Source) == false && NeedMusicUpdate == true)
            {
                NeedMusicUpdate = false;
                this.SelectedIndex = this.mediaCollection.Content.ToList().FindIndex((music) => { return music.Path == MediaPlayer.Instance.Audio.Source; });
            }
        }

        #endregion
    }
}
