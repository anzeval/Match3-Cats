using System.Collections.Generic;
using Match3.Model.Data;

namespace Match3.Model.Board
{
    public class MatchFinder
    {
        public List<MatchGroup> FindMatches(BoardState board)
        {
            List<MatchGroup> matchGroups = new List<MatchGroup>();

            ScanHorizontal(board, matchGroups);
            ScanVertical(board, matchGroups);

            return matchGroups;
        }

        private void ScanHorizontal(BoardState board, List<MatchGroup> matchGroups)
        {
            for (int row = 0; row < board.Height; row++)
            {
                List<Position> matchPositions = new List<Position>();
                TileType previousTileType = TileType.None;

                for (int column = 0; column < board.Width; column++)
                {
                    Position tilePosition = new Position(row, column);
                    TileType newTileType = board.Get(tilePosition);

                    if(newTileType == previousTileType && previousTileType != TileType.None)
                    {
                        matchPositions.Add(tilePosition);
                    } 
                    else
                    {
                        if (matchPositions.Count >= 3)
                        {
                        matchGroups.Add(new MatchGroup(previousTileType, new List<Position>(matchPositions), MatchOrientation.Horizontal)); 
                        }
                        
                        matchPositions.Clear();
                        previousTileType = newTileType;
                        matchPositions.Add(tilePosition);
                    }

                    if(column == board.Width - 1 && matchPositions.Count >= 3)
                    {
                        matchGroups.Add(new MatchGroup(previousTileType, new List<Position>(matchPositions), MatchOrientation.Horizontal)); 
                    }
                }
            }
        }

        private void ScanVertical(BoardState board, List<MatchGroup> matchGroups)
        {
            for (int column = 0; column < board.Width; column++)
            {
                List<Position> matchPositions = new List<Position>();
                TileType previousTileType = TileType.None;

                for (int row = 0; row < board.Height; row++)
                {
                    Position tilePosition = new Position(row, column);
                    TileType newTileType = board.Get(tilePosition);

                    if(newTileType == previousTileType && previousTileType != TileType.None)
                    {
                        matchPositions.Add(tilePosition);
                    }
                    else
                    {
                        if(matchPositions.Count >= 3)
                        {
                            matchGroups.Add(new MatchGroup(previousTileType,
                                new List<Position>(matchPositions),
                                MatchOrientation.Vertical));
                        }

                        matchPositions.Clear();
                        previousTileType = newTileType;
                        matchPositions.Add(tilePosition);
                    }

                    if(row == board.Height - 1 && matchPositions.Count >= 3)
                    {
                        matchGroups.Add(new MatchGroup(previousTileType, new List<Position>(matchPositions), MatchOrientation.Vertical)); 
                    }
                }
            }
        }

        public bool HasMatchAt(BoardState board, Position pos)
        {
            TileType type = board.Get(pos);

            if(type == TileType.None) return false;

            int horizontal = 1 + CountDirection(board, pos, 0, -1, type) + CountDirection(board, pos, 0, 1, type);

            if(horizontal >= 3)
                return true;

            int vertical = 1 + CountDirection(board, pos, -1, 0, type) + CountDirection(board, pos, 1, 0, type);

            return vertical >= 3;
        }

        private int CountDirection(BoardState board, Position start, int dRow, int dCol, TileType type)
        {
            int count = 0;

            int row = start.Row + dRow;
            int col = start.Column + dCol;

            while(board.InBounds(new Position(row, col)) && board.Get(new Position(row, col)) == type)
            {
                count++;
                row += dRow;
                col += dCol;
            }

            return count;
        }
    }
}
