using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance;
    [SerializeField] private GameObject explosionAnimation, levelEnd;

    private AudioSource fireSound, moveSound, standBySound, explosionSound, coinSound;

    private void Awake()
    {
        Instance = this;
        var sources = GetComponents<AudioSource>();
        fireSound = sources[0];
        moveSound = sources[1];
        standBySound = sources[2];
        explosionSound = sources[3];
        coinSound = sources[4];
    }

    public void PlayExplosionSound(Vector3 position)
    {
        explosionSound.Play();
        StartCoroutine(PlayExplodeAnimation(position));
    }

    public bool IsMoveSoundPlaying()
    {
        return moveSound.isPlaying;
    }

    public void PlayFireSound()
    {
        fireSound.Play();
    }

    public void PlayMoveSound()
    {
        moveSound.Play();
    }

    public void LevelEnded(Vector3 position)
    {
        StopAllSounds();
        StartCoroutine(LevelEndedPopup(position));
    }


    public void StopMoveSound()
    {
        moveSound.Stop();
    }

    public void PlayCoinSound()
    {
        coinSound.Play();
    }

    public void StopAllSounds()
    {
        moveSound.Stop();
        standBySound.Stop();
        coinSound.Stop();
        explosionSound.Stop();
        fireSound.Stop();
    }

    private IEnumerator PlayExplodeAnimation(Vector3 position)
    {
        yield return new WaitForSeconds(0.2f);
        var explosion = Instantiate(explosionAnimation, position, Quaternion.identity);
        yield return new WaitForSeconds(0.7f);
        Destroy(explosion);
    }

    private IEnumerator LevelEndedPopup(Vector3 position)
    {
        var levelEndPopup = Instantiate(levelEnd, position, Quaternion.identity);
        var levelEndAudio = levelEndPopup.GetComponent<AudioSource>();
        yield return new WaitUntil(() => !levelEndAudio.isPlaying);
        Destroy(levelEndPopup, 1f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}