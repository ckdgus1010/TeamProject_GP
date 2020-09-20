using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.UI;

public class AnswerManager : MonoBehaviour
{
    //정답 정보를 가지고 있는 Script
    public AnswerData01 answerData01;
    public AnswerData02 answerData02;
    public AnswerData03 answerData03;

    //정답 정보를 가지고 있는 Array ( front = 0, side = 1, top = 2 )
    public List<int>[] answerArray;
    private enum CardDirection { front, side, top };

    //정답, 오답 패널
    public RectTransform oPanel;
    public RectTransform xPanel;
    public GameObject blurredImage;

    //정답, 오답 확인 여부
    public bool isChecked = false;
    public float lerpSpeed = 10.0f;

    //패널의 위치
    [HideInInspector]
    public RectTransform currPanel;
    public RectTransform startPos;
    private Vector2 endPos;

    private void Start()
    {
        answerArray = new List<int>[3];

        currPanel = null;
        endPos = Vector2.zero;
        Debug.Log($"AnswerManager ::: \n answerArray.Length // {answerArray.Length}");
    }

    private void Update()
    {
        if (currPanel != null)
        {
            Vector2 posSP = isChecked ? endPos : startPos.anchoredPosition;
            currPanel.anchoredPosition = Vector2.Lerp(currPanel.anchoredPosition, posSP, Time.deltaTime * lerpSpeed);
        }
    }

    //혼자하기 - 유형1 정답 확인을 위한 method
    public void CompareAnswer_Int(int playerAnswer)
    {
        //modeID == 2
        int modeID = GameManager.Instance.modeID;
        int stageID = GameManager.Instance.stageID;

        //AnswerData Script에서 정답 가져오기
        int answer = answerData01.answerArray01[stageID - 1];

        //정답일 때
        if (playerAnswer == answer)
        {
            Debug.Log($"AnswerManager ::: \n Mode 0{modeID} - Stage 0{stageID} // 정답입니다.");

            UpdateClearData(modeID, stageID);
            currPanel = oPanel;
        }
        //오답일 때
        else
        {
            Debug.Log($"AnswerManager ::: \n Mode 0{modeID} - Stage 0{stageID} ::: 틀렸습니다. 다시 생각해보세요.");

            currPanel = xPanel;
        }

        isChecked = true;
    }


    //혼자하기 - 유형2 / 유형3 정답 확인을 위한 method
    public void CompareAnswer_Array(List<int>[] playerAnswerArray)
    {
        //modeID에 따라 AnswerData Script에서 정답 가져오기
        int modeID = GameManager.Instance.modeID;
        int stageID = GameManager.Instance.stageID;

        if (modeID == 3)
        {
            for (int i = 0; i < answerArray.Length; i++)
            {
                answerArray[i] = answerData02.ChooseAnswerList(stageID)[i];
            }
        }
        else if (modeID == 4)
        {
            for (int i = 0; i < answerArray.Length; i++)
            {
                answerArray[i] = answerData03.ChooseAnswerList(stageID)[i];
            }
        }

        //정답 확인
        CompareLists(playerAnswerArray, answerArray, modeID, stageID);
    }


    //정답 확인 - playerAnswerList와 answerList의 길이 및 요소 비교
    void CompareLists(List<int>[] _playerAnswerArray, List<int>[] _answerArray, int _modeID, int _stageID)
    {
        //정답 확인용
        //위, 앞, 옆 정답 확인 시
        //문제 카드와 일치하면 count += 1, 그렇지 않으면 count += 0
        //count = 3 이면 정답, 아니면 오답
        int count = 0;

        //위, 앞, 옆 정답 확인
        for (int i = 0; i < _answerArray.Length; i++)
        {
            //1. 두 List의 길이 비교
            if (_playerAnswerArray[i].Count != _answerArray[i].Count)
            {
                bool isCountSame = false;

                Debug.Log($"AnswerManager ::: \n {(CardDirection)i} isCountSame ::: {isCountSame}");
                Debug.Log($"AnswerManager ::: \n playerAnswerList[{i}].Count // answerList[{i}].Count ::: {_playerAnswerArray[i].Count} // {_answerArray[i].Count}");
            }
            //2. 두 List의 요소 비교
            else
            {
                bool isSequenceSame = _playerAnswerArray[i].SequenceEqual(_answerArray[i]);

                //정답일 때
                if (isSequenceSame == true)
                {
                    Debug.Log($"AnswerManager ::: \n {(CardDirection)i} // {isSequenceSame} ::: 정답입니다.");
                    count += 1;
                }
                //오답일 때
                else
                {
                    Debug.Log($"AnswerManager ::: \n {(CardDirection)i} // {isSequenceSame} ::: 틀렸습니다.");
                }
            }
        }

        if (count == 3)
        {
            Debug.Log("AnswerManager ::: \n 축하합니다. 정답입니다.");
            UpdateClearData(_modeID, _stageID);
            currPanel = oPanel;
        }
        else
        {
            Debug.Log($"AnswerManager ::: \n 틀렸습니다. 다시 생각해보세요.");
            currPanel = xPanel;
        }

        isChecked = true;
        blurredImage.SetActive(true);
    }


    //Stage Clear 정보를 가지고 있는 GameManager의 stageStateList를 최신화
    void UpdateClearData(int _modeID, int _stageID)
    {
        List<GameManager.StageState> stageStateList = GameManager.Instance.stageStateList;

        //이미 Clear한 Stage를 다시 Clear한 경우
        if (stageStateList[_stageID - 1] != GameManager.StageState.Cleared)
        {
            stageStateList[_stageID - 1] = GameManager.StageState.Cleared;

            if (_stageID < stageStateList.Count)
            {
                stageStateList[_stageID] = GameManager.StageState.Current;
            }
        }

        Debug.Log($"AnswerManager ::: \n UpdateClearData 완료");

        //Data 저장
        //SaveClearData(stageStateList, _modeID);
    }

    //Stage Clear 기록 저장을 위한 Function
    void SaveClearData(List<GameManager.StageState> _stageStateList, int _modeID)
    {
        List<GameManager.StageState> saveList = SaveManager.stageStateArray[_modeID - 2];
        Debug.Log($"AnswerManager ::: 저장 준비");

        if (saveList.Count == 0)
        {
            Debug.Log($"AnswerManager ::: \n SaveManager.stageStatusList0{_modeID - 1}.Count // {saveList.Count}");

            for (int i = 0; i < _stageStateList.Count; i++)
            {
                saveList.Add(_stageStateList[i]);
                Debug.Log($"AnswerManager ::: \n Mode {_modeID} ::: SaveManager.stageStatusList0{_modeID - 1}[{i}] // {_stageStateList[i]}");
            }
        }
        else
        {
            Debug.Log($"AnswerManager ::: \n SaveManager.stageStatusList0{_modeID - 2}.Count // {saveList.Count}");

            for (int i = 0; i < _stageStateList.Count; i++)
            {
                saveList[i] = _stageStateList[i];
                Debug.Log($"AnswerManager ::: \n Mode {_modeID} ::: SaveManager.stageStatusList0{_modeID - 1}[{i}] // {_stageStateList[i]}");
            }
        }

        SaveManager.Save();
    }
}
