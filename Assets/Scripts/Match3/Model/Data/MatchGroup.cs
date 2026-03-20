using System.Collections.Generic;

namespace Match3.Model.Data
{
   public struct MatchGroup
    {
        public TileType tileType {get; private set;}
        public List<Position> positions {get; private set;}
        public MatchOrientation matchOrientation {get; private set;}

        public MatchGroup(TileType tileType, List<Position> positions, MatchOrientation matchOrientation)
        {
            this.tileType = tileType;
            this.positions = positions;
            this.matchOrientation = matchOrientation;
        }
    } 
}