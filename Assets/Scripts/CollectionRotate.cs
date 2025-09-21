using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectionRotate : MonoBehaviour
{
    [SerializeField] private float rotationSpeed = 180f; // ��ת�ٶ�

    private void Update()
    {
        // ��Y����ת
        transform.Rotate(0, rotationSpeed * Time.deltaTime, 0, Space.World);
    }
}
