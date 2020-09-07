using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UI;
public class Connection : MonoBehaviourPunCallbacks
{
    string gameVersion = "1";
  
    public void OnClickConnect()
    {
        PhotonNetwork.CrcCheckEnabled = true;
        PhotonNetwork.GameVersion = gameVersion;
        PhotonNetwork.ConnectUsingSettings();//1
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

        PhotonNetwork.NickName = Palyfab_Login.myPlayfabInfo;
    }

    public override void OnJoinedLobby()//3
    {
        Debug.Log("-OnJoinedLobby");
        PhotonNetwork.LoadLevel(10);
    }

}
