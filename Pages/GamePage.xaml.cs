using System;
using System.Windows.Controls;

namespace P2PBoardGames
{
    /// <summary>
    /// Interaction logic for GamePage.xaml
    /// </summary>
    public partial class GamePage : Page
    {
        public GamePage(string game)
        {
            InitializeComponent();
            GameFrame.Source = new Uri(string.Format("../GamesPages/{0}/{0}.xaml", game), UriKind.Relative);
        }
    }
}
