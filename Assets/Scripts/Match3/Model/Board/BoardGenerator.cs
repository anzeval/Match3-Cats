using Match3.Model.Data;
using Match3.Model.Utils;
using System;

namespace Match3.Model.Board
{
    public class BoardGenerator
    {
        private Rng rng;
        private MatchFinder matchFinder;
        private MoveAvailabilityChecker moveAvailabilityChecker;
        private int generateLimit = 100;

        private TileType[] tileTypes;

        public BoardGenerator(Rng rng, MatchFinder matchFinder, MoveAvailabilityChecker moveAvailabilityChecker)
        {
            this.rng = rng;
            this.matchFinder = matchFinder;
            this.moveAvailabilityChecker = moveAvailabilityChecker;

            //Get all tile types
            tileTypes = (TileType[])Enum.GetValues(typeof(TileType));
        }

        private TileType GetRandomTileType()
        {
            return tileTypes[rng.NextInt(1, tileTypes.Length)];
        }

        public void Generate(BoardState boardState)
        {
            //A limited number of attempts to recreate the board in order to avoid overloading the system.
            for (int i = 0; i < generateLimit; i++)
            {
                GenerateBoard(boardState);
            
                if (matchFinder.FindMatches(boardState).Count == 0 && moveAvailabilityChecker.HasAnyValidMove(boardState)) break;
            }
        }

        private void GenerateBoard(BoardState boardState)
        {
            for (int row = 0; row < boardState.Height; row++)
            {
                for (int column = 0; column < boardState.Width; column++)
                {
                    TileType randomTile;

                    //if create match --> change tile
                    do
                    {
                        randomTile = GetRandomTileType();
                    }
                    while (IsCreateMatch(boardState, randomTile, row, column));

                    boardState.Set(new Position(row, column), randomTile);
                }
            }
        }

        private bool IsCreateMatch(BoardState boardState, TileType randomTile, int row, int column)
        {
            //check left 
            if(column >= 2)
            {
                TileType leftTile = boardState.Get(new Position(row, column - 1));
                TileType farLeftTile = boardState.Get(new Position(row, column - 2));

                if(randomTile == leftTile && leftTile == farLeftTile) return true;
            }

            //check up 
            if(row >= 2)
            {
                TileType upTile = boardState.Get(new Position(row - 1, column));
                TileType farUpTile = boardState.Get(new Position(row - 2, column));

                if(randomTile == upTile && upTile == farUpTile) return true;
            }
            
            return false;
        }
    }
}