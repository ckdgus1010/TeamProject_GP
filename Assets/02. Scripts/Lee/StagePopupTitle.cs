using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StagePopupTitle : MonoBehaviour
{
    [SerializeField]
    private Text title;

    public void ChangeTitleText()
    {
        int modeID = GameManager.Instance.modeID - 2;
        title.text = GameManager.Instance.stageTitleArray[modeID];
    }
}
