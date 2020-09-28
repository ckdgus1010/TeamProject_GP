using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMgr : MonoBehaviourPun
{
    public static string cloudID;
    public static bool isReceive;
    //public List<Profile> proFileList;
    public GameObject cubeFac;
    public int myIndexNumber;
    public static Quaternion gameboardQuaternion_;
    public static Vector3 gameboardTransform_;
    // public Profile pf;
    //public PhotonView myphotonView;


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


        //proFileList = new List<Profile>();
        //myphotonView = this.gameObject.GetComponent<PhotonView>();
        //CreatePlayerListUI(PhotonNetwork.NickName);

    }
    //public void CreatePlayerListUI(string nickName)
    //{
    //    profile = PhotonNetwork.Instantiate("ProFile_ReadtBt", Vector3.zero, Quaternion.identity);
    //    profile.transform.SetParent(WatingButtonMgr.instance.content);
    //    if (myphotonView.IsMine)
    //    {
    //        profile.tag = "MINEPROFILE";
    //        pf = profile.GetComponent<Profile>();
    //        photonView.RPC("RpcSetProfileInfo", RpcTarget.AllBuffered, PhotonNetwork.NickName);
    //        pf.nameText.text = PhotonNetwork.NickName;
    //        proFileList.Add(pf);
    //    }
    //    pf.SetInfo(nickName);
    //    proFileList.Add(pf);
    //}
    [PunRPC]
    void RpcMakeProfile(string nickName) //1
    {
        WatingButtonMgr.instance.CreatePlayerListUI(nickName);
    }
    //[PunRPC]
    //void RpcSetProfileInfo(string nickName)
    //{
    //    pf.SetInfo(nickName);

    //}
    [PunRPC]
    void RpcMasterSetReady(string nickName, bool isReady)
    {
        WatingButtonMgr.instance.OnClickGameStart(nickName, isReady);
    }



    [PunRPC]
    void RpcSetReady(string nickName, bool isReady)
    {
        // WatingButtonMgr.instance.OnClickGameReady(proFileList, nickName, isReady);
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
    void RpcSendLevel(Levels mLevel, int i)
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
    public void SendCloudInfo(string cloudId, Quaternion gameboardQuaternion, Vector3 gameboardTransform)
    {
        cloudID = cloudId;
        print("클라우드아이디 받음");
        print(cloudID);
        isReceive = true;
        print("Player isReceive : " + isReceive);
        gameboardQuaternion_ = gameboardQuaternion;
        gameboardTransform_ = gameboardTransform;
        //isReceiveId = true;
    }

    //[PunRPC]
    //public void RpcMakeCube(GameObject hitObj)
    //{
    //    ButtonManager.instance.RpcMakeCube(hitObj);
    //}

    ///////큐브만들기
    //[PunRPC]
    //public void RpcMakeCube(Vector3 position, Quaternion rotation)
    //{
    //    //GameObject cube = Instantiate(cubeFac, position, rotation);
    //    ButtonManager.instance.Photon_MakeCube(position, rotation);
    //}
    /////////큐브만들기
    //[PunRPC]
    //public void RpcResetCube(Vector3 position, Quaternion rotation)
    //{
    //    //GameObject cube = Instantiate(cubeFac, position, rotation);
    //    ButtonManager.instance.Photon_ResetCube(position, rotation);
    //} 
    /////////큐브만들기
    //[PunRPC]
    //public void RpcDeleteCube(Vector3 position, Quaternion rotation)
    //{
    //    //GameObject cube = Instantiate(cubeFac, position, rotation);
    //    ButtonManager.instance.Photon_DeleteCube(position, rotation);
    //}
}
