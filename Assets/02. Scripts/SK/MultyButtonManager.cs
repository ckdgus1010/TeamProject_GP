using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultyButtonManager : MonoBehaviour
{
    public GameObject settingCanvas;
    public void OnClickSetting()
    {
        settingCanvas.SetActive(!settingCanvas.activeSelf);
    }
}
