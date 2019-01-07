using System.Windows;

namespace P2PBoardGames
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private MainMenuPage mainMenuPage = new MainMenuPage();
        public MainWindow()
        {
            InitializeComponent();
            MainMenuFrame.Content = mainMenuPage;
        }
    }
}
