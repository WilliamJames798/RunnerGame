using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SegmentGenerator : MonoBehaviour
{
    [SerializeField] private Transform playerTransform; // 玩家位置
    [SerializeField] private GameObject[] segments; // 路段数组
    [SerializeField] private float segmentLength = 100f; // 每个路段的长度
    [SerializeField] private int segmentCount = 2; // 初始生成数量
    [SerializeField] private float spawnZ = 0f; // 下一个路段生成位置
    [SerializeField] private int maxSegments = 8; // 最大路段数量
    void Start()
    {
        // 生成初始路段
        GameObject startSegment = Instantiate(segments[0], new Vector3(0, 0, spawnZ), Quaternion.identity);
        startSegment.transform.SetParent(transform, false);
        spawnZ += segmentLength;

        // 生成更多路段
        for (int i = 1; i < segmentCount; i++)
        {
            SpawnSegment();
        }
    }

    void Update()
    {
        // 当玩家接近最后一个路段时生成新路段
        if (playerTransform.position.z > spawnZ - (segmentCount * segmentLength))
        {
            SpawnSegment();

            // 删除最远的路段以节省资源
            if (transform.childCount > maxSegments)
            {
                Destroy(transform.GetChild(0).gameObject);
            }
        }
    }

    private void SpawnSegment()
    {
        // 从路段数组中随机选择一个路段
        int index = UnityEngine.Random.Range(1, segments.Length);
        GameObject prefab = segments[index];
        
        // 生成路段
        GameObject obj = Instantiate(prefab, new Vector3(0, 0, spawnZ), Quaternion.identity);

        // 将生成的路段设置为SegmentGenerator的子对象
        obj.transform.SetParent(transform, false);

        // 更新下一个路段生成位置
        spawnZ += segmentLength;
    }
}
