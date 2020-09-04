using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

namespace Lee
{
    public class TestScript : MonoBehaviour
    {
        public Slider gridSizeSlider;
        public AnswerMgr answerMgr;

        public RayforCheck[] topArray = new RayforCheck[0];
        public RayforCheck[] forwardArray = new RayforCheck[0];
        public RayforCheck[] sideArray = new RayforCheck[0];

        private List<int> topPlayerList = new List<int>();
        private List<int> forwardPlayerList = new List<int>();
        private List<int> sidePlayerList = new List<int>();

        private List<int> list;
        private RayforCheck[] array;
        private List<int> answer;
        private string cardImage;

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

            for (int i = 0; i < 3; i++)
            {
                if (i == 1)
                {
                    list = forwardPlayerList;
                    array = forwardArray;
                    answer = answerMgr.forwardAnswerList;
                    cardImage = "정면";
                }
                else if (i == 2)
                {
                    list = sidePlayerList;
                    array = sideArray;
                    answer = answerMgr.sideAnswerList;
                    cardImage = "옆면";
                }
                else if (i == 0)
                {
                    list = topPlayerList;
                    array = topArray;
                    answer = answerMgr.topAnswerList;
                    cardImage = "윗면";
                }

                if (list.Count > 0)
                {
                    list.Clear();
                }

                //topArray - Grid에서 발사한 ray로 player가 놓은 큐브 감지
                for (int j = 0; j < arraySize; j++)
                {
                    array[j].CheckingCube();
                    list.Add(array[j].count);

                    //Debug.Log($"{list}[{i}] ::: {list[i]}");
                }

                //정답과 player의 답안과 비교
                //1. 두 list의 길이 비교
                if (list.Count != answer.Count)
                {
                    bool isCountSame = false;

                    Debug.Log($"isCountSame ::: {isCountSame}");
                    Debug.Log($"{list}.Count // {answer}.Count \n ::: {list.Count} // {answer.Count}");
                }
                else
                {
                    //두 list의 값 비교
                    bool isSequenceSame = list.SequenceEqual(answer);

                    if (isSequenceSame == true)
                    {
                        count += 1;
                        Debug.Log($"{cardImage} ::: {isSequenceSame} ::: 정답입니다.");
                    }
                    else
                    {
                        Debug.Log($"{cardImage} ::: {isSequenceSame} ::: 틀렸습니다.");
                    }
                }
            }

            if (count == 3)
            {
                Debug.Log("축하합니다. 정답입니다.");
            }
            else
            {
                Debug.Log("다시 생각해보라우.");
            }
        }
    }
}
