using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class StageData : MonoBehaviour
{
    public int stageID = 0;
    private int status;

    //나중에 Start 함수로 바꿀 것
    private void Start()
    {
        List<GameManager.StageState> stageStateList = GameManager.Instance.stageStateList;

        //Stage Clear 여부 확인
        status = (int)stageStateList[stageID - 1];

        Image img = GetComponent<Image>();
        img.color = GameManager.Instance.buttonColor[status];
    }

    public void ConvertScene()
    {
        if (status != (int)GameManager.StageState.Forbidden)
        {
            //GameManager에게 정보 전달
            GameManager.Instance.stageID = stageID;
            SceneManager.LoadScene("14. Play Scene");
        }
        else
        {
            Debug.Log($"{transform.parent.name} ::: 이전 단계를 먼저 클리어하세요.");
        }
    }
}
