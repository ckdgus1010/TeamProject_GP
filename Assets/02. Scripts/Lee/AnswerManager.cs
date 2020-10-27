using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.UI;

public class AnswerManager : MonoBehaviour
{
    //정답 정보를 가지고 있는 Script
    [Header("정답 데이터")]
    public AnswerData01 answerData01;
    public AnswerData02 answerData02;
    public AnswerData03 answerData03;
    public AnswerDataEasy answerDataEasy;
    public AnswerDataNomal answerDataNomal;
    public AnswerDataHard answerDataHard;

    //정답 정보를 가지고 있는 Array ( front = 0, side = 1, top = 2 )
    public List<int>[] answerArray = new List<int>[3];
    private enum CardDirection { front, side, top };

    //정답, 오답 패널
    [Header("정답, 오답 패널")]
    public RectTransform oPanel;
    public RectTransform xPanel;
    public GameObject blurredImage;

    //정답, 오답 확인 여부
    public bool isCorrect = false;
    public bool isChecked = false;
    public float lerpSpeed = 10.0f;

    //패널의 위치
    [HideInInspector]
    public RectTransform currPanel;
    public RectTransform startPos;
    public RectTransform endPos;

    // 스크립트
    [Header("스크립트")]
    public SoundManager soundMgr;


    private void Start()
    {
        currPanel = null;
    }

    private void Update()
    {
        //정답, 오답 팝업창 이동
        if (currPanel != null)
        {
            RectTransform posSP = isChecked ? endPos : startPos;
            currPanel.anchoredPosition = Vector2.Lerp(currPanel.anchoredPosition, posSP.anchoredPosition, Time.deltaTime * lerpSpeed);
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
        Debug.Log($"AnswerManager ::: \n GameManager.Instance.modeID // GameManager.Instance.stageID :: {GameManager.Instance.modeID} //{GameManager.Instance.stageID}");

        switch(modeID)
        {
            case 3:
                for (int i = 0; i < answerArray.Length; i++)
                {
                    answerArray[i] = answerData02.ChooseAnswerList(stageID)[i];
                }
                break;
            case 4:
                for (int i = 0; i < answerArray.Length; i++)
                {
                    answerArray[i] = answerData03.ChooseAnswerList(stageID)[i];
                }
                break;
            case 5:
                Debug.Log($"AnswerManager ::: CompareAnswer_Array // modeID = 5, 잘못 들어옴");
                break;
            case 6:
                Debug.Log("666666666나는 같이하기 Easy 모드야 ^^;;");
                for (int i = 0; i < answerArray.Length; i++)
                {
                    answerArray[i] = answerDataEasy.ChooseAnswerList(stageID)[i];
                }
                break;
            case 7:
                Debug.Log("777777나는 같이하기 Nomal 모드야 ^^;;");
                for (int i = 0; i < answerArray.Length; i++)
                {
                    answerArray[i] = answerDataNomal.ChooseAnswerList(stageID)[i];
                }
                break;
            case 8:
                Debug.Log("88888888나는 같이하기 Hard 모드야 ^^;;");
                for (int i = 0; i < answerArray.Length; i++)
                {
                    answerArray[i] = answerDataHard.ChooseAnswerList(stageID)[i];
                }
                break;
        }

        #region 수정 전 답안 가져오기 코드
        //if (modeID == 3)
        //{
        //    for (int i = 0; i < answerArray.Length; i++)
        //    {
        //        answerArray[i] = answerData02.ChooseAnswerList(stageID)[i];
        //    }
        //}
        //else if (modeID == 4)
        //{
        //    for (int i = 0; i < answerArray.Length; i++)
        //    {
        //        answerArray[i] = answerData03.ChooseAnswerList(stageID)[i];
        //    }
        //}
        //else if (modeID == 5)
        //{
        //    Debug.Log("나는 같이하기 유형이야 ^^;;");
        //}
        //else if (modeID == 6)
        //{
        //    Debug.Log("666666666나는 같이하기 Easy 모드야 ^^;;");
        //    for (int i = 0; i < answerArray.Length; i++)
        //    {
        //        answerArray[i] = answerDataEasy.ChooseAnswerList(stageID)[i];
        //    }
        //}
        //else if (modeID == 7)
        //{
        //    Debug.Log("777777나는 같이하기 Nomal 모드야 ^^;;");
        //    for (int i = 0; i < answerArray.Length; i++)
        //    {
        //        answerArray[i] = answerDataNomal.ChooseAnswerList(stageID)[i];
        //    }
        //}
        //else if (modeID == 8)
        //{
        //    Debug.Log("88888888나는 같이하기 Hard 모드야 ^^;;");
        //    for (int i = 0; i < answerArray.Length; i++)
        //    {
        //        answerArray[i] = answerDataHard.ChooseAnswerList(stageID)[i];
        //    }
        //}

        #endregion

        Debug.Log("AnswerManager ::: 정답 정보 가져옴");
        for (int i = 0; i < answerArray.Length; i++)
        {
            for (int j = 0; j < answerArray[i].Count; j++)
            {
                Debug.Log($"answerArray[{i}][{j}] ::: {answerArray[i][j]}");

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
        isCorrect = false;

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

        OXPanel(_modeID, _stageID, count);
    }

    public void OXPanel(int modeID, int stageID, int count)
    {
        if (count == 3)
        {
            Debug.Log("AnswerManager ::: \n 축하합니다. 정답입니다.");
            currPanel = oPanel;

            // 정답 사운드
            soundMgr.answerAudio.clip = soundMgr.answerSound;
            soundMgr.answerAudio.Play();
            isCorrect = true;

            //혼자하기 모드의 경우 스테이지 클리어 데이터 업데이트
            switch (modeID)
            {
                case 0:
                case 1:
                case 5:
                    Debug.Log($"AnswerManager ::: OXPanel() // modeID 오류 {modeID}");
                    break;
                case 2:
                case 3:
                case 4:
                    UpdateClearData(modeID, stageID);
                    break;
                case 6:
                case 7:
                case 8:
                    Debug.Log("AnswerManager ::: OXPanel() // 같이하기 모드 정답입니다.");
                    break;
            }
        }
        else
        {
            Debug.Log($"AnswerManager ::: \n 틀렸습니다. 다시 생각해보세요.");
            currPanel = xPanel;

            // 오답 사운드
            soundMgr.answerAudio.clip = soundMgr.wrongSound;
            soundMgr.answerAudio.Play();

            isCorrect = false;
        }

        isChecked = true;
        blurredImage.SetActive(true);
    }

    public void SendAnswerManagerInfo(bool isCorrect, bool _isChecked)
    {
        currPanel = isCorrect ? oPanel : xPanel;

        isChecked = _isChecked;
    }

    //----------------------------------------------------------------------------------------------------

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
        SaveClearData(stageStateList, _modeID);
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
