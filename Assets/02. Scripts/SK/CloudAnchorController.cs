using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleARCore;
using GoogleARCore.CrossPlatform;
using Photon.Pun;

//Cloud Anchor Hosting 및 Resolving 관리

public class CloudAnchorController : MonoBehaviour
{
    public MultyScreenMgr multyScreenMgr;

    //게임 보드와 게임 보드 안에 있는 background
    public GameObject gameBoard;
    public GameObject backGround;

    //map 프리팹
    public GameObject mapObj;
    public GameMap gameMap;

    //호스팅 버튼과 리졸브 버튼
    public GameObject hostBt;
    public GameObject resolveBt;

    public bool isHostingFinish = false;
    public bool isResolvingFinish = false;

    private GameObject player;
    private PlayerMgr playerMgr;
    public PhotonView myPhotonView;

    public TouchManager touchManager;
    AsyncTask<CloudAnchorResult> result_AsyncTask;

    public GameObject pointImage;
    public CubeSetting cubeSetting;
    public GameObject boardSizePanel;
    public GameObject clientBoardSizePanel;
    public GameObject waitingClientPopup;
    public GameObject waitingMasterPopup;
    public GameObject masterMapCreateHelp;

    private int mapOnCount = 0;

    private void Start()
    {
        isHostingFinish = false;
        isResolvingFinish = false;

        player = GameObject.FindGameObjectWithTag("MINE");
        myPhotonView = player.GetComponent<PhotonView>();
        playerMgr = player.GetComponent<PlayerMgr>();
    }

    private void Update()
    {
        if(!PhotonNetwork.IsMasterClient && PlayerMgr.isReceive == true)
        {
            Debug.Log("이제 패널 끄자");
            //방장이 맵을 생성할 때까지 잠시만 기다려주세요! 끄기
            waitingMasterPopup.SetActive(false);
            resolveBt.SetActive(true);
            PlayerMgr.isReceive = false;
        }

        if (isResolvingFinish == true && mapOnCount == 0)
        {
            print("CloudAnchorController ::: " + isResolvingFinish);
            ResolveFinish();
            mapOnCount = 1;

            //다른 플레이어가 맵을 생성할 때까지 잠시만 기다려주세요! 끄기
            waitingClientPopup.SetActive(false);
        }
    }

    public void CloudAnchor_Hosting()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            print($"CloudAnchorController ::: 호스팅버튼 // {PhotonNetwork.IsMasterClient}");
            print("playerCs.isReceive ///" + PlayerMgr.isReceive);
            print("호스트클라우드앵커 코루틴 실행");
            //print("호스트클라우드앵커 실행");

            //코루틴 실행
            StartCoroutine(HostCloudAnchor(touchManager.anchor));
            //HostCloudAnchor(touchManager.anchor);
        }
    }

    public void CloudAnchor_Resolving()
    {
        if (!PhotonNetwork.IsMasterClient && PlayerMgr.isReceive == true)
        {
            //print("리졸브클라우드앵커 코루틴 실행");
            print("리졸브클라우드앵커 실행");
            print(PlayerMgr.cloudID);
            StartCoroutine(ResolveCloudAnchor(PlayerMgr.cloudID));//코루틴 실행
            //ResolveCloudAnchor(PlayerMgr.cloudID);//코루틴 실행
            print("ID 받았어요");
            //isResolvingFinish = true;
            //print($"Touchmgr ::: isresolveFinish // {isResolvingFinish}");
        }
    }
    IEnumerator HostCloudAnchor(Anchor anchor)
    {
        result_AsyncTask = XPSession.CreateCloudAnchor(anchor);
        
        yield return new WaitUntil(() => result_AsyncTask.IsComplete);
        Debug.Log(result_AsyncTask.Result.Response);
        Debug.Log(result_AsyncTask.Result.Anchor);
        Debug.Log(result_AsyncTask.Result.Anchor.CloudId);

        string cloudId = result_AsyncTask.Result.Anchor.CloudId;
        Quaternion gameboardQuaternion = gameBoard.transform.rotation;
        Vector3 gameboardPos = gameBoard.transform.position;

        myPhotonView.RPC("SendCloudInfo", RpcTarget.Others, cloudId, gameboardQuaternion, gameboardPos);
        hostBt.SetActive(false);
        isHostingFinish = true;

        //다른 플레이어가 맵을 생성할 때까지 잠시만 기다려주세요! 켜기
        waitingClientPopup.SetActive(true);
        backGround.SetActive(true);
        
        print("!!!호스트 앵커 위치 :  " + anchor.transform.position);
        print("호스트 게임보드 위치 :  " + gameBoard.transform.position);
    }

    IEnumerator ResolveCloudAnchor(string cloudID)
    {
        Debug.Log("리졸빙 코루틴 들어옴" + cloudID);
        result_AsyncTask = XPSession.ResolveCloudAnchor(cloudID);

        yield return new WaitUntil(() => result_AsyncTask.IsComplete);
        Debug.Log("리졸빙 응답 대기중....");
        Debug.Log(result_AsyncTask.Result.Response);
        Debug.Log(result_AsyncTask.Result.Anchor);
        Debug.Log(result_AsyncTask.Result.Anchor.CloudId);

        ////GameBoard 생성
        gameBoard.SetActive(true);

        // GameBoard 위치 
        gameBoard.transform.position = result_AsyncTask.Result.Anchor.transform.position;
        print("!!!클라이언트가 받은 앵커 위치는 : " + result_AsyncTask.Result.Anchor.transform.position);

        //Gameboard 방향
        gameBoard.transform.rotation = PlayerMgr.gameboardQuaternion_;

        mapObj = Instantiate(gameMap.map, backGround.transform.position, gameBoard.transform.rotation, backGround.transform);
        print("클라이언트 맵 위치 :  " + mapObj.transform.position);

        resolveBt.SetActive(false);
        pointImage.SetActive(true);
        cubeSetting.enabled = true;

        //클라이언트일 경우 보드 리셋 버튼 숨김
        boardSizePanel.SetActive(true);
        GameObject boadResetBt = boardSizePanel.transform.GetChild(1).gameObject;
        boadResetBt.SetActive(false);

        print($"{result_AsyncTask.Result.Anchor.transform.position} ::: {gameBoard.transform.position}");

        if (result_AsyncTask.Result.Anchor.transform.position == gameBoard.transform.position)
        {
            Debug.Log("둘이 똑같다");
        }
        isResolvingFinish = true;
        print($"Touchmgr ::: isresolveFinish // {isResolvingFinish}");
    }

    public void ResolveFinish()
    {
        if (!PhotonNetwork.IsMasterClient)
        {
            print("ResolveFinish() 실행 RpcMapOnCountUp 실행해줘");
            multyScreenMgr.myPhotonView.RPC("RpcMapOnCountUp", RpcTarget.MasterClient);
        }
    }
}
