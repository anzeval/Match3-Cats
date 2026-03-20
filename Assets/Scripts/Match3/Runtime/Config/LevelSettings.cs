using UnityEngine;

namespace Match3.Runtime.Config
{
    [CreateAssetMenu(fileName = "LevelSettings", menuName = "Scriptable Objects/LevelSettings")]
    public class LevelSettings : ScriptableObject
    {
        public int height;
        public int width;
        public int movesAllowed;
        public int targetScore;
        public int scorePerTile = 40;
    }
}