using P2PBoardGames.GamesPages.Draughts;
using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;
using System.IO;
using System.Collections.Generic;

namespace P2PBoardGames.Games_Logic
{
    class HandleDraughtsGame
    {
        private readonly Draughts game;
        private readonly string logPath;
        private List<int[]> lastHighlight;
        private bool isWhiteTurn;
        private DraughtsPiece selectedPiece;
        
        public HandleDraughtsGame(Draughts g)
        {
            selectedPiece = null;
            game = g;
            logPath = "log.txt";
            lastHighlight = new List<int[]>();
            isWhiteTurn = true;

            File.WriteAllText(logPath, DateTime.Now.ToString() + Environment.NewLine);

            PlacePieces();
            DisableGroup();
        }

        private void DisableGroup()
        {
            foreach (DraughtsPiece piece in game.Board.Children.OfType<DraughtsPiece>())
                if (isWhiteTurn ^ piece.GroupName.StartsWith("White"))
                    piece.IsEnabled = false;
                else
                    piece.IsEnabled = true;
        }

        private void HandleClick(object sender, EventArgs e)
        {
            selectedPiece = sender as DraughtsPiece;
            File.AppendAllText(logPath, $"[{selectedPiece.ToString()}] is being clicked." + Environment.NewLine);
            UnHighlightMovingSquares();
            lastHighlight.Clear();

            HighlightMovingSquares(selectedPiece);
            HighlightEatingSquares(selectedPiece);
        }

        private void HandleEmptySquareClick(object sender, EventArgs e)
        {
            Border border = sender as Border;
            Ellipse ellipse = border.Child as Ellipse;
            int r = Grid.GetRow(border), c = Grid.GetColumn(border);
            if (ellipse.Fill != Brushes.Transparent && selectedPiece != null)
            {
                Grid.SetRow(border, Grid.GetRow(selectedPiece));
                Grid.SetColumn(border, Grid.GetColumn(selectedPiece));
                Grid.SetRow(selectedPiece, r);
                Grid.SetColumn(selectedPiece, c);

                if (ellipse.Fill == Brushes.ForestGreen)
                {
                    int k = (r + Grid.GetRow(border)) / 2, j = (c + Grid.GetColumn(border)) / 2; //The piece that is being eaten row & column
                    var element = game.Board.Children
                                .Cast<UIElement>()
                                .First(i => Grid.GetRow(i) == k && Grid.GetColumn(i) == j);

                    game.Board.Children.Remove(element);
                    Border emp = CreateEmptySquare();
                    emp.MouseLeftButtonUp += HandleEmptySquareClick;
                    Grid.SetRow(emp, k);
                    Grid.SetColumn(emp, j);
                    game.Board.Children.Add(emp);
                    if (TeamLost())
                        if (isWhiteTurn)
                            MessageBox.Show("White Won");
                        else
                            MessageBox.Show("Black Won");
                }

                if ((r == 0 && selectedPiece.GroupName.Equals("White")) || (r == 7 && selectedPiece.GroupName.Equals("Black")))
                    selectedPiece.GroupName += "King";
                

                ellipse.Fill = Brushes.Transparent;
                isWhiteTurn = !isWhiteTurn;
                selectedPiece.IsChecked = false;
                DisableGroup();
                UnHighlightMovingSquares();
                lastHighlight.Clear();
            }
        }

        private bool TeamLost()
        {
            if (isWhiteTurn)
            {
                var elementCollection = game.Board.Children.OfType<DraughtsPiece>().Where(i => i.GroupName.StartsWith("Black"));
                if (elementCollection != null)
                    if (elementCollection.Count() == 0)
                        return true;
                    else
                        return false;
                else
                    return true;
            }
            else
            {
                {
                    var elementCollection = game.Board.Children.OfType<DraughtsPiece>().Where(i => i.GroupName.StartsWith("White"));
                    if (elementCollection != null)
                        if (elementCollection.Count() == 0)
                            return true;
                        else
                            return false;
                    else
                        return true;
                }
            }

        }

        private void HighlightMovingSquares(DraughtsPiece piece)
        {
            int r = Grid.GetRow(piece), c = Grid.GetColumn(piece);
            if (piece.GroupName.Equals("Black"))
                switch(c)
                {
                    case 0: if (r < 7)
                        {
                            if (IsEmptySquare(r + 1, c + 1))
                            {
                                HighlightSquare(r + 1, c + 1);
                                lastHighlight.Add(new int[2] { r + 1, c + 1});
                            }
                        } break;
                    case 1:
                    case 2:
                    case 3:
                    case 4:
                    case 5:
                    case 6:
                        {
                            if (r < 7)
                            {
                                if (IsEmptySquare(r + 1, c + 1))
                                {
                                    HighlightSquare(r + 1, c + 1);
                                    lastHighlight.Add(new int[2] { r + 1, c + 1 });
                                }

                                if (IsEmptySquare(r + 1, c - 1))
                                {
                                    HighlightSquare(r + 1, c - 1);
                                    lastHighlight.Add(new int[2] { r + 1, c - 1 });
                                }
                            }
                            break;
                        }

                    case 7:
                        if (r < 7)
                        {
                            if (IsEmptySquare(r + 1, c - 1))
                            {
                                HighlightSquare(r + 1, c - 1);
                                lastHighlight.Add(new int[2] { r + 1, c - 1 });
                            }
                        }
                        break;
                    default:
                        throw new NotImplementedException();
                }

            else if (piece.GroupName.Equals("White"))
                switch (c)
                {
                    case 0:
                        if (r > 0)
                        {
                            if (IsEmptySquare(r - 1, c + 1))
                            {
                                HighlightSquare(r - 1, c + 1);
                                lastHighlight.Add(new int[2] { r - 1, c + 1 });
                            }
                        }
                        break;
                    case 1:
                    case 2:
                    case 3:
                    case 4:
                    case 5:
                    case 6:
                        {
                            if (r > 0)
                            {
                                if (IsEmptySquare(r - 1, c + 1))
                                {
                                    HighlightSquare(r - 1, c + 1);
                                    lastHighlight.Add(new int[2] { r - 1, c + 1 });
                                }

                                if (IsEmptySquare(r - 1, c - 1))
                                {
                                    HighlightSquare(r - 1, c - 1);
                                    lastHighlight.Add(new int[2] { r - 1, c - 1 });
                                }
                            }
                            break;
                        }

                    case 7:
                        if (r > 0)
                        {
                            if (IsEmptySquare(r - 1, c - 1))
                            {
                                HighlightSquare(r - 1, c - 1);
                                lastHighlight.Add(new int[2] { r - 1, c - 1 });
                            }
                        }
                        break;
                    default:
                        throw new NotImplementedException();
                }

            else if (piece.GroupName.Contains("King"))
                switch (c)
                {
                    case 0:
                        if (r < 7)
                            if (IsEmptySquare(r + 1, c + 1))
                            {
                                HighlightSquare(r + 1, c + 1);
                                lastHighlight.Add(new int[2] { r + 1, c + 1 });
                            }
                        
                        if (r > 0)
                            if (IsEmptySquare(r - 1, c + 1))
                            {
                                HighlightSquare(r - 1, c + 1);
                                lastHighlight.Add(new int[2] { r - 1, c + 1 });
                            }
                        
                        break;
                    case 1:
                    case 2:
                    case 3:
                    case 4:
                    case 5:
                    case 6:
                        if (r < 7)
                        {
                            if (IsEmptySquare(r + 1, c + 1))
                            {
                                HighlightSquare(r + 1, c + 1);
                                lastHighlight.Add(new int[2] { r + 1, c + 1 });
                            }

                            if (IsEmptySquare(r + 1, c - 1))
                            {
                                HighlightSquare(r + 1, c - 1);
                                lastHighlight.Add(new int[2] { r + 1, c - 1 });
                            }
                        }
                        if (r > 0)
                        {
                            if (IsEmptySquare(r - 1, c + 1))
                            {
                                HighlightSquare(r - 1, c + 1);
                                lastHighlight.Add(new int[2] { r - 1, c + 1 });
                            }

                            if (IsEmptySquare(r - 1, c - 1))
                            {
                                HighlightSquare(r - 1, c - 1);
                                lastHighlight.Add(new int[2] { r - 1, c - 1 });
                            }
                        }
                        break;

                    case 7:
                        if (r < 7)
                            if (IsEmptySquare(r + 1, c - 1))
                            {
                                HighlightSquare(r + 1, c - 1);
                                lastHighlight.Add(new int[2] { r + 1, c - 1 });
                            }
                        
                        if (r > 0)
                            if (IsEmptySquare(r - 1, c - 1))
                            {
                                HighlightSquare(r - 1, c - 1);
                                lastHighlight.Add(new int[2] { r - 1, c - 1 });
                            }                        
                        break;
                    default:
                        throw new NotImplementedException();
                }
        }

        private void HighlightEatingSquares(DraughtsPiece piece)
        {
            int r = Grid.GetRow(piece), c = Grid.GetColumn(piece);
            if (piece.GroupName.Equals("Black"))
                switch (c)
                {
                    case 0:                        
                    case 1:
                        if (r < 6)
                        {
                            if (IsEmptySquare(r + 2, c + 2) && IsOppositeColor(piece, r + 1, c + 1))
                            {
                                HighlightSquareForEating(r + 2, c + 2);
                                lastHighlight.Add(new int[2] { r + 2, c + 2 });
                                
                            }
                        }
                        break;

                    case 2:
                    case 3:
                    case 4:
                    case 5:
                        {
                            if (r < 6)
                            {
                                if (IsEmptySquare(r + 2, c + 2) && IsOppositeColor(piece, r + 1, c + 1))
                                {
                                    HighlightSquareForEating(r + 2, c + 2);
                                    lastHighlight.Add(new int[2] { r + 2, c + 2 });
                                }

                                if (IsEmptySquare(r + 2, c - 2) && IsOppositeColor(piece, r + 1, c - 1))
                                {
                                    HighlightSquareForEating(r + 2, c - 2);
                                    lastHighlight.Add(new int[2] { r + 2, c - 2 });
                                }
                            }
                            break;
                        }
                    case 6:
                    case 7:
                        if (r < 6)
                        {
                            if (IsEmptySquare(r + 2, c - 2) && IsOppositeColor(piece, r + 1, c - 1))
                            {
                                HighlightSquareForEating(r + 2, c - 2);
                                lastHighlight.Add(new int[2] { r + 2, c - 2 });
                            }
                        }
                        break;
                    default:
                        throw new NotImplementedException();
                }

            else if (piece.GroupName.Equals("White"))
                switch (c)
                {
                    case 0:                     
                    case 1:
                        if (r > 1)
                        {
                            if (IsEmptySquare(r - 2, c + 2) && IsOppositeColor(piece, r - 1, c + 1))
                            {
                                HighlightSquareForEating(r - 2, c + 2);
                                lastHighlight.Add(new int[2] { r - 2, c + 2 });
                            }
                        }
                        break;
                    case 2:
                    case 3:
                    case 4:
                    case 5:
                        {
                            if (r > 1)
                            {
                                if (IsEmptySquare(r - 2, c + 2) && IsOppositeColor(piece, r - 1, c + 1))
                                {
                                    HighlightSquareForEating(r - 2, c + 2);
                                    lastHighlight.Add(new int[2] { r - 2, c + 2 });
                                }

                                if (IsEmptySquare(r - 2, c - 2) && IsOppositeColor(piece, r - 1, c - 1))
                                {
                                    HighlightSquareForEating(r - 2, c - 2);
                                    lastHighlight.Add(new int[2] { r - 2, c - 2 });
                                }
                            }
                            break;
                        }
                    case 6:
                    case 7:
                        if (r > 1)
                        {
                            if (IsEmptySquare(r - 2, c - 2) && IsOppositeColor(piece, r - 1, c - 1))
                            {
                                HighlightSquareForEating(r - 2, c - 2);
                                lastHighlight.Add(new int[2] { r - 2, c - 2 });
                            }
                        }
                        break;
                    default:
                        throw new NotImplementedException();
                }

            else if (piece.GroupName.Contains("King"))
                switch (c)
                {
                    case 0:
                    case 1:
                        if (r < 6)
                            if (IsEmptySquare(r + 2, c + 2) && IsOppositeColor(piece, r + 1, c + 1))
                            {
                                HighlightSquareForEating(r + 2, c + 2);
                                lastHighlight.Add(new int[2] { r + 2, c + 2 });
                            }

                        if (r > 1)
                            if (IsEmptySquare(r - 2, c + 2) && IsOppositeColor(piece, r - 1, c + 1))
                            {
                                HighlightSquareForEating(r - 2, c + 2);
                                lastHighlight.Add(new int[2] { r - 2, c + 2 });
                            }

                        break;
                    case 2:
                    case 3:
                    case 4:
                    case 5:
                        if (r < 6)
                        {
                            if (IsEmptySquare(r + 2, c + 2) && IsOppositeColor(piece, r + 1, c + 1))
                            {
                                HighlightSquareForEating(r + 2, c + 2);
                                lastHighlight.Add(new int[2] { r + 2, c + 2 });
                            }

                            if (IsEmptySquare(r + 2, c - 2) && IsOppositeColor(piece, r + 1, c - 1))
                            {
                                HighlightSquareForEating(r + 2, c - 2);
                                lastHighlight.Add(new int[2] { r + 2, c - 2 });
                            }
                        }
                        if (r > 1)
                        {
                            if (IsEmptySquare(r - 2, c + 2) && IsOppositeColor(piece, r - 1, c + 1))
                            {
                                HighlightSquareForEating(r - 2, c + 2);
                                lastHighlight.Add(new int[2] { r - 2, c + 2 });
                            }

                            if (IsEmptySquare(r - 2, c - 2) && IsOppositeColor(piece, r - 1, c - 1))
                            {
                                HighlightSquareForEating(r - 2, c - 2);
                                lastHighlight.Add(new int[2] { r - 2, c - 2 });
                            }
                        }
                        break;

                    case 6:
                    case 7:
                        if (r < 6)
                            if (IsEmptySquare(r + 2, c - 2) && IsOppositeColor(piece, r + 1, c - 1))
                            {
                                HighlightSquareForEating(r + 2, c - 2);
                                lastHighlight.Add(new int[2] { r + 2, c - 2 });
                            }

                        if (r > 1)
                            if (IsEmptySquare(r - 2, c - 2) && IsOppositeColor(piece, r - 1, c - 1))
                            {
                                HighlightSquareForEating(r - 2, c - 2);
                                lastHighlight.Add(new int[2] { r - 2, c - 2 });
                            }
                        break;
                    default:
                        throw new NotImplementedException();
                }
        }

        private void UnHighlightMovingSquares()
        {
            foreach (int[] arr in lastHighlight)
                UnHighlightSquare(arr[0], arr[1]);
        }

        private bool IsEmptySquare(int r, int c)
        {
            var element = game.Board.Children
                                .Cast<UIElement>()
                                .First(i => Grid.GetRow(i) == r && Grid.GetColumn(i) == c);

            return element.GetType() == typeof(Border);
        }

        private void HighlightSquare(Border square)
        {
            Ellipse ellipse = square.Child as Ellipse;
            ellipse.Fill = Brushes.Green;
        }

        private void HighlightSquare(int r, int c)
        {
            var element = game.Board.Children
                                .Cast<UIElement>()
                                .First(i => Grid.GetRow(i) == r && Grid.GetColumn(i) == c);

            if (element is Border)
            {
                Border el = element as Border;
                HighlightSquare(el);
            }
        }

        private void UnHighlightSquare(Border square)
        {
            Ellipse ellipse = square.Child as Ellipse;
            ellipse.Fill = Brushes.Transparent;
        }

        private void UnHighlightSquare(int r, int c)
        {
            var element = game.Board.Children
                                .Cast<UIElement>()
                                .First(i => Grid.GetRow(i) == r && Grid.GetColumn(i) == c);

            if (element is Border)
            {
                Border el = element as Border;
                UnHighlightSquare(el);
            }
        }

        private void HighlightSquareForEating(Border square)
        {
            Ellipse ellipse = square.Child as Ellipse;
            ellipse.Fill = Brushes.ForestGreen;
        }

        private void HighlightSquareForEating(int r, int c)
        {
            var element = game.Board.Children
                                .Cast<UIElement>()
                                .First(i => Grid.GetRow(i) == r && Grid.GetColumn(i) == c);

            if (element is Border)
            {
                Border el = element as Border;
                HighlightSquareForEating(el);
            }
        }

        private Border CreateEmptySquare()
        {
            Border border;
            Ellipse ellipse = new Ellipse()
            {
                Fill = Brushes.Transparent,
                Width = 20,
                Height = 20
            };

            border = new Border
            {
                Background = new SolidColorBrush(Color.FromRgb(139, 69, 18)),
                Child = ellipse
            };

            return border;
        }

        private bool IsOppositeColor(DraughtsPiece piece ,int r, int c)
        {
            var element = game.Board.Children
                                .Cast<UIElement>()
                                .First(i => Grid.GetRow(i) == r && Grid.GetColumn(i) == c);

            if (element is DraughtsPiece sPiece)
                if (sPiece.GroupName.StartsWith("White") ^ piece.GroupName.StartsWith("White"))
                    return true;

            return false;
        }

        private void PlacePieces()
        {
            DraughtsPiece draughtsPiece;
            for (int i = 0; i < 3; i++)
                for (int j = 7 - i % 2; j >= 0; j = j - 2)
                {
                    draughtsPiece = new DraughtsPiece("DarkRed", "Black");
                    draughtsPiece.Style = game.Board.Resources["DraughtsPieceStyle"] as Style;
                    draughtsPiece.Click += HandleClick;
                    Grid.SetRow(draughtsPiece, i);
                    Grid.SetColumn(draughtsPiece, j);
                    game.Board.Children.Add(draughtsPiece);
                }


            Border square;
            for (int i = 3; i < 5; i++)
                for (int j = 7 - i % 2; j >= 0; j = j - 2)
                {
                    square = CreateEmptySquare();
                    square.MouseLeftButtonUp += HandleEmptySquareClick;
                    Grid.SetRow(square, i);
                    Grid.SetColumn(square, j);
                    game.Board.Children.Add(square);
                }

            for (int i = 5; i < 8; i++)
                for (int j = 7 - i % 2; j >= 0; j = j - 2)
                {
                    draughtsPiece = new DraughtsPiece("NavajoWhite", "White");
                    draughtsPiece.Style = game.Board.Resources["DraughtsPieceStyle"] as Style;
                    draughtsPiece.Click += HandleClick;
                    Grid.SetRow(draughtsPiece, i);
                    Grid.SetColumn(draughtsPiece, j);
                    game.Board.Children.Add(draughtsPiece);
                }
        }
    }
}
