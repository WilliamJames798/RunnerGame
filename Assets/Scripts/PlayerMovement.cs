using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private float moveSpeed = 5f;
    private float horizontalSpeed = 2f;
    private void Update()
    {
        transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime, Space.Self);
        if (Input.GetKey(KeyCode.A))
        {
            transform.Translate(Vector3.left * horizontalSpeed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.D))
        {
            transform.Translate(Vector3.right * horizontalSpeed * Time.deltaTime);
        }
    }
}
