using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FirstFloor.ModernUI.Presentation;
using System.Collections.ObjectModel;

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

        #endregion

        #region PROPERTIES

        public ObservableCollection<MusicModel> Musics
        {
            get
            {
                return this.musics;
            }
            set
            {
                this.musics = value;
                this.OnPropertyChanged("Musics");
            }
        }

        #endregion

        #region CONSTRUCTORS

        /// <summary>
        /// Creates a new HomeViewModel instance
        /// </summary>
        public HomeViewModel()
        {
            this.musics = new ObservableCollection<MusicModel>();
            this.musics.Add(new MusicModel()
                {
                    Title = "Cou",
                    Artist = "Samy",
                    Duration = "5:03",
                    Album = "La révolte du cou"
                });
        }

        #endregion

        #region METHODS
        #endregion
    }
}
