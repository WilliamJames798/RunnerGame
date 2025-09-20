using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class CameraMovement : MonoBehaviour
{
    [SerializeField] private Transform playerTransform; // ����Ŀ��
    private Vector3 offset; // ƫ����
    private float smoothSpeed = 5f; // ƽ���ٶ�

    // ��������֮��ĳ�ʼ��Ծ���
    private void Start()
    {
        offset = transform.position - playerTransform.position;
    }

    private void LateUpdate()
    {
        // ����Ŀ��λ��
        Vector3 desiredPosition = playerTransform.position + offset;

        // ƽ����ֵ�ƶ���Ŀ��λ��
        transform.position = Vector3.Lerp(transform.position, desiredPosition, Time.deltaTime * smoothSpeed);

    }
}
