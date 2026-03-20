namespace Match3.Model.Data
{
   public class ClearResult
    {
        public Position Position {get; private set;}
        public TileType TileType {get; private set;}

        public ClearResult(Position position, TileType tileType)
        {
            this.Position = position;
            this.TileType = tileType;
        }
    } 
}

