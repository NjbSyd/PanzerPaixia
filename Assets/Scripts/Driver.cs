using System;
using UnityEngine;
using UnityEngine.UI;

public class Driver : MonoBehaviour
{
    [SerializeField] GameObject bullet;
    [SerializeField] GameObject missionSuccess;
    [SerializeField] Transform bulletPoint;
     float steerSpeed = 100f;
    float moveSpeed = 15f;
    float normalSpeed = 15f;
    float slowSpeed = 5f;
    float boostSpeed = 30f;
    float timer = 0f;
    float maxTime = 2f;
    int remainingValuables = 2;
    bool isValuablePicked = false;
    [SerializeField] Sprite originalSprite, updatedSprite;
    SpriteRenderer bodySpriteRenderer;
    AudioSource fireSound,engineSound;

    bool isGameOver = false;

     void Awake()
     {
         engineSound = transform.GetChild(0).GetComponent<AudioSource>();
         fireSound = transform.GetChild(1).GetComponent<AudioSource>();
     }

    void Start()
    {
        bodySpriteRenderer = transform.GetChild(0).GetComponent<SpriteRenderer>();
        Debug.Log(bodySpriteRenderer.gameObject.name);
    }

    void Update()
    {
        if (!isGameOver)
        {
            EffectTimer();
            Move();
            Fire();

            if (remainingValuables == 0)
            {
                MissionSuccess();
            }
        }
    }

    void Move()
    {
        var vert = Input.GetAxis("Vertical");
        var hori = Input.GetAxis("Horizontal");

        transform.Translate(0, moveSpeed * Time.deltaTime * vert, 0);
        transform.Rotate(0, 0, steerSpeed * Time.deltaTime * -hori);
    }

    void Fire()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            fireSound.Play();
            Instantiate(bullet, bulletPoint.position, bulletPoint.rotation);
        }
    }

    void EffectTimer()
    {
        timer += Time.deltaTime;
        if (timer >= maxTime && moveSpeed != 15)
        {
            moveSpeed = normalSpeed;
            Debug.Log("Normal");
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Enter Trigger");
        if (other.gameObject.CompareTag("OilSpil"))
        {
            moveSpeed = slowSpeed;
            timer = 0f;
        }
        else if (other.gameObject.CompareTag("Speedup"))
        {
            moveSpeed = boostSpeed;
            timer = 0f;
        }
        else if (other.gameObject.CompareTag("Valuable"))
        {
            if (!isValuablePicked)
            {
                Destroy(other.gameObject, 0.1f);
                isValuablePicked = true;
                bodySpriteRenderer.sprite = updatedSprite;
            }
        }
        else if (other.gameObject.CompareTag("Pit"))
        {
            isValuablePicked = false;
            remainingValuables--;
            bodySpriteRenderer.sprite = originalSprite;
        }
    }


    void MissionSuccess()
    {
        moveSpeed = 0f;
        isGameOver = true;
        Debug.Log("Mission Success!");
        Instantiate(missionSuccess, bulletPoint.position, Quaternion.identity);
        engineSound.Stop();

    }
}