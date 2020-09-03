using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CheckBoardMgr : MonoBehaviour
{
    public Slider gridSizeSlider;
    public GameObject[] cbArray = new GameObject[5];
    private int num;
    private GameObject currCB;

    private void Start()
    {
        num = 0;
        currCB = cbArray[0];
    }

    public void CBSize()
    {
        if ((int)gridSizeSlider.value != num)
        {
            currCB.SetActive(false);
            currCB = null;

            num = (int)gridSizeSlider.value;
        }

        int cbSize = (int)gridSizeSlider.value - (int)gridSizeSlider.minValue;
        currCB = cbArray[cbSize];
        currCB.SetActive(true);
    }

    public void CheckingAnswer()
    {
        CBCtrl cbCtrl = currCB.GetComponent<CBCtrl>();
        cbCtrl.CollectResult();
    }
}
