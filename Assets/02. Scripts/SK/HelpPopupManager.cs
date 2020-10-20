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
    public GameObject selectGamemode_Help_Panel;

    public GameObject currentPanel;
    public GameObject masterHelp;
    public GameObject clientHelp;
    public GameObject mainHelp;
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
                profile_Help_Panel.SetActive(true);
                currentPanel = profile_Help_Panel;
                break;
        }
    }
    public void Normal_X_Button()
    {
        currentPanel.SetActive(false);
        
    }
    public void Waiting_X_Button()
    {
        if (currentPanel != null)
        {
            currentPanel.SetActive(false);
        }
        if (mainHelp != null)
        {
            mainHelp.SetActive(true);
        }

        masterHelp.SetActive(true);
        clientHelp.SetActive(false);
    }
    public void RoomList_X_Button()
    {
        if (currentPanel != null)
        {
            currentPanel.SetActive(false);
        }
        mainHelp.SetActive(true);
        masterHelp.SetActive(false);
        clientHelp.SetActive(false);
    }
    public void OnClickRoommakerHelp()
    {
        roomList_Help_Panel.SetActive(true);
        currentPanel = roomList_Help_Panel;
    }
}
