using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChange : MonoBehaviour
{
        public void ChangeIntroScene()
    {
        SceneManager.LoadScene("01. Intro");
    }
        public void ChangeLogInScene()
    {
        SceneManager.LoadScene("02. LogIn");
    }
        public void ChangeTutorialScene()
    {
        SceneManager.LoadScene("03. Tutorial");
    }
        public void ChangeMainMenuScene()
    {
        SceneManager.LoadScene("04. MainMenu");
    }
    public void ChangePlayModeScene()
    {
        SceneManager.LoadScene("05. PlayMode");
    }
     public void ChangeCreateModeScene()
    {
        SceneManager.LoadScene("06. CreateMode");
    }
     public void ChangeAloneModeScene()
    {
        SceneManager.LoadScene("07. AloneMode");
    }
     public void ChangeCountNumberScene()
    {
        SceneManager.LoadScene("08. CountNumber");
    }
     public void ChangeMinerCubeScene()
    {
        SceneManager.LoadScene("09. MinerCube");
    }
     public void ChangeCardCubeScene()
    {
        SceneManager.LoadScene("10. CardCube");
    }
     public void ChangeTogetherModeListScene()
    {
        SceneManager.LoadScene("11. TogetherModeList");
    }
     public void ChangeTogetherModeWaitScene()
    {
        SceneManager.LoadScene("12. TogetherModeWait");
    }
     public void ChangeTogetherModeGameRoomScene()
    {
        SceneManager.LoadScene("13. TogetherModeGameRoom");
    }
}
