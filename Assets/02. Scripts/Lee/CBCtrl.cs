using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;


public class CBCtrl : MonoBehaviour
{
    public Slider gridSizeSlider;
    public AnswerMgr answerMgr;

    //Debug용 - 삭제해도 무방
    private enum RayDirection { front, side, top };

    //RayQuad 모음
    private RayforCheck[][] rayQuadArray;
    public RayforCheck[] topArray = new RayforCheck[9];
    public RayforCheck[] forwardArray = new RayforCheck[9];
    public RayforCheck[] sideArray = new RayforCheck[9];

    //player가 쌓은 cube를 확인
    public List<int>[] playerAnswerArray;
    private List<int> topPlayerList = new List<int>();
    private List<int> frontPlayerList = new List<int>();
    private List<int> sidePlayerList = new List<int>();

    private void Start()
    {
        rayQuadArray = new RayforCheck[][] { forwardArray, sideArray, topArray };
        playerAnswerArray = new List<int>[3] { frontPlayerList, sidePlayerList, topPlayerList };
    }

    public List<int>[] MakePlayerAnswerArray()
    {
        //int arraySize = (int)gridSizeSlider.value * (int)gridSizeSlider.value;
        int arraySize = 9;
        Debug.Log($"arraySize ::: {arraySize}");

        Check(arraySize);

        return playerAnswerArray;
    }

    void Check(int _arraySize)
    {
        for (int i = 0; i < playerAnswerArray.Length; i++)
        {
            if (playerAnswerArray[i].Count > 0)
            {
                playerAnswerArray[i].Clear();
            }

            //Grid에서 발사한 ray로 player가 놓은 큐브 감지
            for (int j = 0; j < _arraySize; j++)
            {
                RayforCheck[] _array = rayQuadArray[i];
                _array[j].CheckingCube();
                playerAnswerArray[i].Add(_array[j].count);

                Debug.Log($"{(RayDirection)i}[{j}] ::: {playerAnswerArray[i][j]}");
            }
        }
    }
}

