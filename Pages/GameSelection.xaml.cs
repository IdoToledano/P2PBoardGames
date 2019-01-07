using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;

namespace P2PBoardGames
{
    /// <summary>
    /// Interaction logic for GameSelection.xaml
    /// </summary>
    public partial class GameSelection : Page
    {
        public GameSelection()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Button btn = sender as Button;
            if (btn.Name.Equals("BackButton"))
                NavigationService.GoBack();
            else
                NavigationService.Navigate(new GameModeSelection(btn.Name));
        }
    }
}
