using Match3.Model.Utils;
using Match3.Model.Data;
using System.Collections.Generic;

namespace Match3.Model.Board
{
    public class RefillSystem
    {
        private TileType[] tileTypes;
        private Rng rng;

        public RefillSystem(Rng rng)
        {
            this.rng = rng;
            tileTypes = (TileType[])System.Enum.GetValues(typeof(TileType));
        }

        private TileType GetRandomTileType()
        {
            return tileTypes[rng.NextInt(1, tileTypes.Length)];
        }

        public List<SpawnResult> Refill(BoardState board)
        {
            List<SpawnResult> spawnResults = new List<SpawnResult>();

            for (int column = 0; column < board.Width; column++)
            {
                for (int row = board.Height - 1; row >= 0; row--)
                {
                    Position currentPos = new Position(row, column);

                    if(board.Get(currentPos) != TileType.None) continue;

                    TileType randomTileType = GetRandomTileType();

                    board.Set(currentPos, randomTileType);
                    spawnResults.Add(new SpawnResult(currentPos, randomTileType));
                }
            }
            return spawnResults;
        }
    }
}
