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
    //public RayforCheck[] forwardArray = new RayforCheck[9];
    //public RayforCheck[] leftArray = new RayforCheck[9];

    public List<int> topPlayerList = new List<int>();
    //public List<int> forwardPlayerList = new List<int>();
    //public List<int> leftPlayerList = new List<int>();


    public void CollectResult()
    {
        CheckTop();
    }

    void CheckTop()
    {
        int arraySize = (int)gridSizeSlider.value * (int)gridSizeSlider.value;
        Debug.Log($"arraySize ::: {arraySize}");

        if (topPlayerList.Count > 0)
        {
            topPlayerList.Clear();
        }

        //topArray - Grid에서 발사한 ray로 player가 놓은 큐브 감지
        for (int i = 0; i < arraySize; i++)
        {
            topArray[i].CheckingCube();
            topPlayerList.Add(topArray[i].count);
            Debug.Log($"topPlayerList[{i}] ::: {topPlayerList[i]}");
        }

        //정답과 player의 답안과 비교
        //1. 두 list의 길이 비교
        if (topPlayerList.Count != answerMgr.topAnswerList.Count)
        {
            bool isCountSame = false;
            Debug.Log($"isCountSame ::: {isCountSame}");
            Debug.Log($"topPlayerList.Count // answerMgr.topAnswerList.Count ::: {topPlayerList.Count} // {answerMgr.topAnswerList.Count}");
        }
        else
        {
            //두 list의 값 비교
            bool isSequenceSame = topPlayerList.SequenceEqual(answerMgr.topAnswerList);

            if (isSequenceSame == true)
            {
                Debug.Log($"{isSequenceSame} ::: 정답입니다.");
            }
            else
            {
                Debug.Log($"{isSequenceSame} ::: 틀렸습니다.");
            }
        }
    }
}
