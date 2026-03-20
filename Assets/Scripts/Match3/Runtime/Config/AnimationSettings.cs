using UnityEngine;

namespace Match3.Runtime.Config
{
    [CreateAssetMenu(fileName = "AnimationSettings", menuName = "Scriptable Objects/AnimationSettings")]
    public class AnimationSettings : ScriptableObject
    {
        public float swapDuration;
        public float fallDuration;
        public float clearDelay;
        public float spawnDelay;
    }
}
