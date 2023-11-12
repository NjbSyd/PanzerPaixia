using UnityEngine;

public class BulletController : MonoBehaviour
{
    private readonly float _bulletSpeed = 700f;
    private Rigidbody2D _rigidbody2D;

    private void Awake()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _rigidbody2D.AddRelativeForce(new Vector2(0, _bulletSpeed));
    }

    private void Start()
    {
        Destroy(gameObject, 10f);
    }

    private void Update()
    {
        _rigidbody2D.AddRelativeForce(new Vector2(0, 10));
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player")) return;

        Destroy(gameObject);
    }
}