using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P2PBoardGames
{
    public class AllCheckerPieces
    {
        private List<CheckersPiece> checkersPieces;
        private int[,] takenSquares;

        public AllCheckerPieces()
        {
            checkersPieces = new List<CheckersPiece>();
            takenSquares = new int[8, 8];
        }

        public void AddPiece(CheckersPiece piece)
        {
            checkersPieces.Add(piece);
            int[] pos = piece.GetPosition();
            if (piece.IsWhite())
                takenSquares[pos[0], pos[1]] = 1;

            else
                takenSquares[pos[0], pos[1]] = 2;
        }

        public List<CheckersPiece> GetCheckersPieces() { return checkersPieces; }

        public int[,] GetTakenSquares() { return takenSquares; }
    }
}