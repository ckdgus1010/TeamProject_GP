using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ModeData : MonoBehaviour
{
    public int modeID = 0;

    public void ConvertScene()
    {
        GameManager.Instance.modeID = modeID;
        //modeID = 0 ::: Create Mode
        //modeID = 1 ::: 혼자하기 모드
        //modeID = 2 ::: 혼자하기 모드 - 유형1
        //modeID = 3 ::: 혼자하기 모드 - 유형2
        //modeID = 4 ::: 혼자하기 모드 - 유형3
        //modeID = 5 ::: 같이하기 모드

        switch(modeID)
        {
            case 0:
                GameManager.Instance.stageID = 1;
                SceneManager.LoadScene("06. CreateMode");
                break;
            case 1:
                SceneManager.LoadScene("07. AloneMode");
                break;
            case 2:
                GameManager.Instance.stageStateList = GameManager.Instance.stageStateArray[modeID - 2];
                SceneManager.LoadScene("08. CountNumber");
                break;
            case 3:
                GameManager.Instance.stageStateList = GameManager.Instance.stageStateArray[modeID - 2];
                SceneManager.LoadScene("09. MinerCube");
                break;
            case 4:
                GameManager.Instance.stageStateList = GameManager.Instance.stageStateArray[modeID - 2];
                SceneManager.LoadScene("10. CardCube");
                break;
            case 5:
                SceneManager.LoadScene("11. TogetherModeList");
                break;
        }
    }
}
