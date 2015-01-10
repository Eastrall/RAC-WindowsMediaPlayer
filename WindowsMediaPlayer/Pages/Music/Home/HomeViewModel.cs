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
        #region FIELDS

        private MediaCollection<MusicModel> mediaCollection;

        private RelayCommand addMusicButtonCommand;

        private RelayCommand addMusicDropCommand;

        private RelayCommand playMusicCommand;

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

        #endregion

        #region CONSTRUCTORS

        /// <summary>
        /// Creates a new HomeViewModel instance
        /// </summary>
        public HomeViewModel()
        {
            this.mediaCollection = new MediaCollection<MusicModel>(Constants.MUSICS_FILE);
            this.mediaCollection.Load();
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
                if (this.Musics.FirstOrDefault((musicPath) => { return musicPath.Path == path; }) == null)
                {
                    this.Musics.Add(_toAdd);
                    this.OnPropertyChanged("Musics");
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

            MediaPlayer.Instance.Audio.Load(_music.Path);
            MediaPlayer.Instance.Audio.Play();
        }

        #endregion
    }
}
