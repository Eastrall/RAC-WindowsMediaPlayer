using FirstFloor.ModernUI.Presentation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

/*--------------------------------------------------------
 * HomeViewModel.cs - file description
 * 
 * Version: 1.0
 * Author: Filipe
 * Created: 03/01/2015 15:54:46
 * 
 * Notes:
 * -------------------------------------------------------*/

namespace WindowsMediaPlayer.Pages.Home
{
    public class HomeViewModel : NotifyPropertyChanged
    {
        #region FIELDS

        private String text = "samy cou";

        private RelayCommand command;

        #endregion

        #region PROPERTIES

        public String Text
        {
            get
            {
                return this.text;
            }
            set
            {
                this.text = value;
                this.OnPropertyChanged("Text");
            }
        }

        public RelayCommand ClickCommand
        {
            get
            {
                if (this.command == null)
                {
                    this.command = new RelayCommand((o) => { this.Click(); });
                }
                return this.command;
            }
        }

        #endregion

        #region CONSTRUCTORS

        public HomeViewModel()
        {
        }

        #endregion

        #region METHODS

        private void Click()
        {
        }

        #endregion
    }
}
