using FirstFloor.ModernUI.Presentation;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

/*--------------------------------------------------------
 * HomePicturesViewModel.cs - file description
 * 
 * Version: 1.0
 * Author: Filipe
 * Created: 11/01/2015 15:48:18
 * 
 * Notes:
 * -------------------------------------------------------*/

namespace WindowsMediaPlayer.Pages.Pictures
{
    public class HomePicturesViewModel : NotifyPropertyChanged
    {
        #region FIELDS

        private RelayCommand openImageCommand;
        private RelayCommand editImageCommand;

        private String source;

        #endregion

        #region PROPERTIES

        /// <summary>
        /// Open an image
        /// </summary>
        public RelayCommand OpenImageCommand
        {
            get
            {
                if (this.openImageCommand == null)
                {
                    this.openImageCommand = new RelayCommand((param) => { this.OpenImageCommandAction(param); });
                }
                return this.openImageCommand;
            }
            set
            {
                this.openImageCommand = value;
                this.OnPropertyChanged("OpenImageCommand");
            }
        }

        /// <summary>
        /// Open the image in the default image editor
        /// </summary>
        public RelayCommand EditImageCommand
        {
            get
            {
                if (this.editImageCommand == null)
                {
                    this.editImageCommand = new RelayCommand((param) => { this.EditImageCommandAction(param); });
                }
                return this.editImageCommand;
            }
            set
            {
                this.editImageCommand = value;
                this.OnPropertyChanged("EditImgeCommand");
            }
        }

        public String Source
        {
            get
            {
                return this.source;
            }
            set
            {
                this.source = value;
                this.OnPropertyChanged("Source");
            }
        }

        #endregion

        #region CONSTRUCTORS

        /// <summary>
        /// Creates a new HomePicturesViewModel instance
        /// </summary>
        public HomePicturesViewModel()
        {
        }

        #endregion

        #region METHODS

        /// <summary>
        /// Open an image in the media element
        /// </summary>
        /// <param name="param"></param>
        private void OpenImageCommandAction(Object param)
        {
            OpenFileDialog _openFileDialog = new OpenFileDialog();

            _openFileDialog.Filter = "Vidéos (*.PNG;*.JPG;*.JPEG;*.BMP,*.GIF)|**.PNG;*.JPG;*.JPEG;*.BMP,*.GIF";
            if (_openFileDialog.ShowDialog() == true)
            {
                this.Source = _openFileDialog.FileName;
            }
        }

        /// <summary>
        /// Open the image of the media element in paint
        /// </summary>
        /// <param name="param"></param>
        private void EditImageCommandAction(Object param)
        {
            ProcessStartInfo _startInfo = new ProcessStartInfo(this.source);

            _startInfo.Verb = "edit";
            Process.Start(_startInfo);
        }

        #endregion
    }
}
