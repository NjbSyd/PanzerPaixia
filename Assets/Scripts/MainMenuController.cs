using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayButtonScript : MonoBehaviour
{
    [SerializeField] public AudioSource emptyGameObject;

    public void PlayGame()
    {
        StartCoroutine(ButtonClickSoundAndAction("Level_1"));
    }

    public void CreditsLoad()
    {
        StartCoroutine(ButtonClickSoundAndAction("Credits"));
    }

    private IEnumerator ButtonClickSoundAndAction(string sceneName, int index = 0)
    {
        emptyGameObject.Play();
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene(sceneName);
    }
}