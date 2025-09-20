using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float horizontalSpeed = 2f;
    [SerializeField] private float leftLimit = -9f; // 游戏区域左边界
    [SerializeField] private float rightLimit = 9f; // 游戏区域右边界

    private void Update()
    {
        transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime, Space.Self);
        // 向左移动
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            if (transform.position.x > leftLimit)
            {
                transform.Translate(Vector3.left * horizontalSpeed * Time.deltaTime);
            }
        }
        // 向右移动
        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            if (transform.position.x < rightLimit)
            {
                transform.Translate(Vector3.right * horizontalSpeed * Time.deltaTime);
            }
        }
    }
}
