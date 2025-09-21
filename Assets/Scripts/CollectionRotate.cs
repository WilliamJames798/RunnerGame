using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectionRotate : MonoBehaviour
{
    [SerializeField] private float rotationSpeed = 180f; // 旋转速度

    private void Update()
    {
        // 绕Y轴旋转
        transform.Rotate(0, rotationSpeed * Time.deltaTime, 0, Space.World);
    }
}
