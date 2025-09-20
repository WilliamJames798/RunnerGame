using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class CameraMovement : MonoBehaviour
{
    [SerializeField] private Transform playerTransform; // 跟随目标
    private Vector3 offset; // 偏移量
    private float smoothSpeed = 5f; // 平滑速度

    // 相机和玩家之间的初始相对距离
    private void Start()
    {
        offset = transform.position - playerTransform.position;
    }

    private void LateUpdate()
    {
        // 计算目标位置
        Vector3 desiredPosition = playerTransform.position + offset;

        // 平滑插值移动到目标位置
        transform.position = Vector3.Lerp(transform.position, desiredPosition, Time.deltaTime * smoothSpeed);

    }
}
