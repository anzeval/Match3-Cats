using Match3.Model.Data;
using Match3.Runtime.Config;
using UnityEngine;

public class TileDatabase : MonoBehaviour
{
    [SerializeField] private TileVisualRegistry[] tileVisuals;

    public TileVisualRegistry[] GetTileVisuals()
    {
        return tileVisuals;
    }

    public Sprite GetSprite(TileType tileType)
    {
        foreach (var tile in tileVisuals)
        {
            if(tile.tileType == tileType) return tile.sprite;
        }

        return null;
    }

    public GameObject GetPrefab(TileType tileType)
    {
        foreach (var tile in tileVisuals)
        {
            if(tile.tileType == tileType) return tile.prefab;
        }

        return null;
    }    
}
