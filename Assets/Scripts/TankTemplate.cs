using UnityEngine;

namespace DefaultNamespace
{
    [CreateAssetMenu(fileName = "New Tank", menuName = "Tank", order = 0)]
    public class TankTemplate : ScriptableObject
    {
        public string tankName;
        public Sprite tankBodySprite, tankMuzzleSprite;
        public float steerSpeed, moveSpeed, bulletSpeed;
        public bool isSelected;
        public GameObject bullet;
    }
}