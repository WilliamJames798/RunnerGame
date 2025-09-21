using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CoinCountUI : MonoBehaviour
{
    public static int coinCount = 0; // 金币数量
    [SerializeField] private TMPro.TMP_Text coinCountText; // 金币数量
    [SerializeField] private Image coinCountImage; // 金币图标
    private int lastCoinCount = 0; // 上一次金币数量
    private void Update()
    {
        if (coinCount != lastCoinCount)
        {
            // 更新金币数量
            coinCountText.text = "金币：" + coinCount.ToString();   
            lastCoinCount = coinCount;
        }
    }
}
