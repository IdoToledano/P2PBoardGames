using P2PBoardGames.GamesPages.Draughts;
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

namespace P2PBoardGames.Games_Logic
{
    /// <summary>
    /// Interaction logic for Draughts.xaml
    /// </summary>
    public partial class Draughts : Page
    {
        private HandleDraughtsGame hdg;
        public Draughts()
        {
            InitializeComponent();
            hdg = new HandleDraughtsGame(this);
        }
        
    }
}
