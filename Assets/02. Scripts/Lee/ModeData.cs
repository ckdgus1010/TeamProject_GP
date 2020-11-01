using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Photon.Pun;

public class ModeData : MonoBehaviourPun
{
    [SerializeField] private CanvasController canvasController;
    [SerializeField] private ModeTitle modeTitle;
    [SerializeField] private int modeID = 0;
    [SerializeField] private StagePopupTitle stagePopupTitle;
    [SerializeField] private StageData[] stageDatas = new StageData[9];

    //public void MasterConvertScene()
    //{
    //    if (PhotonNetwork.IsMasterClient)
    //    {
    //        ConvertScene();
    //    }
    //}

    public void ChangeModeID()
    {
        // GameManager의 modeID 및 currStageStatus 값 변경
        GameManager.Instance.modeID = modeID;
        GameManager.Instance.stageStateList = GameManager.Instance.stageStateArray[modeID - 2];

        // Stage Popup에 있는 title 변경
        stagePopupTitle.ChangeTitleText();

        // Stage Popup에 있는 모든 button image 변경
        for (int i = 0; i < stageDatas.Length; i++)
        {
            stageDatas[i].ChangeButtonImage();
        }
    }

    public void ConvertCanvas()
    {
        GameManager.Instance.modeID = modeID;

        switch (modeID)
        {
            case 0:
                Debug.Log($"{this.gameObject.name} // ModeData {modeID} ::: Create Mode로 이동");
                GameManager.Instance.stageID = 1;
                SceneManager.LoadScene("06. CreateMode");
                break;
            case 1:
                Debug.Log($"{this.gameObject.name} // ModeData {modeID} ::: \n 혼자하기 유형 선택 화면으로 이동");

                // Canvas 전환
                canvasController.OpenAloneModeCanvas();
                break;
            case 2:
            case 3:
            case 4:
                Debug.Log($"{this.gameObject.name} // ModeData {modeID} ::: \n 혼자하기 스테이지 선택 화면으로 이동");
                modeTitle.ChangeModeTitle();

                GameManager.Instance.stageStateList = GameManager.Instance.stageStateArray[modeID - 2];
                // Canvas 전환
                canvasController.OpenStageSelectCanvas();
                break;
            case 5:
                Debug.Log($"{this.gameObject.name} // ModeData {modeID} ::: \n 같이하기 모드 방목록 화면으로 이동");
                //SceneManager.LoadScene("11. TogetherModeList");
                SceneManager.LoadScene("TogetherMix");
                break;
            case 6:
            case 7:
            case 8:
                Debug.Log($"{this.gameObject.name}ModeData {modeID} ::: \n 이 에러를 봤다면 작업을 중단하고 연락주세요.");
                break;
        }
    }

    public void ConvertCanvas_Multy()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            GameManager.Instance.modeID = modeID;
        }
    }
    /*
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
    }*/
}
