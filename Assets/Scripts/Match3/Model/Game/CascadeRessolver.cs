using System.Collections.Generic;
using Match3.Model.Board;
using Match3.Model.Data;

namespace Match3.Model.Game
{
    public class CascadeRessolver
    {
        private MatchFinder matchFinder;
        private ClearSystem clearSystem;
        private GravitySystem gravitySystem;
        private RefillSystem refillSystem;
        private MoveAvailabilityChecker moveAvailabilityChecker;
        private ShuffleSystem shuffleSystem;
        private int loopLimiter = 50;

        public CascadeRessolver(
            MatchFinder matchFinder,
            ClearSystem clearSystem, 
            GravitySystem gravitySystem,
            RefillSystem refillSystem, 
            MoveAvailabilityChecker moveAvailabilityChecker, 
            ShuffleSystem shuffleSystem)
        {
            this.matchFinder = matchFinder;
            this.clearSystem = clearSystem;
            this.gravitySystem = gravitySystem;
            this.refillSystem = refillSystem;
            this.moveAvailabilityChecker = moveAvailabilityChecker;
            this.shuffleSystem = shuffleSystem;
        }

        public List<StepResult> Resolve(BoardState board)
        {   
            List<StepResult> stepResults = new List<StepResult>();

            StabilizeBoard(board, stepResults);
            EnsurePlayable(board, stepResults);

            return stepResults;
        }

        private void StabilizeBoard(BoardState board, List<StepResult> stepResults)
        {
            for (int i = 0; i < loopLimiter; i++)
            {
                List<MatchGroup> matches = matchFinder.FindMatches(board);

                if(matches.Count == 0) break;
                
                List<ClearResult> clearResults = clearSystem.Clear(board, matches);
                List<MoveResult> moveResults = gravitySystem.ApplyGravity(board);
                List<SpawnResult> spawnResults = refillSystem.Refill(board);

                stepResults.Add(new StepResult(clearResults, moveResults, spawnResults));
            }
        }

        private void EnsurePlayable(BoardState board, List<StepResult> stepResults)
        {
            for (int i = 0; i < loopLimiter; i++)
            {   // if no moves --> shuffle --> check again
                if(!moveAvailabilityChecker.HasAnyValidMove(board))
                {
                    List<ShuffleResult> shuffleResult = shuffleSystem.ShuffleUntilPlayable(board);
                    stepResults.Add(new StepResult(true, shuffleResult)); 
                    
                    StabilizeBoard(board, stepResults);
                } else 
                {
                    break;
                } 
            }
        }
    }
} 