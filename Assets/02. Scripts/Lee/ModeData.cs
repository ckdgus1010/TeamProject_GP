using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Photon.Pun;

public class ModeData : MonoBehaviourPun
{
    public int modeID = 0;

    public void MasterConvertScene()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            ConvertScene();
        }
    }

    public void ConvertScene()
    {
        GameManager.Instance.modeID = modeID;
        //modeID = 0 ::: Create Mode
        //modeID = 1 ::: 혼자하기 모드
        //modeID = 2 ::: 혼자하기 모드 - 유형1
        //modeID = 3 ::: 혼자하기 모드 - 유형2
        //modeID = 4 ::: 혼자하기 모드 - 유형3
        //modeID = 5 ::: 같이하기 모드
        //modeID = 6 ::: 같이하기 Easy 모드
        //modeID = 7 ::: 같이하기 Nomal 모드
        //modeID = 8 ::: 같이하기 Hard 모드

        switch (modeID)
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
                Debug.Log("난 5번 모드야... 하라고 해서 함");
                SceneManager.LoadScene("11. TogetherModeList");
                break;
            case 6:
                //Easy_mode
                Debug.Log("Easy_mode");
                break;
            case 7:
                //Nomal_mode
                Debug.Log("Nomal_mode");
                break;
            case 8:
                //Hard_mode
                Debug.Log("Hard_mode");
                break;

        }
    }
}
