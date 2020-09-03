using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class RoomCreat : MonoBehaviour
{
    private LobbyMgr lobbyMgr;
    public GameObject roomMaker_Panel;
    public GameObject RoomList_Panel;

    private byte two = 2;
    private byte three = 3;

    const string playerNamePrefKey = "PlayerName";



    public void Start()
    {


        lobbyMgr = GameObject.Find("LobbyMgr").GetComponent<LobbyMgr>();


    }
    public void Send2()
    {

        lobbyMgr.personNum = two;
    }

    public void Send3()
    {

        lobbyMgr.personNum = three;
    }

      public void OnRoomMaker_Panel()
    {
        RoomList_Panel.SetActive(false);
        roomMaker_Panel.SetActive(true);
    }
}
