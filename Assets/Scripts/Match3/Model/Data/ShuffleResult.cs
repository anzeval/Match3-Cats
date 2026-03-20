namespace Match3.Model.Data
{
    public class ShuffleResult
    {
        
        public Position From {get; private set;}
        public Position To {get; private set;}
        public TileType TileType {get; private set;}

        public ShuffleResult(Position from, Position to, TileType tileType)
        {
            From = from;
            To = to;
            TileType = tileType;
        }
    }
}

