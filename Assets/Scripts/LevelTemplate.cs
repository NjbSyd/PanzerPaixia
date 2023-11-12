using UnityEngine;

namespace DefaultNamespace
{
    [CreateAssetMenu(fileName = "Level_", menuName = "ScriptableObjects/Level", order = 0)]
    public class LevelTemplate : ScriptableObject
    {
        public GameObject SandbagBeige, SandbagBrown;
        public int sandBagAmount;
    }
}