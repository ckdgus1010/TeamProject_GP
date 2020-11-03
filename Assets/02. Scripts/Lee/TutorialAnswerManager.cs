using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class TutorialAnswerManager : MonoBehaviour
{
    public List<int> frontAnswerList = new List<int> { 0, 0, 0
                                                     , 0, 0, 1
                                                     , 0, 1, 1 };

    public List<int> sideAnswerList = new List<int>  { 0, 0, 0
                                                     , 0, 0, 1
                                                     , 0, 1, 1 };

    public List<int> topAnswerList = new List<int>   { 0, 1, 1
                                                     , 0, 0, 1
                                                     , 0, 0, 0 };

    public List<int>[] answerArray;
    private bool isCorrect = false;
    private enum CardDirection { front, side, top };

    public RectTransform oPanel;
    public RectTransform xPanel;
    private RectTransform currPanel;

    public bool isChecked = false;
    public RectTransform startPos;
    public RectTransform endPos;
    public float lerpSpeed = 5.0f;


    private void Start()
    {
        answerArray = new List<int>[3] { frontAnswerList, sideAnswerList, topAnswerList };
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

    public void CheckAnswer(List<int>[] playerAnswerArray)
    {
        Debug.Log("TutorialAnswerManager ::: 정답 확인");

        //정답 확인용
        //위, 앞, 옆 정답 확인 시
        //문제 카드와 일치하면 count += 1, 그렇지 않으면 count += 0
        //count = 3 이면 정답, 아니면 오답
        int count = 0;
        isCorrect = false;

        //위, 앞, 옆 정답 확인
        for (int i = 0; i < answerArray.Length; i++)
        {
            //1. 두 List의 길이 비교
            if (playerAnswerArray[i].Count != answerArray[i].Count)
            {
                bool isCountSame = false;

                Debug.Log($"AnswerManager ::: \n {(CardDirection)i} isCountSame ::: {isCountSame}");
                Debug.Log($"AnswerManager ::: \n playerAnswerList[{i}].Count // answerList[{i}].Count ::: {playerAnswerArray[i].Count} // {answerArray[i].Count}");
            }
            //2. 두 List의 요소 비교
            else
            {
                bool isSequenceSame = playerAnswerArray[i].SequenceEqual(answerArray[i]);

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

        OXPanel(count);
    }

    public void OXPanel(int count)
    {
        if (count == 3)
        {
            Debug.Log("AnswerManager ::: \n 축하합니다. 정답입니다.");
            currPanel = oPanel;

            // 정답 사운드
            //soundMgr.answerAudio.clip = soundMgr.answerSound;
            //soundMgr.answerAudio.Play();
            //isCorrect = true;
        }
        else
        {
            Debug.Log($"AnswerManager ::: \n 틀렸습니다. 다시 생각해보세요.");
            currPanel = xPanel;

            // 오답 사운드
            //soundMgr.answerAudio.clip = soundMgr.wrongSound;
            //soundMgr.answerAudio.Play();

            isCorrect = false;
        }

        isChecked = true;
    }
}
