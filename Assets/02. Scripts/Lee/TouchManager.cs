using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleARCore;
using GoogleARCore.CrossPlatform;
using Photon.Pun;


//목표: 사용자가 touch한 지점에 GameBoard 생성

public class TouchManager : MonoBehaviour
{
    public Camera cam;
    public CardBoardSetting cardBoardSetting;

    public GameObject gameBoard;

    public GameObject pointImage;
    public CubeSetting cubeSetting;
    public GameObject gridSizePanel;
    public GameObject boardSizePanel;
    public GameObject blockImg;
    public GameObject cardButton;
    public GameObject playButtons;
    public GameObject checkingButton;
    public GameObject inputField;

    //Touch 횟수
    [HideInInspector]
    public int count = 0;

    //GameBoard 위치 보정
    public float height = 0.1f;
    public float depth = 0.5f;

    // 같이하기 
    public GameObject beachMap;
    private GameObject mapObj;
    AsyncTask<CloudAnchorResult> result_AsyncTask;
    public PhotonView myPhotonView;
    private Anchor anchor;
    public GameObject backGround;
    public GameObject mapMgr;
    private GameMap gameMap;
    private int hostingCount =0 ;
    public GameObject hostBt;
    public GameObject resolveBt;

    void Start()
    {
        count = 0;

        //같이하기 모드인 경우
        if(GameManager.Instance.modeID == 5)
        {
            gameMap = mapMgr.GetComponent<GameMap>();
            myPhotonView = GameObject.Find("Player(Clone)").GetComponent<PhotonView>();

            if (PhotonNetwork.IsMasterClient)
            {
                hostBt.SetActive(true);
            }
            else
            {
                resolveBt.SetActive(true);
            }
        }
    }

    void Update()
    {
        if (count == 1 || Input.touchCount == 0)
        {
            return;
        }

        Touch touch = Input.GetTouch(0);
        TrackableHitFlags raycastFilter = TrackableHitFlags.PlaneWithinPolygon
                                          | TrackableHitFlags.FeaturePointWithSurfaceNormal;

        if (count == 0 && touch.phase == TouchPhase.Began && Frame.Raycast(touch.position.x, touch.position.y, raycastFilter, out TrackableHit hit))
        {
            anchor = hit.Trackable.CreateAnchor(hit.Pose);

            //같이하기 모드인 경우
            if (GameManager.Instance.modeID == 5 && PhotonNetwork.IsMasterClient)
            {
                //GameBoard 생성
                gameBoard.SetActive(true);

                //GameBoard 위치 설정
                gameBoard.transform.position = anchor.transform.position;
                var rot = Quaternion.LookRotation(cam.transform.position - hit.Pose.position);
                gameBoard.transform.rotation = Quaternion.Euler(cam.transform.position.x, rot.eulerAngles.y, cam.transform.position.z);
                
                //GameBoard 생성
                mapObj = Instantiate(gameMap.map, backGround.transform.position, Quaternion.Euler(cam.transform.position.x, rot.eulerAngles.y, cam.transform.position.z));
                mapObj.transform.SetParent(backGround.transform);
                Debug.Log("앵커 만들어짐 : " + mapObj.transform.position);

                pointImage.SetActive(true);
                cubeSetting.enabled = true;
                boardSizePanel.SetActive(true);
            }
            //Create Mode 또는 혼자하기 모드인 경우
            else if(GameManager.Instance.modeID != 5)
            {
                SoloPlay(hit, anchor);
            }

            count = 1;
        }
    }
    public void OnClickHost_ResoleButton()
    {
        print("호스팅버튼");
        if (GameManager.Instance.modeID == 5 && PhotonNetwork.IsMasterClient && hostingCount ==0)
        {
            print("호스트클라우드앵커 코루틴 실행");
            StartCoroutine(HostCloudAnchor(anchor));//코루틴 실행ㅡ
            hostingCount = 1;
        }
        if (GameManager.Instance.modeID == 5 && !PhotonNetwork.IsMasterClient)
        {
            print("리졸브클라우드앵커 코루틴 실행");
            print(Player.cloudID);
            StartCoroutine(ResolveCloudAnchor(Player.cloudID));//코루틴 실행ㅡ
            print("ID 받았어요");

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
        Debug.Log(cloudId);
        myPhotonView.RPC("SendCloudInfo", RpcTarget.AllBuffered, cloudId);
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


        mapObj = Instantiate(gameMap.map, result_AsyncTask.Result.Anchor.transform.position, Quaternion.identity);
        mapObj.transform.SetParent(result_AsyncTask.Result.Anchor.transform);
        pointImage.SetActive(true);
        cubeSetting.enabled = true;
        boardSizePanel.SetActive(true);
    }
    
    public void SoloPlay(TrackableHit hit, Anchor anchor)
    {
        //GameBoard 생성
        gameBoard.SetActive(true);

        //GameBoard 위치 설정
        gameBoard.transform.position = anchor.transform.position;

        //GameBoard 방향 설정
        //gameBoard.transform.rotation = Quaternion.Euler(Vector3.forward);

        var rot = Quaternion.LookRotation(cam.transform.position - hit.Pose.position);
        gameBoard.transform.rotation = Quaternion.Euler(cam.transform.position.x, rot.eulerAngles.y, cam.transform.position.z);

        //기타 UI 조정
        switch(GameManager.Instance.modeID)
        {
            case 0:     //Create Mode
                gridSizePanel.SetActive(true);
                break;
            case 1:
                Debug.LogError($"TouchManager ::: modeID = {GameManager.Instance.modeID}");
                break;
            case 2:     //혼자하기 모드 - 유형1
                inputField.SetActive(true);
                playButtons.SetActive(false);
                break;
            case 3:     //혼자하기 모드 - 유형2
            case 4:     //혼자하기 모드 - 유형3
                playButtons.SetActive(true);
                cardBoardSetting.isCardBoardOn = true;
                break;
        }

        pointImage.SetActive(true);
        cubeSetting.enabled = true;
        boardSizePanel.SetActive(true);
        cardButton.SetActive(true);
        checkingButton.SetActive(true);
    }
    public void SetOrigin()
    {
        gameBoard.SetActive(false);
        gameBoard.transform.position = Vector3.zero;

        pointImage.SetActive(false);
        cubeSetting.enabled = false;
        gridSizePanel.SetActive(false);
        boardSizePanel.SetActive(false);

        count = 0;
    }
}
