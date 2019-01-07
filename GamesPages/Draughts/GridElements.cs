using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace P2PBoardGames.GamesPages.Draughts
{
    class GridElements
    {
        public ContentControl[,] Elements { get; }

        public GridElements()
        {
            Elements = new ContentControl[8, 8];
        }

        public void AddElement(ContentControl cc, int r, int c)
        {
            Elements[r, c] = cc;
        }
    }
}
