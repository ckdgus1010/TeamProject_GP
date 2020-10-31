using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//목표: GameManager의 modeID와 stageID를 받아 알맞은 유형의 문제를 제시
//modeID 0 = CreateMode
//modeID 1, 2, 3 = 혼자하기 유형 1, 2, 3
//modeId 4 = 같이하기

public class QuestManager : MonoBehaviour
{
    //GameManager의 modeID와 stageID를 받을 변수
    public int modeID = 0;
    public int stageID = 0;
    public CardBoardSetting cardBoardSetting;

    //Quest Bundle
    public List<GameObject[]> questList;
    public GameObject[] questMode01 = new GameObject[9];      //Quest Bundle 01 - 혼자하기 유형 1
    public GameObject[] questMode02 = new GameObject[9];      //Quest Bundle 02 - 혼자하기 유형 2
    public GameObject[] questMode03 = new GameObject[9];      //Quest Bundle 03 - 혼자하기 유형 3

    //같이하기 문제
    public GameObject[] questMode06 = new GameObject[9];      //Quest Bundle 06 - 같이하기 Easy
    public GameObject[] questMode07 = new GameObject[6];      //Quest Bundle 07 - 같이하기 Normal
    public GameObject[] questMode08 = new GameObject[7];      //Quest Bundle 08 - 같이하기 Hard

    //선택된 mode와 stage에 따른 문제
    public GameObject[] questModeArray = new GameObject[9];
    public GameObject currStage;

    void Start()
    {
        modeID = GameManager.Instance.modeID;
        stageID = GameManager.Instance.stageID;

        //modeID에 맞는 유형 Stage Array
        //modeID ::: 2 / 혼자하기 유형1
        //modeID ::: 3 / 혼자하기 유형2
        //modeID ::: 4 / 혼자하기 유형3
        questList = new List<GameObject[]> { questMode01, questMode02, questMode03, questMode06, questMode07, questMode08 };

        Debug.Log($"QuestManager ::: \n modeID // stageID : {modeID} // {stageID}");

        switch (modeID)
        {
            case 0:
                Debug.Log($"QuestManager ::: \n modeID = {modeID} // Create Mode");
                break;
            case 1:
                Debug.LogError($"QuestManager ::: \n modeID = {modeID} // 04 - 2 Scene에서 modeID 값을 제대로 받아오지 못했습니다.");
                break;
            case 2:
            case 3:
            case 4:
                Debug.Log($"퀘스트 메니져 :: {modeID}");
                SelectQuest(modeID, stageID);
                break;
            case 5:
                Debug.Log($"QuestManager ::: \n modeID = {modeID} // 같이하기 모드");
                break;
            case 6:
            case 7:
            case 8:
                Debug.Log($"퀘스트 메니져 :: {modeID}");
                SelectQuest(modeID - 1, stageID);
                break;
        }
    }

    void SelectQuest(int _modeID, int _stageID)
    {
        Debug.Log($"SelectQuest ::: {_modeID} // {_stageID}");
        questModeArray = questList[_modeID - 2];
        Debug.Log($"SelectQuest ::: {questList[_modeID - 2]}");

        //stageID에 맞는 문제 On
        currStage = questModeArray[_stageID - 1];
        currStage.SetActive(true);
        Debug.Log($"QuestManager ::: \n currStage // {currStage.name}");
    }

    public void ChangeQuset(int stageID)
    {
        switch (GameManager.Instance.modeID)
        {
            case 0: 
            case 1:
            case 5:
                Debug.LogError("QuestMgr :: modeID를 체크하세요");
                break;
            case 2:
            case 3:
            case 4:
                Debug.Log($"QuestManager ::: ChangeQuest() // 혼자하기 모드 {GameManager.Instance.modeID}0{stageID}");
                GameManager.Instance.stageID += 1;
                break;
            case 6:
            case 7:
            case 8:
                Debug.Log($"QuestManager ::: ChangeQuest() // 같이하기 모드 {GameManager.Instance.modeID}0{stageID}");
                GameManager.Instance.stageID = stageID;
                break;

        }
        currStage.SetActive(false);

        currStage = questModeArray[GameManager.Instance.stageID - 1];
        currStage.SetActive(true);
        Debug.Log($"QuestManager ::: \n currStage is changed // {currStage.name}");

        //cardBoardSetting.ChangeCard();
    }

}
