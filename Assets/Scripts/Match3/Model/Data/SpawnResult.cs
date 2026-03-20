namespace Match3.Model.Data
{
  public class SpawnResult
    {
         public Position Position { get; private set;}
        public TileType TileType { get; private set;}

        public SpawnResult(Position position, TileType type)
        {
          this.Position = position;
          this.TileType = type;
        }
    }  
}