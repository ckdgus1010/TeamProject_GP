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

public enum Levels
{
    Easy, Nomal, Hard
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


    public Transform content;
    public PhotonView myPhotonView;

    public int readyCount = 0;
    public int mapCount = 0;
    public bool issReady;

    List<Profile> proFileList;
    List<string> mapList2;
    private Text mapName;


    public Levels curruntLevels;
    private void Start()
    {
        instance = this;
        proFileList = new List<Profile>();
        mapList2 = new List<string>();
        GameObject player = PhotonNetwork.Instantiate("Player", Vector3.zero, Quaternion.identity);
        myPhotonView = player.GetComponent<PhotonView>();
        PhotonNetwork.AutomaticallySyncScene = true;
        if (myPhotonView.IsMine)
        {
            player.tag = "MINE";
        }
        mapName = GameObject.Find("MapText").GetComponent<Text>();
        AddmapList();
        mapName.text = mapList2[0];

    }


    #region Photon

    public void CreatePlayerListUI(string nickName)
    {
        GameObject profile = Instantiate(profileFac);
        profile.transform.SetParent(content);
        Profile pf = profile.GetComponent<Profile>();
        pf.SetInfo(nickName);
        proFileList.Add(pf);


    }

    public void OnClickReady()
    {
        if (myPhotonView.IsMine && PhotonNetwork.IsMasterClient)
        {

            WatingButtonMgr.instance.myPhotonView.RPC("RpcMasterSetReady", RpcTarget.AllBuffered, PhotonNetwork.NickName, !issReady);
        }

        else
        {
            WatingButtonMgr.instance.myPhotonView.RPC("RpcSetReady", RpcTarget.AllBuffered, PhotonNetwork.NickName, !issReady);
        }
    }

    public void OnClickGameReady(string nickName, bool isReady)
    {
        if (myPhotonView.IsMine)
        {
            print(PhotonNetwork.NickName);
        }

        print("RPC에서 OnClickGameReady 으로 받음 ");

        issReady = isReady;

        for (int i = 0; i < proFileList.Count; i++)
        {
            if (WatingButtonMgr.instance.myPhotonView.IsMine)
            {
                proFileList[i].ChangeReadyState(nickName, isReady);
                print("RPC에서 ChangeReadyState 으로 보냄 ");
            }
        }

    }

    public void OnClickGameStart(string nickName, bool isReady)
    {
        print("RPC에서 OnClickGameStart 으로 받음 ");

        issReady = isReady;
        if(SelectLevel.instance.levelCilck == true)
        {
            for (int i = 0; i < proFileList.Count; i++)
            {
                proFileList[i].ChangeReadyState(nickName, isReady);
                print("RPC에서 ChangeReadyState 으로 보냄 ");

            }

            PhotonNetwork.LoadLevel("15. MultiyPlay Scene");
        }
      

    }


    public void OnClickLeaveRoom()
    {
        //if (PhotonNetwork.IsMasterClient)
        //{
        //    PhotonNetwork.SetMasterClient(PhotonNetwork.PlayerList[1]);
        //}
        PhotonNetwork.LeaveRoom();

    }
    public override void OnLeftRoom()
    {
        PhotonNetwork.LoadLevel("11. TogetherModeList");
        base.OnLeftRoom();
        print(System.Reflection.MethodBase.GetCurrentMethod().Name);

    }
    public override void OnDisconnected(DisconnectCause cause)
    {
        print(System.Reflection.MethodBase.GetCurrentMethod().Name);

    }
    #endregion

    #region MapSetting

    private void AddmapList()
    {
        mapList2.Add("반짝반짝 모래마을");
        mapList2.Add("글로브포인트마을");
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