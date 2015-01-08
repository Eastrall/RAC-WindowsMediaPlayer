using FirstFloor.ModernUI.Presentation;
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

        public LinkCollection VideosList { get; private set; }

        #endregion

        #region PROPERTIES
        #endregion

        #region CONSTRUCTORS

        public HomeVideoViewModel()
        {
            this.VideosList = new LinkCollection();
            for (int i = 0; i < 20; i++)
            {
                this.VideosList.Add(new Link()
                    {
                        DisplayName = "Test " + i.ToString()
                    });
            }
        }

        #endregion

        #region METHODS
        #endregion
    }
}
