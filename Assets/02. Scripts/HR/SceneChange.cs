using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChange : MonoBehaviour
{
        public void ChangeIntroScene()
    {
        SoundManager.instance.EffefctPlay(6);
        SceneManager.LoadScene("01. Intro");
    }
        public void ChangeLogInScene()
    {
        SoundManager.instance.EffefctPlay(6);
        SceneManager.LoadScene("02. LogIn");
    }
        public void ChangeTutorialScene()
    {
        SoundManager.instance.EffefctPlay(6);
        SceneManager.LoadScene("03. Tutorial");
    }
        public void ChangeMainMenuScene()
    {
        SoundManager.instance.EffefctPlay(6);
        SceneManager.LoadScene("04. MainMenu");
    }
    public void ChangePlayModeScene()
    {
        SoundManager.instance.StopBGM();
        SoundManager.instance.EffefctPlay(6);
        SceneManager.LoadScene("05. PlayMode");
    }
     public void ChangeCreateModeScene()
    {
        SoundManager.instance.EffefctPlay(6);
        SceneManager.LoadScene("06. CreateMode");
    }
     public void ChangeAloneModeScene()
    {
        SoundManager.instance.EffefctPlay(6);
        SceneManager.LoadScene("07. AloneMode");
    }
     public void ChangeCountNumberScene()
    {
        SoundManager.instance.EffefctPlay(6);
        SceneManager.LoadScene("08. CountNumber");
    }
     public void ChangeMinerCubeScene()
    {
        SoundManager.instance.EffefctPlay(6);
        SceneManager.LoadScene("09. MinerCube");
    }
     public void ChangeCardCubeScene()
    {
        SoundManager.instance.EffefctPlay(6);
        SceneManager.LoadScene("10. CardCube");
    }
     public void ChangeTogetherModeListScene()
    {
        SoundManager.instance.EffefctPlay(6);
        SceneManager.LoadScene("11. TogetherModeList");
    }
     public void ChangeTogetherModeWaitScene()
    {
        SoundManager.instance.EffefctPlay(6);
        SceneManager.LoadScene("12. TogetherModeWait");
    }
     public void ChangeTogetherModeGameRoomScene()
    {
        SoundManager.instance.EffefctPlay(6);
        SceneManager.LoadScene("13. TogetherModeGameRoom");
    }
}
