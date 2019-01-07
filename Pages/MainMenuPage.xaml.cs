using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;

namespace P2PBoardGames
{
    /// <summary>
    /// Interaction logic for MainMenuPage.xaml
    /// </summary>
    public partial class MainMenuPage : Page
    {
        public MainMenuPage()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new GameSelection());
        }
    }
}
