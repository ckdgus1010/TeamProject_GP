using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;


public class CBCtrl : MonoBehaviour
{
    public Slider gridSizeSlider;
    public AnswerManager answerMgr;

    private enum RayDirection { front, side, top };

    //RayQuad 모음
    //배열 선언 시 크기를 정해줘야 해서 가장 작은 값 9를 대입
    //inspector 창에서 변경할 것
    private RayforCheck[][] rayQuadArray;
    public RayforCheck[] topArray = new RayforCheck[9];
    public RayforCheck[] forwardArray = new RayforCheck[9];
    public RayforCheck[] sideArray = new RayforCheck[9];

    //player가 쌓은 cube를 확인
    public List<int>[] playerAnswerArray;
    private List<int> topPlayerList = new List<int>();
    private List<int> frontPlayerList = new List<int>();
    private List<int> sidePlayerList = new List<int>();

    private int arraySize;

    private void Start()
    {
        rayQuadArray = new RayforCheck[][] { forwardArray, sideArray, topArray };
        playerAnswerArray = new List<int>[3] { frontPlayerList, sidePlayerList, topPlayerList };
    }

    public List<int>[] MakePlayerAnswerArray(int arraySize)
    {
        Debug.Log($"arraySize ::: {arraySize}");
        Check(arraySize);

        return playerAnswerArray;
    }

    void Check(int _arraySize)
    {
        Debug.Log($"1");

        for (int i = 0; i < playerAnswerArray.Length; i++)
        {
            Debug.Log($"2");
            if (playerAnswerArray[i].Count > 0)
            {
                playerAnswerArray[i].Clear();
            }

        Debug.Log($"3");

            //Grid에서 발사한 ray로 player가 놓은 큐브 감지
            for (int j = 0; j < _arraySize; j++)
            {
                RayforCheck[] _array = rayQuadArray[i];
                _array[j].CheckingCube();
                playerAnswerArray[i].Add(_array[j].count);

                Debug.Log($"{(RayDirection)i}[{j}] ::: {playerAnswerArray[i][j]}");
            }
        }
        Debug.Log($"4");

    }
}

