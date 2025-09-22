using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionDetect : MonoBehaviour
{
    // ��ײ����
    private enum CollisionType
    {
        Obstacle,
        Boost
    }

    // ��������
    [SerializeField]private CollisionType type = CollisionType.Obstacle;

    // ������
    [SerializeField] private float boostAmount = 30f; // �����ٶ�
    [SerializeField] private float boostDuration = 5f;// ���ٳ���ʱ��

    private void OnCollisionEnter(Collision collision)
    {
        if(type == CollisionType.Obstacle && collision.gameObject.CompareTag("Player"))
        {
            // ��������ҵ���ײ
            Debug.Log("��ײ���ϰ���!");
        }
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (type == CollisionType.Boost && collision.gameObject.CompareTag("Player"))
        {
            // �������
            Debug.Log("���������!");
            PlayerMovement playerMovement = collision.GetComponent<PlayerMovement>();
            if (playerMovement != null)
            {
                // ������ҵļ��ٷ���
                playerMovement.StartCoroutine(playerMovement.SpeedBoost(boostAmount, boostDuration));
            }
        }
    }
}
