using System.Collections.Generic;
using Match3.Model.Data;
using Match3.Model.Utils;
using Unity.VisualScripting;
using UnityEngine;

namespace Match3.Model.Board
{
    public class ShuffleSystem
    {
        private Rng rng;
        private MatchFinder matchFinder;
        private MoveAvailabilityChecker moveAvailabilityChecker;
        private int generateLimit = 100;

        public ShuffleSystem(Rng rng, MatchFinder matchFinder, MoveAvailabilityChecker moveAvailabilityChecker)
        {
            this.rng = rng;
            this.matchFinder = matchFinder;
            this.moveAvailabilityChecker = moveAvailabilityChecker;
        }

        public List<ShuffleResult> ShuffleUntilPlayable(BoardState board)
        {
            TileType[,] boardBefore = TakeSnapshot(board);

            for (int i = 0; i < generateLimit; i++)
            {
                List<TileType> availableTiles = new List<TileType>();
            
                FillList(board, availableTiles);
                ShuffleList(availableTiles);
                FillBoard(board, availableTiles);

                if(moveAvailabilityChecker.HasAnyValidMove(board) && matchFinder.FindMatches(board).Count <= 0) break;
            }

            TileType[,] boardAfter = TakeSnapshot(board);

            return BuildShuffleResults(boardBefore, boardAfter, board);
        }

        private List<ShuffleResult> BuildShuffleResults(TileType[,] before, TileType[,] after, BoardState board)
        {
            List<ShuffleResult> shuffleResult = new List<ShuffleResult>();
            bool[,] usedTiles = new bool[board.Height, board.Width];

            for (int row = 0; row < board.Height; row++)
            {
                for (int column = 0; column < board.Width; column++)
                {
                    TileType tileType = before[row, column];
                    bool found = false;

                    for (int row1 = 0; row1 < board.Height; row1++)
                    {
                        for (int column1 = 0; column1 < board.Width; column1++)
                        {
                            if(usedTiles[row1, column1] == true) continue;
                            if(after[row1, column1] != tileType) continue; 

                            if(row != row1 || column != column1)
                            {
                                found = true;
                                usedTiles[row1, column1] = true;
                                shuffleResult.Add(new ShuffleResult(new Position(row, column), new Position(row1, column1), tileType));
                                break;
                            }
                        }
                        if(found) break;
                    }
                }
            }

            return shuffleResult;
        }

        private TileType[,] TakeSnapshot(BoardState board)
        {
            TileType[,] clone = new TileType[board.Height, board.Width];

            for (int row = 0; row < board.Height; row++)
            {
                for (int column = 0; column < board.Width; column++)
                {
                    clone[row, column] = board.Grid[row, column];
                }
            }

            return clone;
        }

        private void ShuffleList(List<TileType> tileTypes)
        {
            for (int i = tileTypes.Count -1; i > 0; i--)
            {
                int j = rng.NextInt(0, i + 1);
                TileType temp = tileTypes[i];
                tileTypes[i] = tileTypes[j];
                tileTypes[j] = temp;
            }
        }

        private void FillList(BoardState board, List<TileType> tileTypes)
        {
            foreach (TileType tileType in board.Grid)
                {
                    tileTypes.Add(tileType);
                }
        }

        private void FillBoard(BoardState board, List<TileType> tileTypes)
        {
            int index = 0;

            for (int row = 0; row < board.Height; row++)
            {
                for (int column = 0; column < board.Width; column++)
                {
                    TileType randomTile = tileTypes[index];
                    board.Set(new Position(row, column), randomTile);
                    index++;
                }
            } 
        }
    }
}

