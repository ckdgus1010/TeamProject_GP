using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMgr : MonoBehaviourPun
{
    public static string cloudID;
    public static bool isReceive;

    //proFile = 준비 상태 불들어오는거
    //public List<Profile> proFileList;
    public GameObject cubeFac;
    public int myIndexNumber;
    public bool isRecevied;

    public static Quaternion gameboardQuaternion_;
    public static Vector3 gameboardTransform_;
    // public Profile pf;
    //public PhotonView myphotonView;
    public bool isready;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    public void Update()
    {
       
    }

    // 프로필 만들기
    [PunRPC]
    void RpcMakeProfile(string nickName) //1
    {
        WatingButtonMgr.instance.CreatePlayerListUI(nickName);
    }

    // 마스터가 게임 시작 버튼 눌렀을때 
    [PunRPC]
    void RpcMasterSetReady()
    {
        WatingButtonMgr.instance.OnClickGameStart();
    }

    // 클라이언트 일때 프로필 준비버튼 색 바꾸는 함수
    [PunRPC]
    //void RpcSetReady(string nickName, bool isReady)
    void RpcSetReady(string nickName)
    {
       
        WatingButtonMgr.instance.OnClickGameReady(nickName);
        print("RPC에서 OnClickGameReady 으로 보냄 ");
    }

    // 마스터가 바꾸는 맵정보 띄우기  >>
    [PunRPC]
    void RpcNextMapText(int map_Count)
    {
        WatingButtonMgr.instance.ChangeMapText(map_Count);
    }

    // 마스터가 바꾸는 맵정보 띄우기 <<
    [PunRPC]
    void RpcBackMapText(int map_Count)
    {
        WatingButtonMgr.instance.ChangeMapText(map_Count);
    }

    // 마스터가 설정한 게임 난이도 넘기기 , 색 띄우기 
    [PunRPC]
    void RpcSendLevel(Levels mLevel, int i)
    {
        //SelectLevel.instance.mLevel = mLevel;
        WatingButtonMgr.instance.curruntLevels = mLevel;
        SelectLevel.instance.selectLevels[i].SelectColor();
    }

    //선택되지 않은 난이도 색 끄기
    [PunRPC]
    void RpcUnSelectColor(int i)
    {
        SelectLevel.instance.selectLevels[i].UnSelectColor();
    }

    // 마스터가 올린 클라우드 앵커아이디 클라이언트는 받기 
    [PunRPC]
    public void SendCloudInfo(string cloudId, Quaternion gameboardQuaternion, Vector3 gameboardTransform)
    {
        cloudID = cloudId;
        print("클라우드아이디 받음");
        print(cloudID);
        isReceive = true;
        print("Player isReceive : " + isReceive);
        gameboardQuaternion_ = gameboardQuaternion;
        gameboardTransform_ = gameboardTransform;
        //isRecevied = true;
        //print(isRecevied);
        //isReceiveId = true;
        // 이제 리졸브버튼 누르세요 하기
    }

    // 마스터가 설정한 설정한 난이도와 스테이지 클라이언트들에게 넘기기 
    [PunRPC]
    public void RpcSetGameData(int _modeID, int _stageID)
    {
        GameManager.Instance.modeID = _modeID;
        GameManager.Instance.stageID = _stageID;

        Debug.Log($"PlayerMgr ::: {PhotonNetwork.IsMasterClient} \n {GameManager.Instance.modeID} // {_modeID} ::: {GameManager.Instance.stageID} // {_stageID}");
    }

    [PunRPC]
    public void RpcReadyCountUp()
    {
        WatingButtonMgr.instance.readyCount += 1;
    }
    [PunRPC]
    public void RpcReadyCountDown()
    {
        WatingButtonMgr.instance.readyCount -= 1;
    }
    
}