using Match3.Model.Data;
using System.Collections.Generic;

namespace Match3.Model.Board
{
    public class ClearSystem
    {
        public List<ClearResult> Clear(BoardState board, List<MatchGroup> matches)
        {
            HashSet<Position> uniquePositions = new HashSet<Position>();
            List<ClearResult> clearResults = new List<ClearResult>();

            foreach (MatchGroup matchGroup in matches)
            {
                foreach (Position position in matchGroup.positions)
                {
                    uniquePositions.Add(position);
                }
            }

            foreach (Position position in uniquePositions)
            {
                clearResults.Add(new ClearResult(position, board.Get(position)));
                board.Set(position, TileType.None);
            }

            return clearResults;
        }
    }
}

