using Match3.Model.Board;
using Match3.Model.Data;
using Match3.Model.Utils;
using System.Collections.Generic;

namespace Match3.Model.Game
{
   public class MoveProcessor
    {
        private BoardState board;
        private MatchFinder matchFinder;
        private MoveAvailabilityChecker moveAvailabilityChecker;
        private BoardGenerator boardGenerator;
        private SwapValidator swapValidator;
        private SwapExecutor swapExecutor;
        private ClearSystem clearSystem;
        private GravitySystem gravitySystem;
        private RefillSystem refillSystem;
        private ShuffleSystem shuffleSystem;
        private CascadeRessolver cascadeResolver;
        private Rng rng;
        
        public MoveProcessor(int boardHeight, int boardWidth, int seed)
        {
            rng = new Rng(seed);
            board = new BoardState(boardHeight, boardWidth);
            matchFinder = new MatchFinder();
            swapValidator = new SwapValidator(matchFinder);
            moveAvailabilityChecker = new MoveAvailabilityChecker(swapValidator);
            swapExecutor = new SwapExecutor();
            clearSystem = new ClearSystem();
            gravitySystem = new GravitySystem();
            refillSystem = new RefillSystem(rng);
            shuffleSystem = new ShuffleSystem(rng, matchFinder, moveAvailabilityChecker);
            cascadeResolver = new CascadeRessolver(matchFinder, clearSystem, gravitySystem, refillSystem, moveAvailabilityChecker, shuffleSystem);

            boardGenerator = new BoardGenerator(rng, matchFinder, moveAvailabilityChecker);
            boardGenerator.Generate(board);
        }

        public MoveProcessResult ProcessMove(Position a, Position b)
        {
            if(!swapValidator.CanSwap(board, a, b))
            return new MoveProcessResult(false);

            MoveResult moveResult = swapExecutor.ApplySwap(board, a, b);

            List<MatchGroup> firstMatches = matchFinder.FindMatches(board);

            if(firstMatches == null || firstMatches.Count == 0)
            {
                swapExecutor.ApplySwap(board, b, a);
                return new MoveProcessResult(false);
            }

            List<StepResult> results = cascadeResolver.Resolve(board);

            return new MoveProcessResult(results, moveResult, true);
        }

        public BoardState GetBoardState()
        {
            return board;
        }
    }  
}