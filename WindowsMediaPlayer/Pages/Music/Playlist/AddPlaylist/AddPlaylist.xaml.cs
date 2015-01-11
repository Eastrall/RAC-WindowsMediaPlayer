using FirstFloor.ModernUI.Windows.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WindowsMediaPlayer.Pages.Music.Playlist.AddPlaylist
{
    /// <summary>
    /// Interaction logic for AddPlaylist.xaml
    /// </summary>
    public partial class AddPlaylist : ModernDialog
    {
        public AddPlaylist()
        {
            InitializeComponent();
            this.OkButton.Click += (sender, e) => { (this.DataContext as AddPlaylistViewModel).SavePlaylist(); };
            this.Buttons = new Button[] { this.OkButton, this.CancelButton };
        }
    }
}
