using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager scoreManager;
    [SerializeField] public Text collectedCoins, remainingCoins, pickedCoins;
    [SerializeField] public TextMeshProUGUI levelText;


    private void Awake()
    {
        scoreManager = this;
        levelText.text = "Level:" + SceneManager.GetActiveScene().buildIndex;
    }

    public void UpdateCollected(int score)
    {
        collectedCoins.text = "Collected: " + score;
    }


    public void UpdateRemaining(int score)
    {
        remainingCoins.text = "Remaining: " + score;
    }

    public void UpdatePicked(int score)
    {
        pickedCoins.text = "Picked: " + score;
    }
}