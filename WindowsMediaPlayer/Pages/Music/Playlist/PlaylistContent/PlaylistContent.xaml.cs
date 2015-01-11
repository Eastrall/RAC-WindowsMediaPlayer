using FirstFloor.ModernUI.Windows;
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

namespace WindowsMediaPlayer.Pages.Music.Playlist
{
    /// <summary>
    /// Interaction logic for PlaylistContent.xaml
    /// </summary>
    public partial class PlaylistContent : UserControl, IContent
    {
        public PlaylistContent()
        {
            InitializeComponent();
        }


        public void OnFragmentNavigation(FirstFloor.ModernUI.Windows.Navigation.FragmentNavigationEventArgs e)
        {
            String _content = e.Fragment;

            (this.DataContext as PlaylistContentViewModel).Source = _content;
        }

        public void OnNavigatedFrom(FirstFloor.ModernUI.Windows.Navigation.NavigationEventArgs e) { }

        public void OnNavigatedTo(FirstFloor.ModernUI.Windows.Navigation.NavigationEventArgs e) { }

        public void OnNavigatingFrom(FirstFloor.ModernUI.Windows.Navigation.NavigatingCancelEventArgs e) { }
    }
}
