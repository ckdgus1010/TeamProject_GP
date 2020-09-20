using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;


public class CBCtrl : MonoBehaviour
{
    public Slider gridSizeSlider;
    public AnswerMgr answerMgr;

    public RayforCheck[] topArray = new RayforCheck[9];
    public RayforCheck[] forwardArray = new RayforCheck[9];
    public RayforCheck[] sideArray = new RayforCheck[9];

    private List<int> topPlayerList = new List<int>();
    private List<int> forwardPlayerList = new List<int>();
    private List<int> sidePlayerList = new List<int>();

    //정답 확인용
    //위, 앞, 옆 정답 확인 시
    //참이면 count += 1, 아니면 count += 0
    //count = 3 이면 정답, 아니면 오답
    public int count;

    private void Start()
    {
        count = 0;
    }

    public void CollectResult()
    {
        int arraySize = (int)gridSizeSlider.value * (int)gridSizeSlider.value;
        Debug.Log($"arraySize ::: {arraySize}");

        count = 0;

        CheckTop(arraySize);
        CheckForward(arraySize);
        CheckSide(arraySize);

        if (count == 3)
        {
            Debug.Log("축하합니다. 정답입니다.");
        }
        else
        {
            Debug.Log("틀렸습니다. 다시 생각해보세요.");
        }
    }

    void CheckTop(int _arraySize)
    {
        if (topPlayerList.Count > 0)
        {
            topPlayerList.Clear();
        }

        //topArray - Grid에서 발사한 ray로 player가 놓은 큐브 감지
        for (int i = 0; i < _arraySize; i++)
        {
            topArray[i].CheckingCube();
            topPlayerList.Add(topArray[i].count);

            //Debug.Log($"topPlayerList[{i}] ::: {topPlayerList[i]}");
        }

        //정답과 player의 답안과 비교
        //1. 두 list의 길이 비교
        if (topPlayerList.Count != answerMgr.topAnswerList.Count)
        {
            bool isCountSame = false;

            Debug.Log($"isCountSame ::: {isCountSame}");
            Debug.Log($"topPlayerList.Count // answerMgr.topAnswerList.Count \n ::: {topPlayerList.Count} // {answerMgr.topAnswerList.Count}");
        }
        else
        {
            //두 list의 값 비교
            bool isSequenceSame = topPlayerList.SequenceEqual(answerMgr.topAnswerList);

            if (isSequenceSame == true)
            {
                count += 1;
                Debug.Log($"윗면 ::: {isSequenceSame} ::: 정답입니다.");
            }
            else
            {
                Debug.Log($"윗면 ::: {isSequenceSame} ::: 틀렸습니다.");
            }
        }
    }

    void CheckForward(int _arraySize)
    {
        if (forwardPlayerList.Count > 0)
        {
            forwardPlayerList.Clear();
        }

        //forwardArray - Grid에서 발사한 ray로 player가 놓은 큐브 감지
        for (int i = 0; i < _arraySize; i++)
        {
            forwardArray[i].CheckingCube();
            forwardPlayerList.Add(forwardArray[i].count);

            //Debug.Log($"forwardPlayerList[{i}] ::: {forwardPlayerList[i]}");
        }

        //정답과 player의 답안과 비교
        //1. 두 list의 길이 비교
        if (forwardPlayerList.Count != answerMgr.forwardAnswerList.Count)
        {
            bool isCountSame = false;

            Debug.Log($"isCountSame ::: {isCountSame}");
            Debug.Log($"forwardPlayerList.Count // answerMgr.forwardAnswerList.Count \n ::: {forwardPlayerList.Count} // {answerMgr.forwardAnswerList.Count}");
        }
        else
        {
            //두 list의 값 비교
            bool isSequenceSame = forwardPlayerList.SequenceEqual(answerMgr.forwardAnswerList);

            if (isSequenceSame == true)
            {
                count += 1;
                Debug.Log($"정면 ::: {isSequenceSame} ::: 정답입니다.");
            }
            else
            {
                Debug.Log($"정면 ::: {isSequenceSame} ::: 틀렸습니다.");
            }
        }
    }

    void CheckSide(int _arraySize)
    {
        if (sidePlayerList.Count > 0)
        {
            sidePlayerList.Clear();
        }

        //leftArray - Grid에서 발사한 ray로 player가 놓은 큐브 감지
        for (int i = 0; i < _arraySize; i++)
        {
            sideArray[i].CheckingCube();
            sidePlayerList.Add(sideArray[i].count);

            //Debug.Log($"sidePlayerList[{i}] ::: {leftPlayerList[i]}");
        }

        //정답과 player의 답안과 비교
        //1. 두 list의 길이 비교
        if (sidePlayerList.Count != answerMgr.sideAnswerList.Count)
        {
            bool isCountSame = false;

            Debug.Log($"isCountSame ::: {isCountSame}");
            Debug.Log($"sidePlayerList.Count // answerMgr.leftAnswerList.Count \n ::: {sidePlayerList.Count} // {answerMgr.sideAnswerList.Count}");
        }
        else
        {
            //두 list의 값 비교
            bool isSequenceSame = sidePlayerList.SequenceEqual(answerMgr.sideAnswerList);

            if (isSequenceSame == true)
            {
                count += 1;
                Debug.Log($"옆면 ::: {isSequenceSame} ::: 정답입니다.");
            }
            else
            {
                Debug.Log($"옆면 ::: {isSequenceSame} ::: 틀렸습니다.");
            }
        }
    }
}

