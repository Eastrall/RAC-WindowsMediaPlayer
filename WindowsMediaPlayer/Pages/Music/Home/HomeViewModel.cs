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

        private ObservableCollection<MusicModel> musics;

        private List<String> musicPaths;

        private RelayCommand addMusicButton;

        private OpenFileDialog dialog;

        #endregion

        #region PROPERTIES

        public ObservableCollection<MusicModel> Musics
        {
            get
            {
                if (this.musics == null)
                    this.musics = new ObservableCollection<MusicModel>();
                return this.musics;
            }
            set
            {
                this.musics = value;
                this.OnPropertyChanged("Musics");
            }
        }

        private List<String> MusicPaths
        {
            get
            {
                if (this.musicPaths == null)
                    this.musicPaths = new List<String>();
                return this.musicPaths;
            }
            set
            {
                this.musicPaths = value;
            }
        }

        public RelayCommand AddMusicButton
        {
            get
            {
                if (this.addMusicButton == null)
                    this.addMusicButton = new RelayCommand((sender) => { this.onAddMusic(sender); });
                return this.addMusicButton;
            }
        }

        public OpenFileDialog Dialog
        {
            get
            {
                if (this.dialog == null)
                {
                    this.dialog = new OpenFileDialog();

                    this.dialog.Title = "Selectionnez les musiques à ajouter";
                    this.dialog.Multiselect = true;
                    this.dialog.Filter =
        "Musiques (*.mp3;*.wav)|*.mp3;*.wav";
                }
                return this.dialog;
            }
        }

        #endregion

        #region CONSTRUCTORS

        /// <summary>
        /// Creates a new HomeViewModel instance
        /// </summary>
        public HomeViewModel()
        {
            LoadMusics();
        }

        #endregion

        #region METHODS

        void    onAddMusic(Object sender)
        {
            String _notAdded = "";

            this.LoadMusics();
            if (Dialog.ShowDialog() == true)
            {
                foreach (String filename in Dialog.FileNames)
                {
                    if (File.Exists(filename))
                    {
                        MusicModel _toAdd = new MusicModel(filename);
                        if (Exists(filename) == false)
                        {
                            this.MusicPaths.Add(filename);
                            this.Musics.Add(_toAdd);
                        }
                        else
                            _notAdded += _toAdd.Title + " de " + _toAdd.Artist + "\n";
                    }
                }
                this.SaveMusics();
            }
            if (_notAdded.Length > 0)
            {
                var _dlg = new ModernDialog
                {
                    Title = "Information",
                    Content = "Les musiques suivantes sont déjà sur votre bibliothèque et n'ont donc pas été ajoutées:\n\n" +_notAdded
                };
                _dlg.Buttons = new Button[] { _dlg.OkButton};
                _dlg.ShowDialog();
            }
        }

        private Boolean Exists(String toAdd)
        {
            foreach (String _music in this.MusicPaths)
            {
                if (_music.Equals(toAdd))
                    return true;
            }
            return false;
        }

        private void SaveMusics()
        {
            XmlSerializer.Serialize<List<String>>(this.MusicPaths, Constants.MUSICS_FILE);
        }

        private void LoadMusics()
        {
            if (File.Exists(Constants.MUSICS_FILE) == false)
            {
                return;
            }
            StreamReader _reader = new StreamReader(Constants.MUSICS_FILE);
            if ((this.MusicPaths = XmlSerializer.Deserialize<List<String>>(_reader)) != null)
            {
                this.Musics.Clear();
                foreach (String path in this.MusicPaths)
                {
                    MusicModel _music = new MusicModel(path);
                    this.Musics.Add(_music);
                }
            }
        }

        #endregion
    }
}
