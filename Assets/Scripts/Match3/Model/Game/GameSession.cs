using Match3.Model.Board;
using Match3.Model.Data;
using Match3.Runtime.Config;
using System.Collections.Generic;

namespace Match3.Model.Game
{
   public class GameSession
    {
        public int RemainingMoves {get; private set;}
        public int Score {get; private set;}
        public GameState CurrentState {get; private set;}

        private MoveProcessor moveProcessor;
        private LevelSettings levelSettings;
        private int seed;

        public GameSession(int seed)
        {
            this.seed = seed;
        }

        public void StartSession(LevelSettings levelSettings)
        {
            this.levelSettings = levelSettings;
            this.moveProcessor = new MoveProcessor(levelSettings.height, levelSettings.width, seed);
            Score = 0;
            RemainingMoves = levelSettings.movesAllowed;
            CurrentState = GameState.PlayerInput;
        }

        public MoveProcessResult TryMakeMove(Position a, Position b)
        {
            if(CurrentState != GameState.PlayerInput) return new MoveProcessResult(false);

            MoveProcessResult result = moveProcessor.ProcessMove(a,b);

            if (result.IsSuccess)
            {
                CurrentState = GameState.Animating;
                RemainingMoves--;
                ApplyScore(result.CascadeSteps);

                if(CheckWinCondition()) EndGame(true);
                else if(RemainingMoves <= 0) EndGame(false);
            }

            return result;
        }

        public void ApplyScore(List<StepResult> steps)
        {
            foreach (var stepList in steps)
            {
                Score += stepList.Clears.Count * levelSettings.scorePerTile; 
            }
        }

        public bool CheckWinCondition()
        {
            return levelSettings.targetScore <= Score;
        }

        public void EndGame(bool win)
        {
            if(win)
            {
                CurrentState = GameState.Win;
            } else
            {
                CurrentState = GameState.GameOver;
            }
        }

        public void Restart()
        {
            StartSession(levelSettings);
        }

        private void ChangeState(GameState newState)
        {
            CurrentState = newState;
        }

        public BoardState GetBoardState()
        {
            return moveProcessor.GetBoardState();
        }

        public void OnAnimationsFinished()
        {
            if(CurrentState == GameState.Win || CurrentState == GameState.GameOver) return;

            CurrentState = GameState.PlayerInput;
        }

        public MoveProcessor GetMoveProcessor()
        {
            return moveProcessor;
        }

    } 
}