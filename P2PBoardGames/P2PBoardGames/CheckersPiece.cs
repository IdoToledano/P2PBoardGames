using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;


namespace P2PBoardGames
{
    public class CheckersPiece : Button
    {
        private bool isWhite;
        private int[] pos;
        private bool isKing;

        public CheckersPiece(bool iW, int[] ps)
        {
            isWhite = iW;
            pos = ps;
        }

        public void SetPieceBackground(ImageBrush imgBrush)
        {
            this.Background = imgBrush;
        }

        public bool IsWhite() { return isWhite; }
        public int[] GetPosition() { return pos; }

        public bool IsKing() { return isKing; }
    }
}