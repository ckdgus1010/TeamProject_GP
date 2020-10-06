using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class CheckBoardMgr : MonoBehaviour
{
    public Slider gridSizeSlider;
    public GameObject[] cbArray = new GameObject[5];
    private int num;
    public GameObject currCB;

    private List<int>[] playerAnswerArray = new List<int>[3];

    private void Start()
    {
        num = 0;
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

    // 플레이어가 작성한 정답을 가져옴
    public List<int>[] MakePlayerAnswerArray()
    {
        currCB = cbArray[(int)gridSizeSlider.value - (int)gridSizeSlider.minValue];
        CBCtrl cbCtrl = currCB.GetComponent<CBCtrl>();

        Debug.Log($"currCB = {currCB}");
        int arraySize = (int)gridSizeSlider.value * (int)gridSizeSlider.value;
        playerAnswerArray = cbCtrl.MakePlayerAnswerArray(arraySize);

        return playerAnswerArray;
    }
}
