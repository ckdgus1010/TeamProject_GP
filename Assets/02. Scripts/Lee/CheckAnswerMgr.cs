using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CheckAnswerMgr : MonoBehaviour
{
    public List<RayforCheck> gridList = new List<RayforCheck>(9);

    //player가 작성한 답안
    public List<int> userList;

    //실제 답안
    public List<int> answer = new List<int>(9);

    public void CheckingAnswer()
    {
        if (userList.Count > 0)
        {
            userList.Clear();
        }

        //Grid에서 발사한 ray로 player가 놓은 큐브 감지
        for (int i = 0; i < gridList.Count; i++)
        {
            //userList.Add(gridList[i].count);
            Debug.Log($"userList[{i}] ::: {userList[i]}");
        }

        //두 개의 list 크기 비교
        if (userList.Count != answer.Count)
        {
            bool isCountSame = false;
            Debug.Log($"isCountSame ::: {isCountSame}");
            Debug.Log($"userList // answer ::: {userList.Count} // {answer.Count}");
        }
        else
        {
            //리스트 값 비교
            bool isSequenceSame = userList.SequenceEqual(answer);

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
