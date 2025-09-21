using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CoinCountUI : MonoBehaviour
{
    public static int coinCount = 0; // �������
    [SerializeField] private TMPro.TMP_Text coinCountText; // �������
    [SerializeField] private Image coinCountImage; // ���ͼ��
    private int lastCoinCount = 0; // ��һ�ν������
    private void Update()
    {
        if (coinCount != lastCoinCount)
        {
            // ���½������
            coinCountText.text = "��ң�" + coinCount.ToString();   
            lastCoinCount = coinCount;
        }
    }
}
