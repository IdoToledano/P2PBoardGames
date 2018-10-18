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

namespace P2PBoardGames
{
    /// <summary>s
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private ImageBrush WhitePieceImageBrush = new ImageBrush();
        private ImageBrush BlackPieceImageBrush = new ImageBrush();

        private ImageBrush WhitePieceSelectedImageBrush = new ImageBrush();
        private ImageBrush BlackPieceSelectedImageBrush = new ImageBrush();

        private ImageBrush MoveButtonImageBrush = new ImageBrush();

        private AllCheckerPieces allPieces = new AllCheckerPieces();
        CheckersPiece lastPiece = null;


        private bool WhiteTurn;

        public MainWindow()
        {
            InitializeComponent();
            WhitePieceImageBrush.ImageSource = new BitmapImage(new Uri("../../img/WhiteCheckersPiece.png", UriKind.Relative));
            WhitePieceSelectedImageBrush.ImageSource = new BitmapImage(new Uri("../../img/WhiteCheckersPieceSelected.png", UriKind.Relative));

            BlackPieceImageBrush.ImageSource = new BitmapImage(new Uri("../../img/BlackCheckersPiece.png", UriKind.Relative));
            BlackPieceSelectedImageBrush.ImageSource = new BitmapImage(new Uri("../../img/BlackCheckersPieceSelected.png", UriKind.Relative));

            MoveButtonImageBrush.ImageSource = new BitmapImage(new Uri("../../img/MoveButtonImage.png", UriKind.Relative));

            initPieces();
            lastPiece = Checkerboard.Children.Cast<UIElement>().First(e => Grid.GetRow(e) == 7 && Grid.GetColumn(e) == 0) as CheckersPiece;
            WhiteTurn = true;
        }

        private void WhitePiece_Click(object sender, RoutedEventArgs e)
        {
            if (WhiteTurn)
            {
                CheckersPiece piece = sender as CheckersPiece;
                bool isAbleToJumpLeft = UtilityClass.IsAbleToJump(piece, allPieces)[0];
                bool isAbleToJumpRight = UtilityClass.IsAbleToJump(piece, allPieces)[1];
                bool isAbleToEatLeft = UtilityClass.IsAbleToEat(piece, allPieces)[0];
                bool isAbleToEatRight = UtilityClass.IsAbleToEat(piece, allPieces)[1];
                if (isAbleToEatLeft || isAbleToEatRight || isAbleToJumpLeft || isAbleToJumpRight)
                {
                    piece.SetPieceBackground(WhitePieceSelectedImageBrush);
                    lastPiece.SetPieceBackground(WhitePieceImageBrush);
                    lastPiece = piece;
                }
            }
        }

        private void BlackPiece_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Some text");
        }

        private void RemovePiece(int row, int column)
        {

        }


        private void initPieces()
        {
            bool isWhite = false;
            int[] pos;

            CheckersPiece piece;

            for (int i = 0; i < 3; i++)
                for (int j = 7 - i % 2; j >= 0; j = j - 2)
                {
                    pos = new int[2];
                    pos[0] = i;
                    pos[1] = j;

                    piece = new CheckersPiece(isWhite, pos);
                    piece.Click += BlackPiece_Click;
                    piece.SetPieceBackground(BlackPieceImageBrush);
                    allPieces.AddPiece(piece);
                }

            isWhite = true;
            for (int i = 5; i < 8; i++)
                for (int j = 7 - i % 2; j >= 0; j = j - 2)
                {
                    pos = new int[2];
                    pos[0] = i;
                    pos[1] = j;

                    piece = new CheckersPiece(isWhite, pos);
                    piece.Click += WhitePiece_Click;
                    piece.SetPieceBackground(WhitePieceImageBrush);
                    allPieces.AddPiece(piece);
                }

            foreach (CheckersPiece aPiece in allPieces.GetCheckersPieces())
            {
                Grid.SetRow(aPiece, aPiece.GetPosition()[0]);
                Grid.SetColumn(aPiece, aPiece.GetPosition()[1]);
                Checkerboard.Children.Add(aPiece);
            }
        }
    }
}