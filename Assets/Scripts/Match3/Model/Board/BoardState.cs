using Match3.Model.Data;

namespace Match3.Model.Board
{
    public class BoardState
    {
        public int Height {get; private set;}
        public int Width {get; private set;}
        public TileType[,] Grid {get; private set;}

        public BoardState(int boardHeight, int boardWidth)
        {
            Height = boardHeight;
            Width = boardWidth;

           Grid = new TileType[Height, Width];
        }

        public TileType Get(Position position)
        {
            return Grid[position.Row, position.Column];
        }          

        public void Set(Position position, TileType tileType)
        {
            Grid[position.Row, position.Column] = tileType;
        }

        public bool InBounds(Position position)
        {
            return position.Row >= 0 && position.Row < Height && position.Column >= 0 && position.Column < Width;
        }

        public void Swap(Position a, Position b)
        {
            TileType tile1 = Get(a);
            TileType tile2 = Get(b);

            Set(a, tile2);
            Set(b, tile1);
        }  
    }
}
