using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectCoin : MonoBehaviour
{
    [SerializeField] private AudioSource coinFX; // 金币拾取音效
    private void OnTriggerEnter(Collider other)
    {
        coinFX.Play(); // 播放音效
        this.gameObject.SetActive(false); // 隐藏金币
    }
}
