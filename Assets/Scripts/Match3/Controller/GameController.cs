using Match3.Model.Data;
using Match3.Model.Game;
using Match3.Runtime.Input;
using Match3.Runtime.Playback;
using Match3.Runtime.Presentation;
using Match3.Runtime.UI;

namespace Match3.Controller
{
    public class GameController
    {
        GameSession session;
        SwapInputController input;
        MovePlaybackController playback;
        BoardView boardView;
        HudController hud;

        public void Initialize(GameSession session, SwapInputController input, MovePlaybackController playback, BoardView boardView, HudController hud)
        {
            this.session = session;
            this.input = input;
            this.playback = playback;
            this.boardView = boardView;
            this.hud = hud;
            playback.onFinished += OnPlaybackFinished;
        }

        public void StartGame()
        {
            boardView.Build(session.GetBoardState());
            hud.UpdateHud(session.Score, session.RemainingMoves);
        }

        public bool OnSwapRequested(Position a, Position b)
        {
            if (session.CurrentState != GameState.PlayerInput) return false;

            MoveProcessResult result = session.TryMakeMove(a, b);

            if (!result.IsSuccess)
            {
                playback.PlayInvalidSwapBounce(a, b);
                return false;
            }

            playback.Play(result);
            hud.UpdateHud(session.Score, session.RemainingMoves);

            return true;
        }

        private void OnPlaybackFinished()
        {
            session.OnAnimationsFinished();

            hud.UpdateHud(session.Score, session.RemainingMoves);

            if (session.CurrentState == GameState.Win)
                hud.ShowWin();

            if (session.CurrentState == GameState.GameOver)
                hud.ShowLose();
        }
    }
}