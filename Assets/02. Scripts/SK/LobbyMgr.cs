using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LobbyMgr : MonoBehaviourPunCallbacks
{
    string gameVersion = "1";

    public byte personNum;
    private byte two = 2;
    private byte three = 3;

    public int roomLevel;

    public InputField roomInputField;

    public GameObject roomInfo;
    public GameObject roomMaker_Panel;
    public GameObject roomList_Panel;
    public GameObject joinRoomfailPopup;
    public GameObject blackBG;

    public Transform content;

    public Button joinRoom_Bt;

    private bool sendPerson = false;

    public LoadingPanelController loadingPanelController;

    public void Start()
    {
        PhotonNetwork.GameVersion = gameVersion;
        PhotonNetwork.ConnectUsingSettings();//1
    }
    private void Update()
    {
        joinRoom_Bt.interactable = roomInputField.text.Length > 0 && sendPerson == true;// 불값으로 나오니까 if문을 한줄로 줄일 수 있음
    }


    public override void OnConnected()
    {
        base.OnConnected();
        print(System.Reflection.MethodBase.GetCurrentMethod().Name);
    }

    public override void OnConnectedToMaster()//2  로비에 들어갈수 있는 상태
    {
        Debug.Log("-OnConnectedToMaster");
        //PhotonNetwork.NickName = ID.text; 나중에 프로필 구현 된 다음그 아이디 받아오기

        //로비로 진입
        PhotonNetwork.JoinLobby();

        PhotonNetwork.NickName = GameManager.Instance.username;
    }
    public override void OnJoinedLobby()//3
    {
        Debug.Log("-OnJoinedLobby");
        //PhotonNetwork.LoadLevel("11. TogetherModeList");
    }


    public void OnClickCreateRoom()
    {
        PhotonNetwork.CreateRoom(roomInputField.text, new RoomOptions { MaxPlayers = personNum }); // 그럼 방을 만들어 준다.개
        // Debug.Log("방이름  :   " + roomInputField.text);
        // Debug.Log("MaxPlayer  :   " + personNum + " 명");
        Debug.Log("-CreateRoom");
    }
    public override void OnCreatedRoom()
    {
        print(System.Reflection.MethodBase.GetCurrentMethod().Name); // 해당 함수를 프린트 해줌
    }
    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        print(System.Reflection.MethodBase.GetCurrentMethod().Name); // 해당 함수를 프린트 해줌
    }
    public void OnClickJoinRoom()
    {
        PhotonNetwork.JoinRoom(roomInputField.text);
    }

    public override void OnJoinedRoom()
    {
        PhotonNetwork.LoadLevel("12. TogetherModeWait");
    }

    public override void OnJoinRoomFailed(short returnCode, string message)
    {
        base.OnJoinRoomFailed(returnCode, message);
        print("OnJoinRoomFailed");
        joinRoomfailPopup.SetActive(true);
        blackBG.SetActive(true);
    }

    public void OnClickXbt()
    {
        joinRoomfailPopup.SetActive(false);
        blackBG.SetActive(false);
    }
    // <"name"이라는  key 값에 수경이라는 값을 넣겠다.
    // <key 값, value 값>
    Dictionary<string, RoomInfo> cacheRoom = new Dictionary<string, RoomInfo>();
    public override void OnRoomListUpdate(List<RoomInfo> roomList)//4  
    {
        Debug.Log("-OnRoomListUpdate");

        DeleteRoomListUI();

        UpdateCacheRoom(roomList);

        CreateRoomListUI();
        loadingPanelController.FadeOut();
        //loadingPanel.SetActive(false);
        // 룸 리스트를 업데이트 다 한 다음에 화면 켜지기 

        // for (int i = 0; i < roomList.Count; i++)
        // {
        //     print("이름 : " + roomList[i].Name + "현재인원" + roomList[i].PlayerCount + "최대인원 : " + roomList[i].MaxPlayers);
        // }
        // if (roomList.Count > 0)
        // {
        //     CreateRoomListUI();
        // }
    }
    void DeleteRoomListUI()
    {
        // 일단 roomlistContent 자식을 다 지우고
        foreach (Transform tr in content)
        {
            Destroy(tr.gameObject); //트랜스폼을 가지고 있는 게임오브젝트를 지운다.
            Debug.Log("-DeleteRoomListUI");
        }
    }
    void UpdateCacheRoom(List<RoomInfo> roomList)
    {
        // 1. roomList를 순차적으로 돌면서 
        for (int i = 0; i < roomList.Count; i++)
        {
            // 2. 해당이름이 cacheRoom에 key 값으로 설정이 되었다면
            if (cacheRoom.ContainsKey(roomList[i].Name))
            {
                // 3. 해당 roomInfo 갱신(변경, 삭제)
                // 만약에 방이 삭제가 되었다면 
                if (roomList[i].RemovedFromList)// RemovedFromList 이게 트루면 룸에서 삭제 되었다라는 뜻 방에서 누가 나가면 방은 사라짐
                {
                    cacheRoom.Remove(roomList[i].Name); // 룸리스트 네임을 가진 애를 삭제하라
                    continue; // 다음 룸 네임으로 던져줌
                }
            }
            // 4. 그렇지 않으면 roomInfo 를 cacheRoom 추가 또는 변경한다
            cacheRoom[roomList[i].Name] = roomList[i];   // roomList[i]변경된 키값이 들어가져있음
            Debug.Log("-UpdateCacheRoom");

        }
    }
   
    void CreateRoomListUI()
    {
        foreach (RoomInfo info in cacheRoom.Values)// 위에있는 캐시에 있는 만큼 룸들을 다 돌면서 방을 생성한다.
        {
            GameObject room = Instantiate(roomInfo);

            room.transform.SetParent(content);
            Debug.Log("-CreateRoomListUI");

            room.transform.SetParent(content);
            room.GetComponent<RoomInButton>().SetInfo(info.Name, info.PlayerCount, info.MaxPlayers);
        }
    }

    public void Send2()
    {
        personNum = two;
        sendPerson = true;
    }

    public void Send3()
    {
        personNum = three;
        sendPerson = true;
    }

    public void OnRoomMaker_Panel()
    {
        roomList_Panel.SetActive(false);
        roomMaker_Panel.SetActive(true);
    }

    public void OnroomList_Panel()
    {
        roomList_Panel.SetActive(true);
        roomMaker_Panel.SetActive(false);
    }
    public void OnClickLeaveLobby()
    {
        SceneManager.LoadScene("05. PlayMode");
        PhotonNetwork.Disconnect();
        print(System.Reflection.MethodBase.GetCurrentMethod().Name);
        //PhotonNetwork.LeaveLobby();
    }
}
