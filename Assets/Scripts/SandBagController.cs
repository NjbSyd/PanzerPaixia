using UnityEngine;
using UnityEngine.SceneManagement;

public class SandBagController : MonoBehaviour
{
    [SerializeField] private Sprite damagedSandBag;
    private int health;
    private SoundManager soundManager;

    private void Awake()
    {
        soundManager = SoundManager.Instance;
    }

    private void Start()
    {
        var currentLevel = SceneManager.GetActiveScene().buildIndex;
        health = currentLevel == 1 ? 1 : 2;
    }


    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Bullet"))
        {
            health--;
            if (health == 0)
            {
                var position = gameObject.transform.position;
                soundManager.PlayExplosionSound(position);
                Destroy(gameObject, 0.3f);
            }
            else
            {
                GetComponent<SpriteRenderer>().sprite = damagedSandBag;
            }
        }
    }
}