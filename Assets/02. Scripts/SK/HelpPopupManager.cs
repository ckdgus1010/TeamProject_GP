using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HelpPopupManager : MonoBehaviour
{

    public GameObject roomList_Help_Panel;
    public GameObject waitingRoom_Help_Panel;
    public GameObject multy_Help_Panel;
    public GameObject alone_Help_Panel;
    public GameObject alone1_Help_Panel;
    public GameObject alone2_Help_Panel;
    public GameObject alone3_Help_Panel;
    public GameObject createMode_Help_Panel;
    public GameObject profile_Help_Panel;

    public GameObject currentPanel;

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
                roomList_Help_Panel.SetActive(true);
                currentPanel = roomList_Help_Panel;
                break;
            case 1000:
                profile_Help_Panel.SetActive(true);
                currentPanel = profile_Help_Panel;
                break;
        }
    }

    public void X_Button()
    {
        currentPanel.SetActive(false);
    }

    public void OnClickRoommakerHelp()
    {
        roomList_Help_Panel.SetActive(true);
        currentPanel = roomList_Help_Panel;
    }
}
