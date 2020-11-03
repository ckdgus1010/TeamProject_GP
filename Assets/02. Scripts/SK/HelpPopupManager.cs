using Lee;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HelpPopupManager : MonoBehaviour
{
  
    public GameObject settingCanvas;

    public GameObject createMode_Help_Panel;
    public GameObject alone_Help_Panel;
    public GameObject alone1_Help_Panel;
    public GameObject alone2_Help_Panel;
    public GameObject alone3_Help_Panel;
    public GameObject roomMaker_Help_Panel;
    public GameObject waitingRoom_Help_Panel;
    public GameObject multy_Help_Panel;
    public GameObject profile_Help_Panel;
    public GameObject selectGamemode_Help_Panel;
    public GameObject first_Help_Panel;

    public GameObject currentPanel;
    public GameObject currentBGPanel;

    public GameObject list_masterHelp;
    public GameObject list_clientHelp;
    public GameObject list_mainHelp;

    public GameObject wait_masterHelp;
    public GameObject wait_clientHelp;
    [SerializeField] private Scrollbar scrollbar;
    [SerializeField] private ScrollRect scrollRect;
    public SwipeMenu swipeMenu;

    public GameObject createBg;
    public GameObject aloneBg;
    public GameObject togetherBg;

    public ButtonManager01 buttonManager01;
    public bool settingOn = false;
    //설정 버튼 클릭
    public void OnClickSetting()
    {
        if(buttonManager01 != null && buttonManager01.profileOn == true)
        {
            return;
        }

        if (swipeMenu != null)
        {
            swipeMenu.enabled = !settingCanvas.activeSelf;
        }

        settingCanvas.SetActive(!settingCanvas.activeSelf);

        settingOn = !settingOn;
    }

    //설정버튼의 도움말 버튼 클릭
    public void OnClickHelpButton()
    {
        switch (GameManager.Instance.modeID)
        {
            case 0:
                createMode_Help_Panel.SetActive(true);
                currentPanel = createMode_Help_Panel;
                break;

            case 1:
                alone_Help_Panel.SetActive(true);
                currentPanel = alone_Help_Panel;
                break;

            case 2:
                alone1_Help_Panel.SetActive(true);
                currentPanel = alone1_Help_Panel;

                break;

            case 3:
                alone2_Help_Panel.SetActive(true);
                currentPanel = alone2_Help_Panel;
                break;

            case 4:
                alone3_Help_Panel.SetActive(true);
                currentPanel = alone3_Help_Panel;

                break;

            case 5:
                multy_Help_Panel.SetActive(true);
                currentPanel = multy_Help_Panel;
                break;

            case 6:
            case 7:
            case 8:
                waitingRoom_Help_Panel.SetActive(true);
                currentPanel = waitingRoom_Help_Panel;
                break;
            case 9:
                selectGamemode_Help_Panel.SetActive(true);
                currentPanel = selectGamemode_Help_Panel;
                break;
            case 1000:
                first_Help_Panel.SetActive(true);
                currentPanel = first_Help_Panel;
                break;
        }
    }
   
    // 프로필 설명 
    public void OnClickProfileHelp()
    {
        profile_Help_Panel.SetActive(true);
        currentPanel = profile_Help_Panel;
    }

    //CreateMode 도움말
    public void OnClickCreate_Help()
    {
        createMode_Help_Panel.SetActive(true);
        currentPanel = createMode_Help_Panel;
        swipeMenu.enabled = false;
        scrollbar.value = 0.0f;
        scrollRect.enabled = false;
        createBg.SetActive(true);
        currentBGPanel = createBg;
    }
    // Alone 도움말
    public void OnClickAlone_Help()
    {
        alone_Help_Panel.SetActive(true);
        currentPanel = alone_Help_Panel;
        swipeMenu.enabled = false;
        scrollbar.value = 0.5f;
        scrollRect.enabled = false;
        aloneBg.SetActive(true);
        currentBGPanel = aloneBg;
    }
    // Alone 1 도움말
    public void OnClickAlone1_Help()
    {
        alone1_Help_Panel.SetActive(true);
        currentPanel = alone1_Help_Panel;
        swipeMenu.enabled = false;

        scrollRect.enabled = false;
    }
    // Alone 2 도움말
    public void OnClickAlone2_Help()
    {
        alone2_Help_Panel.SetActive(true);
        currentPanel = alone2_Help_Panel;
        swipeMenu.enabled = false;

        scrollRect.enabled = false;
    }
    // Alone 3 도움말
    public void OnClickAlone3_Help()
    {
        alone3_Help_Panel.SetActive(true);
        currentPanel = alone3_Help_Panel;
        swipeMenu.enabled = false;
       
        scrollRect.enabled = false;
    }

    // 같이하기 도움말
    public void OnClickMulty_Help()
    {
        multy_Help_Panel.SetActive(true);
        currentPanel = multy_Help_Panel;
        swipeMenu.enabled = false;
        scrollbar.value = 1.0f;
        scrollRect.enabled = false;
        togetherBg.SetActive(true);
        currentBGPanel = togetherBg;

    }

    //같이하기 대기방 방장 설명
    public void OnClickMasterWaitHelp()
    {
        print(":: 대기방 방장도움말 클릭");

        wait_masterHelp.SetActive(true);
        wait_clientHelp.SetActive(false);
    }

    // 같이하기 대기방 팀원 설명
    public void OnClickCilentWaitHelp()
    {
        print(":: 대기방 팀원도움말 클릭");
        wait_masterHelp.SetActive(false);
        wait_clientHelp.SetActive(true);
    }

    // 방 만들기 버튼 클릭
    public void OnClickRoommakerHelp()
    {

        roomMaker_Help_Panel.SetActive(true);
        currentPanel = roomMaker_Help_Panel;
    }

    // 같이하기 셋팅 나가기
    public void OnClickMultySetting()
    {
        settingCanvas.SetActive(!settingCanvas.activeSelf);
        list_mainHelp.SetActive(true);
        list_clientHelp.SetActive(false);
        list_masterHelp.SetActive(false);
    }

    // 같이하기 방장 설명 
    public void OnClickMasterHelp()
    {
        list_masterHelp.SetActive(true);
        list_clientHelp.SetActive(false);
        list_mainHelp.SetActive(false);
    }

    // 같이하기 팀원 설명 
    public void OnClickClientHelp()
    {
        list_masterHelp.SetActive(false);
        list_clientHelp.SetActive(true);
        list_mainHelp.SetActive(false);
    }

    // 도움말 나가기 버튼
    public void Normal_X_Button()
    {
        Debug.Log("Normal_X_Button()");
        if (currentPanel != null)
        {
            Debug.Log("Normal_X_Button1111()");
            currentPanel.SetActive(false);
            currentPanel = null;
            swipeMenu.enabled = true;
            scrollRect.enabled = true;
        }

        if(currentBGPanel != null)
        {
            Debug.Log("Normal_X_Button2222()");
            currentBGPanel.SetActive(false);
            currentBGPanel = null;
        }
    }

    public void RoomList_Wait_X()
    {
        switch (GameManager.Instance.modeID)
        {
            // 같이하기 나가기 버튼
            case 5:
                if (currentPanel != null)
                {
                    currentPanel.SetActive(false);
                }
                list_mainHelp.SetActive(true);
                list_masterHelp.SetActive(false);
                list_clientHelp.SetActive(false);
                break;
           
                // 같이하기 대기방 나가기 버튼
            case 6:
            case 7:
            case 8:
                if (currentPanel != null)
                {
                    currentPanel.SetActive(false);
                }
                if (list_mainHelp != null)
                {
                    list_mainHelp.SetActive(true);
                }

                wait_masterHelp.SetActive(true);
                wait_clientHelp.SetActive(false);
                break;
        }
    }

    // 같이하기 대기방 나가기 버튼
    public void Waiting_X_Button()
    {
        if (currentPanel != null)
        {
            currentPanel.SetActive(false);
        }
        if (list_mainHelp != null)
        {
            list_mainHelp.SetActive(true);
        }

        wait_masterHelp.SetActive(true);
        wait_clientHelp.SetActive(false);
    }

    // 같이하기 나가기 버튼
    public void RoomList_X_Button()
    {
        if (currentPanel != null)
        {
            currentPanel.SetActive(false);
        }
        list_mainHelp.SetActive(true);
        list_masterHelp.SetActive(false);
        list_clientHelp.SetActive(false);
    }

}
