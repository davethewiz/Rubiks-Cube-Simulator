using UnityEngine;
using System.Collections;

public class CameraOrbit : MonoBehaviour {
    public Transform target;
    public float distance = 10;
    public float xSpeed = 20;
    public float ySpeed = 20;

    float x = 0;
    float y = 0;

    // Use this for initialization
    void Start() {
        x = transform.eulerAngles.y;
        y = transform.eulerAngles.x;
    }

    void LateUpdate() {
        if (Input.GetKey(KeyCode.Space)) {
            x += Input.GetAxis("Mouse X") * xSpeed * distance * 0.02f;
            y -= Input.GetAxis("Mouse Y") * ySpeed * distance * 0.02f;
        }

        Quaternion rotation = Quaternion.Euler(y, x, 0);

        Vector3 negDistance = new Vector3(0.0f, 0.0f, -distance);
        Vector3 position = rotation * negDistance + target.position;

        transform.rotation = rotation;
        transform.position = position;
    }
}