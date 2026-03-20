namespace Match3.Model.Data
{
   public class MoveResult
    {
        public Position From { get; private set;}
        public Position To { get; private set;}
        public TileType TileType1 { get;private set;}
        public TileType TileType2 { get;private set;}
        public MatchOrientation matchOrientation;

        public MoveResult(Position from, Position to, TileType tile1, TileType tile2, MatchOrientation matchOrientation)
        {
            From = from;
            To = to;
            TileType1 = tile1;
            TileType2 = tile2;
            this.matchOrientation = matchOrientation;
        }
    } 
}