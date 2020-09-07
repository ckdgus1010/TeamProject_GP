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

        //앞, 옆, 위에 있는 rayQuad
        public RayforCheck[] forwardArray = new RayforCheck[0];
        public RayforCheck[] sideArray = new RayforCheck[0];
        public RayforCheck[] topArray = new RayforCheck[0];

        //player가 놓은 cube를 기준으로 만든 앞, 옆, 위에서 본 이미지
        private List<int> forwardPlayerList = new List<int>();
        private List<int> sidePlayerList = new List<int>();
        private List<int> topPlayerList = new List<int>();

        public List<List<int>> playerList;
        public List<RayforCheck[]> array;
        public List<List<int>> answerList;
        public string[] cardImg = new string[3] { "정면", "옆면", "윗면" };

        //정답 확인용
        //위, 앞, 옆 정답 확인 시
        //참이면 count += 1, 아니면 count += 0
        //count = 3 이면 정답, 아니면 오답
        public int count;

        private void Start()
        {
            count = 0;

            playerList = new List<List<int>> { forwardPlayerList
                                               , sidePlayerList
                                               , topPlayerList };

            array = new List<RayforCheck[]>  { forwardArray
                                               , sideArray
                                               , topArray };

            answerList = new List<List<int>> { answerMgr.forwardAnswerList
                                               , answerMgr.sideAnswerList
                                               , answerMgr.topAnswerList };
        }

        public void CheckAnswer()
        {
            int arraySize = (int)gridSizeSlider.value * (int)gridSizeSlider.value;
            Debug.Log($"arraySize ::: {arraySize}");

            count = 0;

            //0:정면 → 1:옆면 → 2:윗면 순으로 비교
            for (int i = 0; i < playerList.Count; i++)
            {
                CompareLists(arraySize, playerList[i], array[i], answerList[i], cardImg[i]);
            }

            //최종 정답 확인
            if (count == 3)
            {
                Debug.Log("축하합니다. 정답입니다.");
            }
            else
            {
                Debug.Log("다시 생각해보라우.");
            }
        }

        void CompareLists(int _arraySize, List<int> _list, RayforCheck[] _array, List<int> _answerList, string _cardImage)
        {
            //이미 _list가 있다면 제거
            if (_list.Count > 0)
            {
                _list.Clear();
            }

            //각 RayQuad에서 발사한 ray로 player가 놓은 큐브 감지
            for (int j = 0; j < _arraySize; j++)
            {
                _array[j].CheckingCube();
                _list.Add(_array[j].count);
            }

            //정답과 player의 답안과 비교
            //1. 두 list의 길이 비교
            if (_list.Count != _answerList.Count)
            {
                bool isCountSame = false;

                Debug.Log($"isCountSame ::: {isCountSame}");
                Debug.Log($"{_list}.Count // {_answerList}.Count \n ::: {_list.Count} // {_answerList.Count}");
            }
            else
            {
                //2. 두 list의 값 비교
                bool isSequenceSame = _list.SequenceEqual(_answerList);

                if (isSequenceSame)
                {
                    count += 1;
                    Debug.Log($"{_cardImage} ::: {isSequenceSame} ::: 정답입니다.");
                }
                else
                {
                    Debug.Log($"{_cardImage} ::: {isSequenceSame} ::: 틀렸습니다.");
                }
            }
        }
    }
}
