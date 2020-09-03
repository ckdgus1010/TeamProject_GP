using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CheckAnswerMgr : MonoBehaviour
{
    public RayforCheck[] gridArray = new RayforCheck[9];
    public List<int> playerList;
    public List<int> answerList = new List<int>(9);

    public void CheckingAnswer()
    {
        if (playerList.Count > 0)
        {
            playerList.Clear();
        }

        //Grid에서 발사한 ray로 player가 놓은 큐브 감지
        for (int i = 0; i < gridArray.Length; i++)
        {
            gridArray[i].CheckingCube();
            playerList.Add(gridArray[i].count);
            Debug.Log($"userList[{i}] ::: {playerList[i]}");
        }

        //두 개의 list 크기 비교
        if (playerList.Count != answerList.Count)
        {
            bool isCountSame = false;
            Debug.Log($"isCountSame ::: {isCountSame}");
            Debug.Log($"userList.Count // answerList.Count ::: {playerList.Count} // {answerList.Count}");
        }
        else
        {
            //리스트 값 비교
            bool isSequenceSame = playerList.SequenceEqual(answerList);

            if (isSequenceSame == true)
            {
                Debug.Log("정답입니다.");
            }
            else
            {
                Debug.Log("틀렸습니다.");
            }
        }
    }
}
