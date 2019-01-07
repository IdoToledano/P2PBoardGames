using System.Windows;
using System.Windows.Controls;

namespace P2PBoardGames.GamesPages.Draughts
{
    public class DraughtsPiece : RadioButton
    {
        public string Color { get; set; }

        public DraughtsPiece(string colour, string gn)
        {
            Color = colour;
            GroupName = gn;
        }

        public override string ToString()
        {
            return string.Format("Row: {0}, Column: {1}, Color: {2}, Group Name: {3}", 
                Grid.GetRow(this), Grid.GetColumn(this), Color, GroupName);
        }
    }
}
