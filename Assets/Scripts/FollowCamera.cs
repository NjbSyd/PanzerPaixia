using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    [SerializeField] private GameObject tank;

    // Update is called once per frame
    private void LateUpdate()
    {
        transform.position = new Vector3(tank.transform.position.x, tank.transform.position.y, -13);
    }
}