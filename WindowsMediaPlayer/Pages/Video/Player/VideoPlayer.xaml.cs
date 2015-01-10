using FirstFloor.ModernUI.Windows;
using System;
using System.Windows.Controls;
using System.Windows.Navigation;

namespace WindowsMediaPlayer.Pages.Video.Home
{
    /// <summary>
    /// Interaction logic for VideoPlayer.xaml
    /// </summary>
    public partial class VideoPlayer : UserControl, IContent
    {
        public VideoPlayer()
        {
            InitializeComponent();
            this.mediaElement.Play();
        }

        public void OnFragmentNavigation(FirstFloor.ModernUI.Windows.Navigation.FragmentNavigationEventArgs e)
        {
            String _content = e.Fragment;

            (this.DataContext as VideoPlayerViewModel).Source = _content;
        }

        public void OnNavigatedFrom(FirstFloor.ModernUI.Windows.Navigation.NavigationEventArgs e) { }

        public void OnNavigatedTo(FirstFloor.ModernUI.Windows.Navigation.NavigationEventArgs e) { }

        public void OnNavigatingFrom(FirstFloor.ModernUI.Windows.Navigation.NavigatingCancelEventArgs e) { }
    }
}