using System.Collections.Generic;
using Match3.Model.Data;

namespace Match3.Model.Board
{
    public class SwapValidator
    {
        private MatchFinder matchFinder;

        public SwapValidator(MatchFinder matchFinder)
        {
            this.matchFinder = matchFinder;
        }

        public bool CanSwap(BoardState board, Position a, Position b)
        {
            if(!board.InBounds(a) || !board.InBounds(b)) return false;
            if(!a.IsNeighbor4(b)) return false;

            board.Swap(a, b);

            bool hasMatch = matchFinder.HasMatchAt(board, a) || matchFinder.HasMatchAt(board, b);

            board.Swap(a, b);

            return hasMatch;
        }
    }
}