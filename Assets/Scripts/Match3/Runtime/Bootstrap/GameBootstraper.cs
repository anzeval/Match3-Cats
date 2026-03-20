using UnityEngine;
using Match3.Model.Game;
using Match3.Runtime.Config;
using Match3.Controller;
using Match3.Runtime.Input;
using Match3.Runtime.Presentation;
using Match3.Runtime.UI;
using Match3.Runtime.Playback;

namespace Match3.Runtime.Bootstrap
{
   public class GameBootstraper : MonoBehaviour
    {
        [SerializeField] private int seed = 12345;
        [SerializeField] private LevelSettings levelSettings; // todo implement level database in the near future 
        [SerializeField] AnimationSettings animationSettings;

        [SerializeField] private BoardView boardView;
        [SerializeField] private HudController hud;
        [SerializeField] private SwapInputController swapInputController;
        [SerializeField] private MovePlaybackController animation;

        private GameSession gameSession;
        private GameController gameController;

        void Start()
        {
            gameSession = new GameSession(seed);
            gameSession.StartSession(levelSettings);

            gameController = new GameController();

            gameController.Initialize(gameSession, swapInputController, animation, boardView, hud);
            swapInputController.Initialize(gameController);
            animation.Initialize(boardView, swapInputController);
            gameController.StartGame();
        }
    } 
}