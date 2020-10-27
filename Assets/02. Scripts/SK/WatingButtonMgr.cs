using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UI;
using System;
//using Google.Protobuf.WellKnownTypes;
using Photon.Pun.UtilityScripts;
//using WebSocketSharp;
using UnityEngine.SceneManagement;
using UnityEngine.Assertions.Must;
using System.Linq;
using Photon.Pun.Demo.Cockpit;
using Hashtable = ExitGames.Client.Photon.Hashtable;
public enum Levels
{
    Easy, Nomal, Hard, none
}
public class WatingButtonMgr : MonoBehaviourPunCallbacks
{
    public static WatingButtonMgr instance;
    //게임 준비버튼을 누르면 게임 준비의 컬러가 바뀐다. 프로필 옆에 준비 버튼에 불이 들어온다
    //팀원은 게임 난이도 조절 및 맵 설정 버튼을 누를 수 없다. 

    //마스터는 게임의 난이도와 맵을 설정한다. 
    //마스터는 다른 팀원이 준비 버튼을 누르지 않으면 게임시작 버튼을 누를 수 없다.
    //게임 시작 버튼을 누르면 게임이 시작된다. 패널이 꺼진다
    //패널이 꺼질때 난이도와 맵 설정에 맞는 게임 플레이 그리드와 문제가 나온다.


    //public GameObject mapsellectOption_Panel;

    public GameObject profileFac;
    public GameObject master_ProfileFac;
    public GameObject dontGameStartPopup;
    public GameObject settingCanvas;


    public Transform content;
    public PhotonView myPhotonView;

    public int readyCount = 0;
    public int mapCount = 0;
    public bool issReady;
    public bool isClientReady;
    public RoomInfo roomInfo;

    public List<Profile> proFileList;

    List<string> mapList2;
    private Text mapName;
    public Text gameStart_Ready;
    public Text currentRoomName;

    private GameObject player;
    private PlayerMgr playerCs;
    public Levels curruntLevels;

    private GameObject profile;

    //플레이어 인덱스 번호 받기
    public int[] playerList = { 0, 0, 0 };
    public GameObject[] profileList;
    public int myIndexNumber;
    public int emptyIndex;
    public int playerIndex;
    public Array array;
    public bool ismaster_Mark;
    public GameObject masterHelp;
    public GameObject clientHelp;

    //Hashtable CP = new Hashtable();
    private void Start()
    {

        GameManager.Instance.modeID = 7;
        print(PhotonNetwork.PlayerList.Length);
        instance = this;
        PhotonNetwork.AutomaticallySyncScene = true;

        proFileList = new List<Profile>();
        mapList2 = new List<string>();

        //방이름
        currentRoomName.text = PhotonNetwork.CurrentRoom.Name;

        //플레이어 생성
        player = PhotonNetwork.Instantiate("Player", Vector3.zero, Quaternion.identity);
        myPhotonView = player.GetComponent<PhotonView>();
        playerCs = player.GetComponent<PlayerMgr>();


        if (myPhotonView.IsMine)
        {
            player.tag = "MINE";
        }

        mapName = GameObject.Find("MapText").GetComponent<Text>();
        AddmapList();
        mapName.text = mapList2[0];

        if (PhotonNetwork.IsMasterClient)
        {
            gameStart_Ready.text = "Game Start";
        }
        else
        {
            gameStart_Ready.text = "Ready";
        }

        if (PhotonNetwork.IsMasterClient)
        {
            AddPlayer(PhotonNetwork.LocalPlayer.ActorNumber);
        }
          Debug.Log(PhotonNetwork.CurrentRoom.MaxPlayers);

        //CP = PhotonNetwork.CurrentRoom.CustomProperties;
        //print(" 들어왔고 룸프로퍼티 줘라 ::: "+CP);
        
    }

    //public override void OnRoomPropertiesUpdate(Hashtable propertiesThatChanged)
    //{
    //    photonView.RPC("RoomPropertiesUpdate", RpcTarget.All, propertiesThatChanged);
    //}

    //[PunRPC]
    //public void RoomPropertiesUpdate(Hashtable propertiesThatChanged)
    //{
    //    CP = PhotonNetwork.CurrentRoom.SetCustomProperties(propertiesThatChanged);
    //    print(" 새로 받은 프로퍼티ㅣ::::" + CP);
    //}
    // 내 인덱스 번호랑 같은 프로필 번호 오브젝트를 찾는다
    // 거기안에있는 텍스트를 내 닉네임으로 바꾸고 rpc로 애들에게 알려준다.
    // 내 인덱스 번호와 같은 프로필 번호 오브젝트에 내꺼라는 표시를 달아준다.
    //

    // 프로필UI 
    public void FindProfile()
    {
        // myIndexNumber_ = myIndexNumber - 1;
        GameObject myProfile = profileList[myIndexNumber];
        print(myProfile.name);
        myProfile.GetComponent<Profile>().isReady = false;
        myProfile.GetComponent<Profile>().img_Ready.color = Color.white;
        myProfile.SetActive(true);

        var profileName = myProfile.transform.GetChild(1).GetComponent<Text>();
        GameObject masterProfileImage = myProfile.transform.GetChild(3).gameObject;
        GameObject clientProfileImage = myProfile.transform.GetChild(4).gameObject;
        GameObject IsmineImage = myProfile.transform.GetChild(5).gameObject;
        GameObject ReadtBt = myProfile.transform.GetChild(2).gameObject;
        profileName.text = PhotonNetwork.NickName;
        IsmineImage.SetActive(true);
        print("FindProfile()에서 내꺼라는 표시");

        if (PhotonNetwork.IsMasterClient)
        {
            masterProfileImage.SetActive(true);
            clientProfileImage.SetActive(false);
            ReadtBt.SetActive(false);
            ismaster_Mark = true;
            print("마스터로 프로필 리스트 업데이트");
            photonView.RPC("RpcProfileLisUpdate", RpcTarget.OthersBuffered, myIndexNumber, PhotonNetwork.NickName, ismaster_Mark);
        }
        else
        {
            clientProfileImage.SetActive(true);
            ismaster_Mark = false;
            print("클라이언트로 프로필 리스트 업데이트");

            photonView.RPC("RpcProfileLisUpdate", RpcTarget.OthersBuffered, myIndexNumber, PhotonNetwork.NickName, ismaster_Mark);

        }
        print(profileName.text);

    }
    [PunRPC]
    public void RpcProfileLisUpdate(int indexNum, string nickname, bool ismasterMark)
    {

        GameObject myProfile = profileList[indexNum];
        print(myProfile.name);
        myProfile.GetComponent<Profile>().isReady = false;
        myProfile.GetComponent<Profile>().img_Ready.color = Color.white;

        photonView.RPC("RpcReadyCountReset", RpcTarget.All);
        myProfile.SetActive(true);

        var profileName = myProfile.transform.GetChild(1).GetComponent<Text>();
        GameObject masterProfileImage = myProfile.transform.GetChild(3).gameObject;
        GameObject clientProfileImage = myProfile.transform.GetChild(4).gameObject;
        GameObject IsmineImage = myProfile.transform.GetChild(5).gameObject;
        profileName.text = PhotonNetwork.NickName;

        if (ismasterMark == true)
        {
            masterProfileImage.SetActive(true);
            clientProfileImage.SetActive(false);
        }
        else
        {
            clientProfileImage.SetActive(true);
        }
        profileName.text = nickname;

    }

    #region Photon

    public void CreatePlayerListUI(string nickName)
    {
        print("CreatePlayerListUI만들기");
        // GameObject profile = Instantiate(profileFac);
        GameObject profile = PhotonNetwork.Instantiate("ProFile", Vector3.zero, Quaternion.identity);
        profile.transform.SetParent(content);

        if (player.tag == "MINE")
        {
            profile.tag = "MINEPROFILE";

        }
        Profile pf = profile.GetComponent<Profile>();
        //pf.SetInfo(nickName);
        proFileList.Add(pf);
    }

    public void OnClickReady()
    {
        Debug.Log(PhotonNetwork.CurrentRoom.MaxPlayers);
        if (myPhotonView.IsMine && PhotonNetwork.IsMasterClient)
        {
            if (readyCount == PhotonNetwork.CurrentRoom.MaxPlayers - 1)
            {
                myPhotonView.RPC("RpcMasterSetReady", RpcTarget.AllBuffered);
            }
            else
            {
                dontGameStartPopup.SetActive(true);
                Debug.Log("모든 플레이어가 준비가 될때까지 기다려주세요");
            }
        }

        else
        {
            isClientReady = !isClientReady;
            if (isClientReady == true)
            {
                myPhotonView.RPC("RpcReadyCountUp", RpcTarget.MasterClient);
            }
            else
            {
                myPhotonView.RPC("RpcReadyCountDown", RpcTarget.MasterClient);

            }
            //myPhotonView.RPC("RpcSetReady", RpcTarget.AllBuffered, PhotonNetwork.NickName, !issReady);//playerCs.proFileList, PhotonNetwork.NickName, !issReady
            myPhotonView.RPC("RpcSetReady", RpcTarget.AllBuffered, PhotonNetwork.NickName);//playerCs.proFileList, PhotonNetwork.NickName, !issReady
        }
    }

    public void OnClickGameReady(string nickName) //List<Profile> proFileList, string nickName, bool isReady
    {
        if (myPhotonView.IsMine)
        {
            print(PhotonNetwork.NickName);
        }

        print("RPC에서 OnClickGameReady 으로 받음 ");

   
        GameManager.Instance.stageID = 1;
        for (int i = 0; i < profileList.Length; i++)
        {
            if (myPhotonView.IsMine)
            {
                profileList[i].GetComponent<Profile>().ChangeReadyState(nickName);
                print("RPC에서 ChangeReadyState 으로 보냄 ");
            }
        }
    }
    public void OnClickGameStart()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            print("RPC에서 OnClickGameStart 으로 받음 ");

            //마스터만 Client들한테 modeID, stageID 넘겨주기
            Debug.Log(readyCount);
            int modeID = GameManager.Instance.modeID;
            // 나중에 난수 생성
            int stageID = UnityEngine.Random.Range(1, 10);
            print(stageID);
            GameManager.Instance.stageID = stageID;
            print(" 와팅 와팅 Mgr:::::" + GameManager.Instance.modeID + "//" + modeID + "//" + stageID);
            myPhotonView.RPC("RpcSetGameData", RpcTarget.Others, modeID, stageID);

            //Scene 전환
            PhotonNetwork.LoadLevel("15. MultiyPlay Scene");
        }
    }

    public void OnClickLeaveRoom()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            if (PhotonNetwork.PlayerList.Length == 1)
            {
                PhotonNetwork.LeaveRoom();
            }
            else
            {
                PhotonNetwork.SetMasterClient(PhotonNetwork.PlayerList[1]);
            }
        }
        else
            PhotonNetwork.LeaveRoom();
            GameManager.Instance.modeID = 5;
        // PhotonNetwork.LoadLevel("11. TogetherModeList");
    }
    public override void OnLeftRoom()
    {
        SceneManager.LoadScene("11. TogetherModeList");
        print(System.Reflection.MethodBase.GetCurrentMethod().Name);

    }
    //public override void OnDisconnected(DisconnectCause cause)
    //{
    //    print(System.Reflection.MethodBase.GetCurrentMethod().Name);
    //    PhotonNetwork.ConnectUsingSettings();
    //}

    public void AddPlayer(int playerActorNumber)
    {
        emptyIndex = Array.IndexOf(playerList, 0);
        playerList[emptyIndex] = playerActorNumber;
        print("RPC_UpdateEnterPlayerList()실행시켜줘");

        photonView.RPC("UpdateEnterPlayerList", RpcTarget.All, playerList);
    }

    public void RemovePlayer(int playerActorNumber)
    {
        playerIndex = Array.IndexOf(playerList, playerActorNumber);
        GameObject outProfile = profileList[playerIndex];

        outProfile.GetComponent<Profile>().isReady = false;
        outProfile.GetComponent<Profile>().img_Ready.color = Color.white;

        //print(outProfile.name);
        outProfile.SetActive(false);

        var profileName = outProfile.transform.GetChild(1).GetComponent<Text>();
        GameObject masterProfileImage = outProfile.transform.GetChild(3).gameObject;
        GameObject clientProfileImage = outProfile.transform.GetChild(4).gameObject;
        profileName.text = "";
        masterProfileImage.SetActive(false);
        clientProfileImage.SetActive(false);
        playerList[playerIndex] = 0;
        print(":::RPC_UpdatePlayerList()실행시켜줘");

        photonView.RPC("UpdatePlayerList", RpcTarget.All, playerList, playerActorNumber);

    }

    // 플레이어가 나가면
    // 그 플레이어의 담당 프로필 정보를 싹지운다.
    //그리고 플레이어 인덱스를 0으로 한다. 
    //

    //플레이어 
    [PunRPC]
    void UpdateEnterPlayerList(int[] newPlayerList)
    {
        playerList = newPlayerList;
        print(PhotonNetwork.NickName + " ::: RPC_UpdatePlayerList() 실행");
        myIndexNumber = Array.IndexOf(playerList, PhotonNetwork.LocalPlayer.ActorNumber);
        FindProfile();
        print("프로필 만들기" + PhotonNetwork.LocalPlayer.NickName);
    }

    [PunRPC]
    void UpdatePlayerList(int[] newPlayerList, int playerActorNumber)
    {

        playerIndex = Array.IndexOf(playerList, playerActorNumber);
        GameObject outProfile = profileList[playerIndex];

        outProfile.GetComponent<Profile>().isReady = false;
        outProfile.GetComponent<Profile>().img_Ready.color = Color.white;

        playerList = newPlayerList;
        print(PhotonNetwork.NickName + " ::: RPC_UpdatePlayerList() 실행");
        myIndexNumber = Array.IndexOf(playerList, PhotonNetwork.LocalPlayer.ActorNumber);
        FindProfile();
        print("프로필 만들기" + PhotonNetwork.LocalPlayer.NickName);

        //print(outProfile.name);
        outProfile.SetActive(false);
    }
    [PunRPC]
    void RpcReadyCountReset()
    {
        readyCount = 0;
    }

    public void Xbt()
    {
        dontGameStartPopup.SetActive(false);
    }

    public void OnClickSetting()
    {
        settingCanvas.SetActive(!settingCanvas.activeSelf);
    }

    public void OnCilckMasterHelp()
    {
        masterHelp.SetActive(true);
        clientHelp.SetActive(false);
    }
    public void OnCilckCilentHelp()
    {
        masterHelp.SetActive(false);
        clientHelp.SetActive(true);

    }
    #endregion

    #region MapSetting

    private void AddmapList()
    {
        mapList2.Add("반짝모래마을");
        mapList2.Add("글포마을");
        mapList2.Add("좀비성");
        mapList2.Add("혼돈의 카오스");
        mapList2.Add("올라프 성");
    }
    public void OnClickRight()
    {
        if (!PhotonNetwork.IsMasterClient) return;
        if (mapCount == 4) return;
        ++mapCount;
        WatingButtonMgr.instance.myPhotonView.RPC("RpcNextMapText", RpcTarget.AllBuffered, mapCount);
    }

    public void OnClikLeft()
    {
        if (!PhotonNetwork.IsMasterClient) return;
        if (mapCount == 0) return;
        --mapCount;
        WatingButtonMgr.instance.myPhotonView.RPC("RpcBackMapText", RpcTarget.AllBuffered, mapCount);
    }

    public void ChangeMapText(int map_Count)
    {
        mapCount = map_Count;
        switch (map_Count)
        {
            case 0:
                mapName.text = mapList2[map_Count]; // 0 반짝반짝 모래마을
                break;
            case 1:
                mapName.text = mapList2[map_Count]; //1 글로브포인트마을
                break;
            case 2:
                mapName.text = mapList2[map_Count]; //2 좀비성
                break;
            case 3:
                mapName.text = mapList2[map_Count]; //3 혼돈의 카오스
                break;
            case 4:
                mapName.text = mapList2[map_Count]; //4 올라프 성
                break;
        }
    }

    #endregion

}