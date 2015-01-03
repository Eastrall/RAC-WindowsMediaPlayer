using FirstFloor.ModernUI.Windows.Controls;
using FirstFloor.ModernUI.Presentation;
using System;
using System.Collections.Generic;
using System.IO;
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
using WindowsMediaPlayer.Serializer;

namespace WindowsMediaPlayer
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : ModernWindow
    {
        /// <summary>
        /// Initialize the MainWindow
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();
            this.LoadConfiguration();
        }

        /// <summary>
        /// Loads the configuration
        /// </summary>
        private void LoadConfiguration()
        {
            StreamReader _reader = new StreamReader(Constants.CONFIGURATION_FILE);
            Pages.Settings.AppearanceModel _appConfig = XmlSerializer.Deserialize<Pages.Settings.AppearanceModel>(_reader);
            
            AppearanceManager.Current.AccentColor = _appConfig.AmbiantColor;
            switch (_appConfig.Theme)
            {
                case "dark": AppearanceManager.Current.ThemeSource = AppearanceManager.DarkThemeSource; break;
                case "light": 
                default:
                    AppearanceManager.Current.ThemeSource = AppearanceManager.LightThemeSource; break;
            }
            switch (_appConfig.FontSize)
            {
                case "small": AppearanceManager.Current.FontSize = FirstFloor.ModernUI.Presentation.FontSize.Small; break;
                case "large":
                default:
                    AppearanceManager.Current.FontSize = FirstFloor.ModernUI.Presentation.FontSize.Large; break;
            }
        }
    }
}
