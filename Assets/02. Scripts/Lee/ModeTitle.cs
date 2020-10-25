using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ModeTitle : MonoBehaviour
{
    [SerializeField] private Text title;

    public void ChangeModeTitle()
    {
        switch (GameManager.Instance.modeID)
        {
            case 2:
                title.text = "유형01 \n 개수 맞추기";
                break;
            case 3:
                title.text = "유형02 \n 카드 보고 큐브 빼기";
                break;
            case 4:
                title.text = "유형03 \n 카드 보고 큐브 쌓기";
                break;
        }
    }
}
