using Match3.Model.Data;

namespace Match3.Model.Board
{
    public class SwapExecutor
    {
         public MoveResult ApplySwap(BoardState board, Position a, Position b)
        {
            TileType tile1 = board.Get(a);
            TileType tile2 = board.Get(b);

            board.Swap(a,b);
            return new MoveResult(a,b, tile1, tile2, IsVerticalMatch(a,b));
        }

        private MatchOrientation IsVerticalMatch(Position a, Position b)
        {
            if(a.Row != b.Row) return MatchOrientation.Vertical;

            return MatchOrientation.Horizontal;
        }
    }
}
