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

    private List<int>[] playerAnswerArray = new List<int>[3];

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

    public List<int>[] MakePlayerAnswerArray()
    {
        CBCtrl cbCtrl = currCB.GetComponent<CBCtrl>();
        playerAnswerArray = cbCtrl.MakePlayerAnswerArray();

        return playerAnswerArray;
    }
}
