using Match3.Controller;
using Match3.Model.Data;
using Match3.Runtime.Presentation;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Match3.Runtime.Input
{
    public class SwapInputController : MonoBehaviour
    {
        [SerializeField] private PlayerInput input;
        [SerializeField] private BoardView boardView;
        [SerializeField] private float minSwipeDistance = 0.5f;
        [SerializeField] private float cameraDistance = 10f;

        private GameController gameController;
        private Camera camera;

        private Vector3 startPos;
        private Vector3 currentPos;

        private Position startCell;

        private InputAction click;
        private InputAction point;

        private bool isSwiping;
        private bool inputEnabled = true;

        public void Initialize(GameController controller)
        {
            gameController = controller;
            camera = Camera.main;

            click = input.actions["Click"];
            point = input.actions["Point"];
        }

        void Update()
        {
            if(!inputEnabled) return;

            currentPos = ConvertMousePos(point.ReadValue<Vector2>());

            HandlePointerDown();
            HandlePointerDrag();
            HandlePointerUp();
        }

        public void EnableInput()
        {
            inputEnabled = true;
        }

        public void DisableInput()
        {
            inputEnabled = false;
        }

        private void HandlePointerDown()
        {
            if (!click.WasPressedThisFrame()) return;

            startPos = currentPos;

            if (TryGetBoardCellFromPointer(startPos, out startCell))
            {
                isSwiping = true;
            }
        }

        private void HandlePointerDrag()
        {
            if (!isSwiping) return;

            float distance = Vector3.Distance(startPos, currentPos);
            if (distance < minSwipeDistance) return;

            Vector3 direction = ResolveSwipeDirection(startPos, currentPos);

            if (TryBuildTargetCell(startCell, direction, out Position targetCell))
            {
                gameController.OnSwapRequested(startCell, targetCell);
            }

            isSwiping = false;
        }

        private void HandlePointerUp()
        {
            if (click.WasReleasedThisFrame())
            {
                isSwiping = false;
            }
        }

        private bool TryGetBoardCellFromPointer(Vector3 worldPos, out Position cell)
        {
            cell = boardView.PositionToLocal(worldPos);

            if (!boardView.IsCellInBounds(cell))
                return false;

            return true;
        }

        private Vector3 ResolveSwipeDirection(Vector2 startPos, Vector2 currentPos)
        {
            Vector3 delta = currentPos - startPos;

            if (Mathf.Abs(delta.x) > Mathf.Abs(delta.y))
                return new Vector3(Mathf.Sign(delta.x), 0, 0);
            else
                return new Vector3(0, Mathf.Sign(delta.y), 0);
        } 

        private Vector3 ConvertMousePos(Vector2 mousePosition)
        {
            Vector3 screenPos = new Vector3(mousePosition.x, mousePosition.y, cameraDistance);
            return camera.ScreenToWorldPoint(screenPos);
        }

        private bool TryBuildTargetCell(Position cell, Vector3 direction, out Position targetCell)
        {
            int row = cell.Row - Mathf.RoundToInt(direction.y);
            int column = cell.Column + Mathf.RoundToInt(direction.x);

            targetCell = new Position(row, column);

            if (!boardView.IsCellInBounds(targetCell))
                return false;

            return true;
        }
    }
}