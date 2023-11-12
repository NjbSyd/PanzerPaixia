using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;

public class Driver : MonoBehaviour
{
    [SerializeField] private Transform bulletPoint, centerOfMap;
    [SerializeField] private List<TankTemplate> tankTemplates;
    [SerializeField] private GameObject tankBody, tankMuzzle;
    [SerializeField] private LevelTemplate levelTemplate;
    private GameObject bullet;
    private int collectedCoins, remainingCoins, pickedCoins;
    private float currentMoveSpeed;
    private bool isSpeedBoosted, levelPassed;
    private float normalMoveSpeed;
    private TankTemplate selectedTank;
    private SoundManager soundManager;
    private float steerSpeed;


    private void Awake()
    {
        soundManager = SoundManager.Instance;
        collectedCoins = pickedCoins = 0;
    }

    private void Start()
    {
        StartCoroutine(InstantiateLevel());
        remainingCoins = levelTemplate.sandBagAmount;
        ScoreManager.scoreManager.UpdateCollected(collectedCoins);
        ScoreManager.scoreManager.UpdateRemaining(remainingCoins);
        ScoreManager.scoreManager.UpdatePicked(pickedCoins);
        foreach (var template in tankTemplates)
            if (template.isSelected)
            {
                selectedTank = template;
                break;
            }

        normalMoveSpeed = selectedTank.moveSpeed;
        currentMoveSpeed = normalMoveSpeed;
        steerSpeed = selectedTank.steerSpeed;
        tankBody.GetComponent<SpriteRenderer>().sprite = selectedTank.tankBodySprite;
        tankMuzzle.GetComponent<SpriteRenderer>().sprite = selectedTank.tankMuzzleSprite;
        bullet = selectedTank.bullet;
    }

    private void Update()
    {
        if (!levelPassed)
        {
            Move();
            Fire();
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        currentMoveSpeed = normalMoveSpeed;
        isSpeedBoosted = false;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Coin"))
        {
            soundManager.PlayCoinSound();
            Destroy(other.gameObject);
            pickedCoins++;
            ScoreManager.scoreManager.UpdatePicked(pickedCoins);
        }
        else if (other.gameObject.CompareTag("Oilspil"))
        {
            isSpeedBoosted = false;
            currentMoveSpeed = normalMoveSpeed / 2;
        }
        else if (other.gameObject.CompareTag("Finish"))
        {
            collectedCoins += pickedCoins;
            remainingCoins -= pickedCoins;
            pickedCoins = 0;
            ScoreManager.scoreManager.UpdateCollected(collectedCoins);
            ScoreManager.scoreManager.UpdateRemaining(remainingCoins);
            ScoreManager.scoreManager.UpdatePicked(pickedCoins);
            if (remainingCoins == 0) StartCoroutine(LevelPassed());
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Oilspil"))
            currentMoveSpeed = normalMoveSpeed;
        else if (other.gameObject.CompareTag("Speedup"))
            if (!isSpeedBoosted)
                StartCoroutine(HandleSpeedUp());
    }

    private void Move()
    {
        var verticalInput = Input.GetAxis("Vertical");
        var horizontalInput = Input.GetAxis("Horizontal");
        if (verticalInput != 0)
        {
            if (!soundManager.IsMoveSoundPlaying()) soundManager.PlayMoveSound();
        }
        else
        {
            soundManager.StopMoveSound();
        }

        transform.Translate(0, currentMoveSpeed * Time.deltaTime * verticalInput, 0);
        transform.Rotate(0, 0, steerSpeed * Time.deltaTime * -horizontalInput);
    }

    private void Fire()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            soundManager.PlayFireSound();
            Instantiate(bullet, bulletPoint.position, bulletPoint.rotation);
        }
    }

    private IEnumerator HandleSpeedUp()
    {
        isSpeedBoosted = true;
        currentMoveSpeed = normalMoveSpeed * 2;
        yield return new WaitForSeconds(5f);
        currentMoveSpeed = normalMoveSpeed;
        isSpeedBoosted = false;
    }

    private IEnumerator LevelPassed()
    {
        levelPassed = true;
        currentMoveSpeed = steerSpeed = 0;
        soundManager.LevelEnded(gameObject.transform.position);
        yield return new WaitForSeconds(1);
    }

    private IEnumerator InstantiateLevel()
    {
        var occupiedPositions = new List<Vector2>();

        for (var i = 0; i < levelTemplate.sandBagAmount; i++)
        {
            var randomPosition = GetRandomPosition(occupiedPositions);

            var sandbag =
                Instantiate(Random.Range(0, 2) == 0 ? levelTemplate.SandbagBeige : levelTemplate.SandbagBrown);
            sandbag.transform.position = randomPosition;
            occupiedPositions.Add(randomPosition);
            yield return new WaitForSeconds(0.1f);
        }
    }

    private Vector2 GetRandomPosition(List<Vector2> occupiedPositions)
    {
        var center = centerOfMap.position;
        float x = center.x, y = center.y;

        do
        {
            var randomPosition = new Vector2(Random.Range(x - 18, x + 18), Random.Range(y - 8, y + 8));
            var positionIsValid = Physics2D.OverlapCircleAll(randomPosition, 2.5f).Length <= 0 &&
                                  !occupiedPositions.Contains(randomPosition);
            if (positionIsValid) return randomPosition;
        } while (true);
    }
}