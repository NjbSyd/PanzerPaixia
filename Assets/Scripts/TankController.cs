using System;
using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Driver : MonoBehaviour
{
    private GameObject bullet;
    private AudioSource fireSound;
    [SerializeField] private Transform bulletPoint;
    [SerializeField] private List<TankTemplate> tankTemplates;
    [SerializeField] private GameObject tankBody, tankMuzzle;
    private TankTemplate selectedTank;
    private float currentMoveSpeed;
    private float steerSpeed;
    private float normalMoveSpeed;
    int collectedCoins = 0;
    int remainingCoins = 5;
    bool isSpeedBoosted = false;


    private void Awake()
    {
        fireSound = GetComponent<AudioSource>();
    }

    void Start()
    {
        ScoreManager.scoreManager.UpdateCollected(collectedCoins);
        ScoreManager.scoreManager.UpdateRemaining(remainingCoins);
        foreach (TankTemplate template in tankTemplates)
        {
            if (template.isSelected)
            {
                selectedTank = template;
                break;
            }
        }

        normalMoveSpeed = selectedTank.moveSpeed;
        currentMoveSpeed = normalMoveSpeed;
        steerSpeed = selectedTank.steerSpeed;
        tankBody.GetComponent<SpriteRenderer>().sprite = selectedTank.tankBodySprite;
        tankMuzzle.GetComponent<SpriteRenderer>().sprite = selectedTank.tankMuzzleSprite;
        bullet = selectedTank.bullet;
    }

    void Update()
    {
        Move();
        Fire();
    }

    void Move()
    {
        var vert = Input.GetAxis("Vertical");
        var hori = Input.GetAxis("Horizontal");

        transform.Translate(0, currentMoveSpeed * Time.deltaTime * vert, 0);
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

    void OnCollisionEnter2D(Collision2D other)
    {
        currentMoveSpeed = normalMoveSpeed;
        isSpeedBoosted = false;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Coin"))
        {
            Debug.Log("Coin was hit");
            Destroy(other.gameObject);
            collectedCoins++;
            remainingCoins--;
            ScoreManager.scoreManager.UpdateCollected(collectedCoins);
            ScoreManager.scoreManager.UpdateRemaining(remainingCoins);
            if (remainingCoins == 0)
            {
                StartCoroutine(LevelPassed());
                Debug.Log("You win!");
            }
        }
        else if (other.gameObject.CompareTag("Oilspil"))
        {
            isSpeedBoosted = false;
            Debug.Log("Entered OilSpil");
            currentMoveSpeed = normalMoveSpeed / 2;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Oilspil"))
        {
            Debug.Log("Exited OilSpil");
            currentMoveSpeed = normalMoveSpeed;
        }
        else if (other.gameObject.CompareTag("Speedup"))
        {
            if (!isSpeedBoosted)
                StartCoroutine(HandleSpeedUp());
        }
    }

    IEnumerator HandleSpeedUp()
    {
        isSpeedBoosted = true;
        currentMoveSpeed = normalMoveSpeed * 2;
        yield return new WaitForSeconds(5f);
        currentMoveSpeed = normalMoveSpeed;
        isSpeedBoosted = false;
    }

    IEnumerator LevelPassed()
    {
        yield return new WaitForSeconds(2);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}