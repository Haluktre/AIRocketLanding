using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    // Start is called before the first frame update
    public Transform Rocket;
    float Distance;

    private void Start() {
        Distance = Mathf.Abs(Rocket.transform.position.y - transform.position.y);
    }
    private void Update() {
        transform.position = new Vector3(transform.position.x, Rocket.transform.position.y + Distance, transform.position.z);
    }
}
