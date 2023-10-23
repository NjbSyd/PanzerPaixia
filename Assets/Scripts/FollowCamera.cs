using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    [SerializeField]
    GameObject tank;

    // Update is called once per frame
    void LateUpdate()
    {
        transform.position = new Vector3(tank.transform.position.x, tank.transform.position.y, -13);
    }
}
