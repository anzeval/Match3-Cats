using Match3.Model.Data;
using UnityEngine;

namespace Match3.Runtime.Config
{
    [CreateAssetMenu(fileName = "TileVisualRegistry", menuName = "Scriptable Objects/TileVisualRegistry")]
    public class TileVisualRegistry : ScriptableObject
    {
        public TileType tileType;
        public Sprite sprite;
        public GameObject prefab;
    }
}