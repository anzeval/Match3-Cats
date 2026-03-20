using System.Collections.Generic;

namespace Match3.Model.Data
{
   public class StepResult
    {
        public List<ClearResult> Clears { get; private set;} 
        public List<MoveResult> GravityMoves { get; private set;}
        public List<SpawnResult> Spawns { get; private set;} 
        public List<ShuffleResult> ShuffleResults { get; private set;} 
        public bool IsShuffled { get; private set;}  

        public StepResult(List<ClearResult> clears, List<MoveResult> gravityMoves, List<SpawnResult> spawns)
        {
            Clears = clears;
            GravityMoves = gravityMoves;
            Spawns = spawns;
            ShuffleResults = new List<ShuffleResult>();
        }

        public StepResult(bool isShuffled, List<ShuffleResult> shuffleResult)
        {
            Clears = new List<ClearResult>();
            GravityMoves  = new List<MoveResult>();
            Spawns = new List<SpawnResult>();
            ShuffleResults = shuffleResult;
            
            IsShuffled = isShuffled;
        }
    } 
}