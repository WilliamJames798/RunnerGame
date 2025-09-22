using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SegmentGenerator : MonoBehaviour
{
    [SerializeField] private Transform playerTransform; // ���λ��
    [SerializeField] private GameObject[] segments; // ·������
    [SerializeField] private float segmentLength = 100f; // ÿ��·�εĳ���
    [SerializeField] private int segmentCount = 2; // ��ʼ��������
    [SerializeField] private float spawnZ = 0f; // ��һ��·������λ��
    [SerializeField] private int maxSegments = 8; // ���·������
    void Start()
    {
        // ���ɳ�ʼ·��
        GameObject startSegment = Instantiate(segments[0], new Vector3(0, 0, spawnZ), Quaternion.identity);
        startSegment.transform.SetParent(transform, false);
        spawnZ += segmentLength;

        // ���ɸ���·��
        for (int i = 1; i < segmentCount; i++)
        {
            SpawnSegment();
        }
    }

    void Update()
    {
        // ����ҽӽ����һ��·��ʱ������·��
        if (playerTransform.position.z > spawnZ - (segmentCount * segmentLength))
        {
            SpawnSegment();

            // ɾ����Զ��·���Խ�ʡ��Դ
            if (transform.childCount > maxSegments)
            {
                Destroy(transform.GetChild(0).gameObject);
            }
        }
    }

    private void SpawnSegment()
    {
        // ��·�����������ѡ��һ��·��
        int index = UnityEngine.Random.Range(1, segments.Length);
        GameObject prefab = segments[index];
        
        // ����·��
        GameObject obj = Instantiate(prefab, new Vector3(0, 0, spawnZ), Quaternion.identity);

        // �����ɵ�·������ΪSegmentGenerator���Ӷ���
        obj.transform.SetParent(transform, false);

        // ������һ��·������λ��
        spawnZ += segmentLength;
    }
}
