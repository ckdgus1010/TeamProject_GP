using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviourPun
{
    public static string cloudID;
    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }
    // Start is called before the first frame update
    void Start()
    {
        if (photonView.IsMine)
        {
            photonView.RPC("RpcMakeProfile", RpcTarget.AllBuffered, PhotonNetwork.NickName);
        }
    }

    [PunRPC]
    void RpcMakeProfile(string nickName)
    {
        WatingButtonMgr.instance.CreatePlayerListUI(nickName);
    }

    [PunRPC]
    void RpcMasterSetReady(string nickName, bool isReady)
    {
        WatingButtonMgr.instance.OnClickGameStart(nickName, isReady);
    }



    [PunRPC]
    void RpcSetReady(string nickName, bool isReady)
    {
        WatingButtonMgr.instance.OnClickGameReady(nickName, isReady);
        print("RPC에서 OnClickGameReady 으로 보냄 ");
    }

    [PunRPC]
    void RpcNextMapText(int map_Count)
    {
        WatingButtonMgr.instance.ChangeMapText(map_Count);
    }

    [PunRPC]
    void RpcBackMapText(int map_Count)
    {
        WatingButtonMgr.instance.ChangeMapText(map_Count);
    }

    [PunRPC]
    void RpcSendLevel(Levels mLevel,int i)
    {
        SelectLevel.instance.mLevel = mLevel;
        WatingButtonMgr.instance.curruntLevels = mLevel;
        SelectLevel.instance.selectLevels[i].SelectColor();
    }
    [PunRPC]
    void RpcUnSelectColor(int i)
    {
        SelectLevel.instance.selectLevels[i].UnSelectColor();

    }
    [PunRPC]
    public void SendCloudInfo(string cloudId)
    {
        cloudID = cloudId;
        print("클라우드아이디 받음");
        print(cloudID);
        //isReceiveId = true;
    }
}
