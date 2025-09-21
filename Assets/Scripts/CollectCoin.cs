using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectCoin : MonoBehaviour
{
    [SerializeField] private AudioSource coinFX; // ���ʰȡ��Ч
    private void OnTriggerEnter(Collider other)
    {
        coinFX.Play(); // ������Ч
        CoinCountUI.coinCount++; // ���ӽ������
        this.gameObject.SetActive(false); // ���ؽ��
    }
}
