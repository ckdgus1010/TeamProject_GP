using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class MainMenuMgr : MonoBehaviour
{
    public Text nickName_Text;

    public GameObject popUpProfile;
    public GameObject popUpOption;      //일반 옵션 
    public GameObject PopupRoomMaker;   // 11. TogetherModeList
    public GameObject PopupGameOption;  //게임 옵션 (뒤로가기 있음)
    public GameObject BlackBG;

    public GameObject BGM_OFF;
    public GameObject ES_OFF;

    void Start()
    {
        nickName_Text.text = Palyfab_Login.myPlayfabInfo;

        //Debug.Log($"SoundManager.instance.canEffect ::: {SoundManager.instance.canEffect}");
        //ES_OFF.SetActive(!SoundManager.instance.CanEffect());
        //ES_OFF.SetActive(!SoundManager.instance.canEffect);
        //BGM_OFF.SetActive(!SoundManager.instance.CanBGM());
    }

    public void OnClickProfileBtn()
    {
        //SoundManager.instance.EffefctPlay(6);

        // 만약에 popUpProfile이 활성화가 되어있으면 비활성화
        if (popUpProfile.activeSelf == true)   //얘는 이제 쓸모가 없어져 버렸음
        {
            popUpProfile.SetActive(false);
            BlackBG.SetActive(false);
        }
        // 그렇지 않으면 활성화
        else
        {
            popUpProfile.SetActive(true);
            BlackBG.SetActive(true);
        }

    }

    //위에 네줄을 한방에 처리하는 문구
    public void OnClickOptionBtn()
    {
        //SoundManager.instance.EffefctPlay(6);
        popUpOption.SetActive(!popUpOption.activeSelf);
        BlackBG.SetActive(popUpOption.activeSelf); // 뒷배경 out of 안중으로 처리 함 (약간 어둡게)
    }

    public void OnClickRoomMakerBtn()
    {
        //SoundManager.instance.EffefctPlay(6);
        PopupRoomMaker.SetActive(!PopupRoomMaker.activeSelf);
        BlackBG.SetActive(PopupRoomMaker.activeSelf);
    }

    public void OnClickGameOptionBtn()
    {
        //SoundManager.instance.EffefctPlay(6);
        PopupGameOption.SetActive(!PopupGameOption.activeSelf);
        BlackBG.SetActive(PopupGameOption.activeSelf);
    }

    public void OnClickOptionExitBtn()
    {
        //SoundManager.instance.EffefctPlay(6);
        popUpOption.SetActive(false);
        BlackBG.SetActive(false);
    }

    public void OnClickEffectBtn()
    {
        //SoundManager.instance.EffefctPlay(6);
        //SoundManager.instance.EnableEffect();
        ES_OFF.SetActive(!ES_OFF.activeSelf); // off 꺼졌다 켜졌다 하기
    }

    public void OnClickBGMBtn()
    {
        //SoundManager.instance.EffefctPlay(6);
        //SoundManager.instance.EnableBGM();

        Debug.Log($"BGM_OFF.activeSelf ::: {BGM_OFF.activeSelf}");
        BGM_OFF.SetActive(!BGM_OFF.activeSelf); // off 꺼졌다 켜졌다 하기
    }
}
