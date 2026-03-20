using System.Collections.Generic;
using Match3.Model.Data;

namespace Match3.Model.Board
{
    public class GravitySystem
    {
        public List<MoveResult> ApplyGravity(BoardState board)
        {
            List<MoveResult> results = new List<MoveResult>();

            for(int col = 0; col < board.Width; col++)
            {
                int writeRow = board.Height - 1;

                for(int row = board.Height - 1; row >= 0; row--)
                {
                    TileType tile = board.Get(new Position(row, col));

                    if(tile == TileType.None) continue;

                    if(row != writeRow)
                    {
                        Position from = new Position(row, col);
                        Position to = new Position(writeRow, col);

                        board.Set(to, tile);
                        board.Set(from, TileType.None);

                        results.Add(new MoveResult(from, to, tile, tile, MatchOrientation.Vertical));
                    }

                    writeRow--;
                }
            }

            return results;
        }
    }
}