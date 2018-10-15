using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace P2PBoardGames
{
    public static class UtilityClass
    {
        public static void initPieces()
        {

        }

        public static bool[] IsAbleToJump(CheckersPiece piece, AllCheckerPieces allPieces)
        {
            bool[] canJump = new bool[2] { IsAbleToJumpLeft(piece, allPieces), IsAbleToJumpRight(piece, allPieces) };
            return canJump;
        }

        private static bool IsAbleToJumpLeft(CheckersPiece piece, AllCheckerPieces allPieces)
        {
            bool canJumpLeft = false;
            int[,] takenSquares = allPieces.GetTakenSquares();
            bool isWhite = piece.IsWhite();
            int row = piece.GetPosition()[0];
            int column = piece.GetPosition()[1];

            if (column == 0)
                return canJumpLeft;

            else if (isWhite && takenSquares[row - 1, column - 1] != 0)
                return canJumpLeft;

            else if (!isWhite && takenSquares[row + 1, column - 1] != 0)
                return canJumpLeft;

            else
                return !canJumpLeft;
        }

        private static bool IsAbleToJumpRight(CheckersPiece piece, AllCheckerPieces allPieces)
        {
            bool canJumpRight = false;
            int[,] takenSquares = allPieces.GetTakenSquares();
            bool isWhite = piece.IsWhite();
            int row = piece.GetPosition()[0];
            int column = piece.GetPosition()[1];

            if (column == 7)
                return canJumpRight;

            else if (isWhite && takenSquares[row - 1, column + 1] != 0)
                return canJumpRight;

            else if (!isWhite && takenSquares[row + 1, column + 1] != 0)
                return canJumpRight;

            else
                return !canJumpRight;
        }

        public static bool[] IsAbleToEat(CheckersPiece piece, AllCheckerPieces allPieces)
        {
            bool[] canEat = new bool[2] { IsAbleToEatLeft(piece, allPieces), IsAbleToEatRight(piece, allPieces) };
            return canEat;
        }

        private static bool IsAbleToEatLeft(CheckersPiece piece, AllCheckerPieces allPieces)
        {
            bool canEatLeft = false;
            int[,] takenSquares = allPieces.GetTakenSquares();
            bool isWhite = piece.IsWhite();
            int row = piece.GetPosition()[0];
            int column = piece.GetPosition()[1];

            if (column == 0 || column == 1)
                return canEatLeft;

            return false;
        }

        private static bool IsAbleToEatRight(CheckersPiece piece, AllCheckerPieces allPieces)
        {
            bool canEatLeft = false;
            int[,] takenSquares = allPieces.GetTakenSquares();
            bool isWhite = piece.IsWhite();
            int row = piece.GetPosition()[0];
            int column = piece.GetPosition()[1];

            if (column == 6 || column == 7)
                return canEatLeft;

            return false;
        }
    }
}