using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P2PBoardGames
{
    public class PieceMovement
    {
        private List<int> vertical;
        private List<int> horizontal;
        private List<int[]> movementPermutation;
        public PieceMovement()
        {
            vertical = new List<int>();
            horizontal = new List<int>();
            movementPermutation = new List<int[]>();
        }

        public void SetVertical(List<int> vList)
        {
            vertical.Clear();
            vertical.AddRange(vList);
        }
        public void SetHorizontal(List<int> hList)
        {
            horizontal.Clear();
            horizontal.AddRange(hList);
        }

        public void updateMovementPermutation()
        {
            int[] combined;
            foreach (int v in vertical)
                foreach (int h in horizontal)
                {
                    combined = new int[2] { v, h };
                    movementPermutation.Add(combined);
                }

            movementPermutation = movementPermutation.Distinct().ToList();
        }

        public List<int[]> GetMovementPermutation() => movementPermutation;
    }
}
