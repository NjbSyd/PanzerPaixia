using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    private readonly float _bulletSpeed = 700f;
    private Rigidbody2D _rigidbody2D;

    void Awake()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _rigidbody2D.AddRelativeForce(new Vector2(0, _bulletSpeed));
    }

    private void Start()
    {
        Destroy(gameObject, 10f);
    }

    void Update()
    {
        _rigidbody2D.AddRelativeForce(new Vector2(0, 10));
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log("Player was hit");
            return;
        }

        if (other.gameObject.CompareTag("Sandbag"))
        {
            Debug.Log("Sandbag was hit");
            Destroy(other.gameObject);
        }

        Destroy(gameObject);
    }
}