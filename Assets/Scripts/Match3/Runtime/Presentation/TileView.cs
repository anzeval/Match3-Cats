using Match3.Model.Data;
using UnityEngine;

namespace Match3.Runtime.Presentation
{
   public class TileView : MonoBehaviour
    {
        public TileType TileType {get; private set;}
        public Position Position {get; private set;}

        public bool isSelected {get; private set;}
        public bool isHinted {get; private set;}

        private TileDatabase tileDatabase;
        private SpriteRenderer spriteRenderer;

        void Awake()
        {
            spriteRenderer = GetComponent<SpriteRenderer>();
        }

        public void Init(TileDatabase tileDatabase, Position position)
        {
            this.tileDatabase = tileDatabase; 
            Position = position;
            isSelected = false;
            isHinted = false;
        }

        public void SetTileType(TileType type)
        {
            TileType = type;
            Sprite sprite = tileDatabase.GetSprite(type);
            spriteRenderer.sprite = sprite;
        }

        public void SetPosition(Position position)
        {
            Position = position;
        }

        public void SetPositionInstant(Vector3 worldPos)
        {
            transform.position = worldPos;
        }

        public Vector3 GetWorldPosition()
        {
            return transform.position;
        }
    }
}
