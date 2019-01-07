using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;

namespace P2PBoardGames
{
    /// <summary>
    /// Interaction logic for GameModeSelection.xaml
    /// </summary>
    public partial class GameModeSelection : Page
    {
        public GameModeSelection(string g)
        {
            InitializeComponent();
            Headline.Text = g.Remove(g.Length - 6, 6);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Button btn = sender as Button;
            if (btn.Name.Equals("BackButton"))
                NavigationService.GoBack();
            else
                NavigationService.Navigate(new GamePage(Headline.Text));
        }
    }
}
