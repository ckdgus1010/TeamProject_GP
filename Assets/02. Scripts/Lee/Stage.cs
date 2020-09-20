using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Stage : MonoBehaviour
{
    public AnswerMgr answerMgr;
    public GameObject[] stageArray = new GameObject[5];

    public int num = 0;
    public GameObject currStage;

    private void Awake()
    {
        num = 0;
        currStage = stageArray[0];
    }

    public void ShowStage()
    {
        int _stageId = answerMgr.stageId - 1;
        Debug.Log($"_stageId = {_stageId}");

        if (_stageId != num)
        {
            Debug.Log($"_stageId // num ::: {_stageId} // {num}");
            currStage.SetActive(false);
            currStage = null;

            num = _stageId;
            Debug.Log("num 변경");
        }

        stageArray[_stageId].SetActive(true);
        Debug.Log($"stageArray [{_stageId}]번째로 변경");
    }
}

