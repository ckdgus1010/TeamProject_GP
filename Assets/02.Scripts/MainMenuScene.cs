using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MainMenuScene : MonoBehaviour
{
    public GameObject popUpProfile;
    public GameObject popUpOption; //일반 옵션 
    public GameObject PopupRoomMaker; // 11. TogetherModeList
    public GameObject PopupGameOption; //게임 옵션 (뒤로가기 있음)
    public GameObject BlackBG;

    public void OnClickProfileBtn()
    {
        // 만약에 popUpProfile이 활성화가 되어있으면 비활성화
        if(popUpProfile.activeSelf == true)   //얘는 이제 쓸모가 없어져 버렸음
        {
            popUpProfile.SetActive(false);
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
        popUpOption.SetActive(!popUpOption.activeSelf);
        BlackBG.SetActive(popUpOption.activeSelf); // 뒷배경 out of 안중으로 처리 함 (약간 어둡게)
    }

        public void OnClickRoomMakerBtn()
    {
        PopupRoomMaker.SetActive(!PopupRoomMaker.activeSelf);
        BlackBG.SetActive(PopupRoomMaker.activeSelf);       
        
    }

        public void OnClickGameOptionBtn()
    {
        PopupGameOption.SetActive(!PopupGameOption.activeSelf);
        BlackBG.SetActive(PopupGameOption.activeSelf);
    }

    void Update() 
    {
        // UI 와 3D 간의 터치 막는거
        //if(EventSystem.current.IsPointerOverGameObject()) return;  //PC
        
        // if(!EventSystem.current.IsPointerOverGameObject(Input.GetTouch(0).fingerId))  // 모바일
        // {
        // }
    }
}

