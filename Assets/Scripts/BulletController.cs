using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    float bulletSpeed = 1000f;
    Rigidbody2D _rigidbody2D;
    [SerializeField] private Sprite sandBagBlastSprite_1, sandBagBlastSprite_2;

    void Awake()
    {
        _rigidbody2D=GetComponent<Rigidbody2D>();
        _rigidbody2D.AddRelativeForce(new Vector2(0, bulletSpeed));
        Destroy(gameObject, 10f);
    }

    void Update()
    {
        _rigidbody2D.AddRelativeForce(new Vector2(0,10));
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Sandbag"))
        {
            other.gameObject.GetComponent<SpriteRenderer>().sprite = sandBagBlastSprite_1;
            Destroy(other.gameObject, 0.1f);
            other.gameObject.GetComponent<SpriteRenderer>().sprite = sandBagBlastSprite_2;
            Destroy(gameObject, 0.1f);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}