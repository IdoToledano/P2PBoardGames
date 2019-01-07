namespace P2PBoardGames.GamesPages.Draughts
{
    class AllPieces
    {
        public DraughtsPiece[,] allPieces { get; }

        public AllPieces()
        {
            allPieces = new DraughtsPiece[8, 8];
        }

        public void AddPiece(DraughtsPiece piece)
        {
            allPieces[piece.Row, piece.Column] = piece;
        }
    }
}

