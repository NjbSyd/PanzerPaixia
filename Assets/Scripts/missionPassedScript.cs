using UnityEngine;

public class missionPassedScript : MonoBehaviour
{
     void Start()
    {
        gameObject.GetComponent<AudioSource>().Play();
    }
}
