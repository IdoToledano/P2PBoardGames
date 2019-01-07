using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace P2PBoardGames.GamesPages.Draughts
{
    class EmptySquare : ContentControl
    {
        public int Row { get; set; }
        public int Column { get; set; }
        public string BackColor { get; set; }
        public string EllipseColor { get; set; }

        public EmptySquare(int r, int c)
        {
            Row = r;
            Column = c;
            BackColor = "#8b4512";
            EllipseColor = "transparent";
        }
    }
}
