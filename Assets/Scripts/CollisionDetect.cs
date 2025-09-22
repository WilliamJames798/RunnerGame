using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionDetect : MonoBehaviour
{
    // 碰撞类型
    private enum CollisionType
    {
        Obstacle,
        Boost
    }

    // 对象类型
    [SerializeField]private CollisionType type = CollisionType.Obstacle;

    // 加速区
    [SerializeField] private float boostAmount = 30f; // 加速速度
    [SerializeField] private float boostDuration = 5f;// 加速持续时间

    private void OnCollisionEnter(Collision collision)
    {
        if(type == CollisionType.Obstacle && collision.gameObject.CompareTag("Player"))
        {
            // 处理与玩家的碰撞
            Debug.Log("碰撞到障碍物!");
        }
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (type == CollisionType.Boost && collision.gameObject.CompareTag("Player"))
        {
            // 处理加速
            Debug.Log("进入加速区!");
            PlayerMovement playerMovement = collision.GetComponent<PlayerMovement>();
            if (playerMovement != null)
            {
                // 调用玩家的加速方法
                playerMovement.StartCoroutine(playerMovement.SpeedBoost(boostAmount, boostDuration));
            }
        }
    }
}
