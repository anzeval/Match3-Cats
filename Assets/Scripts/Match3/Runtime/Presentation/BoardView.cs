using Match3.Model.Board;
using Match3.Model.Data;
using System.Collections.Generic;
using UnityEngine;

namespace Match3.Runtime.Presentation
{
    public class BoardView : MonoBehaviour
    {
        [SerializeField] private TileDatabase tileDatabase;
        [SerializeField] private float cellSize = 1f;
        [SerializeField] private float spacing = 1f;
        
        private Vector2 origin;
        private BoardState board;
        private TileView[,] grid;

        private int boardHeight;
        private int boardWidth;

        public void Build(BoardState board)
        {
            origin = new Vector2(transform.position.x, transform.position.y);
            this.board = board;
            boardHeight = board.Height;
            boardWidth = board.Width;
            grid = new TileView[boardHeight, boardWidth];

            CreateVisualGridFromBoard();
        }

        public void Rebuild()
        {
            ClearAll();
            Build(board);
        }

        public TileView GetTileView(Position position)
        {
            if(board == null) return null;

            return board.InBounds(position)? grid[position.Row, position.Column] : null;
        }

        public void SetTileView(Position position, TileView tile)
        {
            grid[position.Row, position.Column] = tile;
        }

        public void RemoveTileView(Position position)
        {
            grid[position.Row, position.Column] = null;
        }

        public TileView CreateTileView(Position position, TileType tileType)
        {
            if (grid[position.Row, position.Column] != null)
            {
                return null;
            }

            GameObject prefab = tileDatabase.GetPrefab(tileType);
            TileView tileView = Instantiate(prefab, PositionToWorld(position), Quaternion.identity, transform).GetComponent<TileView>();

            tileView.Init(tileDatabase, position);
            tileView.SetTileType(tileType);

            grid[position.Row, position.Column] = tileView;
            return tileView;
        }

        public void MoveTileReference(Position from, Position to)
        {
            TileView tileView = grid[from.Row, from.Column];
            SetTileView(to, tileView);
            RemoveTileView(from);
        }

        public void ClearAll()
        {
            if(board == null) return;

            for (int row = 0; row < boardHeight; row++)
            {
                for (int column = 0; column < boardWidth; column++)
                {
                    if(grid[row,column] != null)
                        Destroy(grid[row,column].gameObject);
                }
            }

            grid = new TileView[boardHeight, boardWidth];
        }

        public Vector3 PositionToWorld(Position pos)
        {
            float x = origin.x + ((cellSize + spacing)* pos.Column);
            float y = origin.y - ((cellSize + spacing)* pos.Row);

            return new Vector3(x, y, transform.position.z);
        }

        public Position PositionToLocal(Vector3 pos)
        {   
            float step = cellSize + spacing;

            float dx = pos.x - origin.x;
            float dy = origin.y - pos.y;

            int column = Mathf.RoundToInt(dx / step);
            int row = Mathf.RoundToInt(dy / step);

            return new Position(row, column);
        }

        private void CreateVisualGridFromBoard()
        {
            for (int row = 0; row < boardHeight; row++)
            {
                for (int column = 0; column < boardWidth; column++)
                {
                    TileType tileType = board.Get(new Position(row,column));
                    CreateTileView(new Position(row, column), tileType);
                }
            }
        }

        public void ApplyMoveReferences(List<MoveResult> moves)
        {
            TileView[,] original = (TileView[,])grid.Clone();
            TileView[,] next = (TileView[,])grid.Clone();

            foreach (var move in moves)
            {
                next[move.From.Row, move.From.Column] = null;
            }

            foreach (var move in moves)
            {
                TileView tileView = original[move.From.Row, move.From.Column];
                next[move.To.Row, move.To.Column] = tileView;
            }

            grid = next;
        }

        public void SwapTileReferences(Position a, Position b)
        {
            TileView first = grid[a.Row, a.Column];
            TileView second = grid[b.Row, b.Column];

            grid[a.Row, a.Column] = second;
            grid[b.Row, b.Column] = first;
        }

        public bool IsCellInBounds(Position cell)
        {
            return board.InBounds(cell);
        }
    }
}