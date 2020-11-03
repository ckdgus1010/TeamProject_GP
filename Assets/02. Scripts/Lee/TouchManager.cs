using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using GoogleARCore;
using GoogleARCore.CrossPlatform;
using Photon.Pun;


//목표: 사용자가 touch한 지점에 GameBoard 생성

public class TouchManager : MonoBehaviour
{
    [Header("AR Camera")]
    public Camera cam;
    public GameObject pointImage;
    public CubeSetting cubeSetting;

    [Header("Gameboard")]
    public GameObject gameBoard;
    public Transform gameBoardTr;

    [Header("기타 UI")]
    public CardBoardSetting cardBoardSetting;
    public GameObject gridSizePanel;
    public GameObject blockImg;
    public GameObject cardButton;
    public GameObject playButtons;
    public GameObject playButtons_mode01;

    //Touch 횟수
    [HideInInspector]
    public int count = 0;

    //GameBoard 위치 보정
    [Header("Gameboard 위치 보정")]
    public float height = 0.1f;
    public float depth = 0.5f;

    // 같이하기
    [Header("같이하기")]
    public GameObject backGround;
    public GameObject beachMap;
    public GameObject mapObj;
    public GameObject mapMgr;
    private GameMap gameMap;
    public GameObject masterMapCreateHelp;

    public Quaternion gameboardQuaternion;
    public Vector3  gameboardTransform;

    public PhotonView myPhotonView;

    [HideInInspector] public Anchor anchor;
    AsyncTask<CloudAnchorResult> result_AsyncTask;
    private int hostingCount = 0;

    public GameObject hostBt;
    public GameObject resolveBt;
    private GameObject player;

    public MultyScreenMgr multyScreenMgr;
    public bool ishostFinish;
    public bool isresolveFinish =false;

    public CloudAnchorController cloudAnchorController;
    public GameObject waitingClientPopup;
    public GameObject notePanel;

    [Header("Create Mode Sound")]
    public AudioClip createSound;
    public SoundManager soundMgr;
    void Start()
    {
        count = 0;
        switch (GameManager.Instance.modeID)
        {
            case 0:
            case 2:
            case 3:
            case 4:
                Invoke("OffGameGuidePanel", 4);
                break;
            case 5:
            case 6:
            case 7:
            case 8:
                gameMap = mapMgr.GetComponent<GameMap>();
                player = GameObject.FindGameObjectWithTag("MINE");
                myPhotonView = player.GetComponent<PhotonView>();
                break;
        }
                //같이하기 모드인 경우
        //        if (GameManager.Instance.modeID == 6 || GameManager.Instance.modeID == 7 || GameManager.Instance.modeID == 8 || GameManager.Instance.modeID == 5)
        //{
        //            gameMap = mapMgr.GetComponent<GameMap>();
        //            player = GameObject.FindGameObjectWithTag("MINE");
        //            myPhotonView = player.GetComponent<PhotonView>();
        //            //playerCs = player.GetComponent<PlayerMgr>();
        //        }
    }

    void Update()
    {
        //if (isresolveFinish == true && multyScreenMgr.mapOnCount ==1)
        //{
        //    print(isresolveFinish);
        //    ResolveFinish();
        //}

        if (count == 1 || Input.touchCount == 0)
        {
            return;
        }

        Touch touch = Input.GetTouch(0);
        TrackableHitFlags raycastFilter = TrackableHitFlags.PlaneWithinPolygon
                                          | TrackableHitFlags.FeaturePointWithSurfaceNormal;

        // UI를 터치한 경우 터치 무시
        if (EventSystem.current.IsPointerOverGameObject(touch.fingerId))
        {
            return;
        }

        if (count == 0 && touch.phase == TouchPhase.Began && Frame.Raycast(touch.position.x, touch.position.y, raycastFilter, out TrackableHit hit))
        {
            anchor = hit.Trackable.CreateAnchor(hit.Pose);

            switch (GameManager.Instance.modeID)
            {
                case 0:
                case 2:
                case 3:
                case 4:
                    SoloPlay(hit, anchor);
                    break;
                case 5:
                case 6:
                case 7:
                case 8:
                    if (PhotonNetwork.IsMasterClient)
                    {
                        masterMapCreateHelp.SetActive(false);
                        //GameBoard 생성
                        gameBoard.SetActive(true);

                        //GameBoard 위치 설정
                        gameBoard.transform.position = anchor.transform.position;
                        gameboardTransform = anchor.transform.position;
                        gameBoard.SetActive(true);

                        var rot = Quaternion.LookRotation(cam.transform.position - hit.Pose.position);
                        gameBoard.transform.rotation = Quaternion.Euler(cam.transform.position.x, rot.eulerAngles.y, cam.transform.position.z);
                        gameboardQuaternion = gameBoard.transform.rotation;

                        //GameBoard 생성
                        mapObj = Instantiate(gameMap.map, backGround.transform.position, gameboardQuaternion, backGround.transform);

                        Debug.Log("호스트 앵커 위치 : " + anchor.transform.position);
                        Debug.Log("호스트 게임 보드 위치 : " + gameBoard.transform.position);
                        Debug.Log("호스트 맵 위치 : " + mapObj.transform.position);

                        pointImage.SetActive(true);
                        cubeSetting.enabled = true;
                        //hostBt.SetActive(true);
                        waitingClientPopup.SetActive(true);
                        cloudAnchorController.CloudAnchor_Hosting();
                    }
                    break;
            }

            ////같이하기 모드인 경우
            //if (GameManager.Instance.modeID == 6 || GameManager.Instance.modeID == 7 || GameManager.Instance.modeID == 8 || GameManager.Instance.modeID == 5 )
            //{
            //    if (PhotonNetwork.IsMasterClient)
            //    {
            //        masterMapCreateHelp.SetActive(false);
            //        //GameBoard 생성
            //        gameBoard.SetActive(true);

            //        //GameBoard 위치 설정
            //        gameBoard.transform.position = anchor.transform.position;
            //        gameboardTransform = anchor.transform.position;
            //        gameBoard.SetActive(true);

            //        var rot = Quaternion.LookRotation(cam.transform.position - hit.Pose.position);
            //        gameBoard.transform.rotation = Quaternion.Euler(cam.transform.position.x, rot.eulerAngles.y, cam.transform.position.z);
            //        gameboardQuaternion = gameBoard.transform.rotation;

            //        //GameBoard 생성
            //        mapObj = Instantiate(gameMap.map, backGround.transform.position, gameboardQuaternion, backGround.transform);

            //        Debug.Log("호스트 앵커 위치 : " + anchor.transform.position);
            //        Debug.Log("호스트 게임 보드 위치 : " + gameBoard.transform.position);
            //        Debug.Log("호스트 맵 위치 : " + mapObj.transform.position);

            //        pointImage.SetActive(true);
            //        cubeSetting.enabled = true;
            //        boardSizePanel.SetActive(true);
            //        hostBt.SetActive(true);
            //    }
            //}
            ////Create Mode 또는 혼자하기 모드인 경우
            //else if (GameManager.Instance.modeID != 5)
            //{
            //    SoloPlay(hit, anchor);
            //}

            count = 1;
        }
    }

    #region OnClickHost_ResoleButton() // 주석 처리
    //public void OnClickHost_ResoleButton()
    //{
    //    // playerCs = player.GetComponent<Player>();
    //    print("호스팅버튼");
    //    print("마스터냐 넌 : " + PhotonNetwork.IsMasterClient);
    //    print("hostingCount : " + hostingCount);
    //    print("playerCs.isReceive ///" + PlayerMgr.isReceive);

    //    if (PhotonNetwork.IsMasterClient && hostingCount == 0)
    //    {

    //        print("호스트클라우드앵커 코루틴 실행");
    //        StartCoroutine(HostCloudAnchor(anchor));//코루틴 실행

    //    }
    //    if (!PhotonNetwork.IsMasterClient && PlayerMgr.isReceive == true)
    //    {
    //        print("리졸브클라우드앵커 코루틴 실행");
    //        print(PlayerMgr.cloudID);
    //        StartCoroutine(ResolveCloudAnchor(PlayerMgr.cloudID));//코루틴 실행
    //        print("ID 받았어요");
    //        isresolveFinish = true;
    //        print($"Touchmgr ::: isresolveFinish // {isresolveFinish}");
    //    }
    //}

    //IEnumerator HostCloudAnchor(Anchor anchor)
    //{
    //    result_AsyncTask = XPSession.CreateCloudAnchor(anchor);
    //    yield return new WaitUntil(() => result_AsyncTask.IsComplete);
    //    Debug.Log(result_AsyncTask.Result.Response);
    //    Debug.Log(result_AsyncTask.Result.Anchor);
    //    Debug.Log(result_AsyncTask.Result.Anchor.CloudId);

    //    string cloudId = result_AsyncTask.Result.Anchor.CloudId;
    //    Debug.Log(cloudId);
    //    myPhotonView.RPC("SendCloudInfo", RpcTarget.Others, cloudId, gameboardQuaternion, gameboardTransform);
    //    hostBt.SetActive(false);
    //    ishostFinish = true;
    //    print("!!!호스트 앵커 위치 :  " + anchor.transform.position);
    //    print("호스트 게임보드 위치 :  " + gameBoard.transform.position);
    //    hostingCount = 1;
    //}

    //IEnumerator ResolveCloudAnchor(string cloudID)
    //{
    //    Debug.Log("리졸빙 코루틴 들어옴" + cloudID);
    //    result_AsyncTask = XPSession.ResolveCloudAnchor(cloudID);
    //    yield return new WaitUntil(() => result_AsyncTask.IsComplete);

    //    Debug.Log("리졸빙 응답 대기중....");
    //    Debug.Log(result_AsyncTask.Result.Response);
    //    Debug.Log(result_AsyncTask.Result.Anchor);
    //    Debug.Log(result_AsyncTask.Result.Anchor.CloudId);

    //    ////GameBoard 생성
    //    gameBoard.SetActive(true);

    //    // GameBoard 위치 
    //    gameBoard.transform.position = result_AsyncTask.Result.Anchor.transform.position;
    //    print("!!!클라이언트가 받은 앵커 위치는 : " + result_AsyncTask.Result.Anchor.transform.position);

    //    //Gameboard 방향
    //    gameBoard.transform.rotation = PlayerMgr.gameboardQuaternion_;

    //    mapObj = Instantiate(gameMap.map, backGround.transform.position, gameBoard.transform.rotation, backGround.transform);
    //    print("클라이언트 맵 위치 :  " + mapObj.transform.position);


    //    resolveBt.SetActive(false);
    //    pointImage.SetActive(true);
    //    cubeSetting.enabled = true;
    //    boardSizePanel.SetActive(true);



    //    print($"{result_AsyncTask.Result.Anchor.transform.position} ::: {gameBoard.transform.position}");

    //    if (result_AsyncTask.Result.Anchor.transform.position == gameBoard.transform.position)
    //    {
    //        Debug.Log("둘이 똑같다");
    //    }


    //}

    //public void ResolveFinish()
    //{
    //    if (!PhotonNetwork.IsMasterClient)
    //    {
    //        print("ResolveFinish() 실행 RpcMapOnCountUp 실행해줘");
    //        multyScreenMgr.myPhotonView.RPC("RpcMapOnCountUp", RpcTarget.MasterClient);
    //    }
    //}

    #endregion

    public void SoloPlay(TrackableHit hit, Anchor anchor)
    {
        //맵 생성 도움말
        masterMapCreateHelp.SetActive(false);
        //GameBoard 생성
        gameBoard.SetActive(true);

        //GameBoard 위치 설정
        gameBoard.transform.position = anchor.transform.position;

        //GameBoard 방향 설정
        //gameBoard.transform.rotation = Quaternion.Euler(Vector3.forward);

        var rot = Quaternion.LookRotation(cam.transform.position - hit.Pose.position);
        gameBoard.transform.rotation = Quaternion.Euler(cam.transform.position.x, rot.eulerAngles.y, cam.transform.position.z);

        //기타 UI 조정
        switch (GameManager.Instance.modeID)
        {
            case 0:     //Create Mode
                gridSizePanel.SetActive(true);
                soundMgr.bGM.clip = createSound;
                soundMgr.bGM.Play();
                break;
            case 1:
                Debug.LogError($"TouchManager ::: modeID = {GameManager.Instance.modeID}");
                break;
            case 2:     //혼자하기 모드 - 유형1
                playButtons_mode01.SetActive(true);
                playButtons.SetActive(false);
                break;
            case 3:     //혼자하기 모드 - 유형2
            case 4:     //혼자하기 모드 - 유형3
                playButtons.SetActive(true);
                cardBoardSetting.isCardBoardOn = true;
                cardButton.SetActive(true);
                break;
        }

        pointImage.SetActive(true);
        cubeSetting.enabled = true;
        blockImg.SetActive(false);
    }

    public void SetOrigin()
    {
        gameBoard.SetActive(false);
        gameBoard.transform.position = Vector3.zero;

        pointImage.SetActive(false);
        cubeSetting.enabled = false;

        if(gridSizePanel != null)
        {
            gridSizePanel.SetActive(false);
        }

        count = 0;
    }
    public void OffGameGuidePanel()
    {
        notePanel.SetActive(false);
        masterMapCreateHelp.SetActive(true);
    }
}
