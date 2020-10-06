using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Photon.Pun;
using Photon.Realtime;

public class SceneChange : MonoBehaviourPunCallbacks
{
    public void ChangeIntroScene()
    {
        //SoundManager.instance.EffefctPlay(6);
        SceneManager.LoadScene("01. Intro");
    }

    public void ChangeLogInScene()
    {
        //SoundManager.instance.EffefctPlay(6);
        SceneManager.LoadScene("02. LogIn");
    }

    public void ChangeTutorialScene()
    {
        //SoundManager.instance.EffefctPlay(6);
        SceneManager.LoadScene("03. Tutorial");
    }

    public void ChangeMainMenuScene()
    {
        //SoundManager.instance.EffefctPlay(6);
        SceneManager.LoadScene("04. MainMenu");
    }

    public void ChangePlayModeScene()
    {
        //modeID와 stageID 초기화
        GameManager.Instance.modeID = 1000;
        GameManager.Instance.stageID = 0;

        //SoundManager.instance.StopBGM();
        //SoundManager.instance.EffefctPlay(6);
        SceneManager.LoadScene("05. PlayMode");
    }

    public void ChangeCreateModeScene()
    {
        //SoundManager.instance.EffefctPlay(6);
        SceneManager.LoadScene("06. CreateMode");
    }

    public void ChangeAloneModeScene()
    {
        //SoundManager.instance.EffefctPlay(6);
        SceneManager.LoadScene("07. AloneMode");
    }

    public void ChangeCountNumberScene()
    {
        GameManager.Instance.stageID = 0;

        //SoundManager.instance.EffefctPlay(6);
        SceneManager.LoadScene("08. CountNumber");
    }

    public void ChangeMinerCubeScene()
    {
        GameManager.Instance.stageID = 0;

        //SoundManager.instance.EffefctPlay(6);
        SceneManager.LoadScene("09. MinerCube");
    }

    public void ChangeCardCubeScene()
    {
        GameManager.Instance.stageID = 0;

        //SoundManager.instance.EffefctPlay(6);
        SceneManager.LoadScene("10. CardCube");
    }

    public void ChangeTogetherModeListScene()
    {
        //SoundManager.instance.EffefctPlay(6);
        SceneManager.LoadScene("11. TogetherModeList");
    }

    public void ChangeTogetherModeWaitScene()
    {
        //SoundManager.instance.EffefctPlay(6);
        SceneManager.LoadScene("12. TogetherModeWait");
    }

    public void ChangeTogetherModeGameRoomScene()
    {
        //SoundManager.instance.EffefctPlay(6);
        SceneManager.LoadScene("13. TogetherModeGameRoom");
    }

    public void BackToPrevScene()
    {
        int modeID = GameManager.Instance.modeID;

        switch (modeID)
        {
            case 0:                 //Create Mode인 경우
                ChangePlayModeScene();
                break;
            case 1:
                Debug.LogError($"SceneManager ::: \n modeID = {modeID} // 04 -2 Scene에서 modeID 값을 제대로 받아오지 못했습니다.");
                break;
            case 2:                 //혼자하기 모드 - 유형 1인 경우
                ChangeCountNumberScene();
                break;
            case 3:                 //혼자하기 모드 - 유형 2인 경우
                ChangeMinerCubeScene();
                break;
            case 4:                 //혼자하기 모드 - 유형 3인 경우
                ChangeCardCubeScene();
                break;
            case 5:                 //같이하기 모드인 경우
                
                break;
        }
    }
    public void OnClickLeaveRoom()
    {
        //if (PhotonNetwork.IsMasterClient)
        //{
        //    PhotonNetwork.SetMasterClient(PhotonNetwork.PlayerList[1]);
        //}
        PhotonNetwork.LeaveRoom();

    }
    public override void OnLeftRoom()
    {
        SceneManager.LoadScene("11. TogetherModeList");
        base.OnLeftRoom();
        print(System.Reflection.MethodBase.GetCurrentMethod().Name);

    }


}
