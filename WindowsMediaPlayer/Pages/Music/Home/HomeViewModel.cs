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

        public String PlayIcon
        {
            get
            {
                return "F1 M 30.0833,22.1667L 50.6665,37.6043L 50.6665,38.7918L 30.0833,53.8333L 30.0833,22.1667 Z";
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
            //for (Int32 i = 0; i < 50; i ++)
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
