using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class MultyScreenMgr : MonoBehaviourPun
{
    public TouchManager touchManager;
    public CloudAnchorController cloudAnchorController;
    public ButtonManager buttonManager;
    public RectTransform playButtons;
    public GameObject blackBG;
    public GameObject notePanel;
    public RectTransform startPos_PlayButtons;
    public RectTransform endPos_PlayButtons;
    public float lerpSpeed = 3.0f;
    public bool isNotePanelOff;
    public int mapOnCount = 0;
    public PhotonView myPhotonView;
    public int maxplayerNum;
    public GameObject hr_Background;

    void Start()
    {
        myPhotonView = GetComponent<PhotonView>();
        Invoke("OffNotePanel", 3);
        maxplayerNum = 2;
        hr_Background.SetActive(true);
    }

    public void OffNotePanel()
    {
        blackBG.SetActive(false);
        notePanel.SetActive(false);
        isNotePanelOff = true;
    }

    void Update()
    {
        // 플레이어들이 맵을 생성하면
        if(cloudAnchorController.isResolvingFinish == true || cloudAnchorController.isHostingFinish == true)
        {
            playButtons.anchoredPosition = Vector2.Lerp(playButtons.anchoredPosition, endPos_PlayButtons.anchoredPosition,Time.deltaTime*lerpSpeed);
            
            //if (PhotonNetwork.IsMasterClient)
            //{
            //    print("다른 플레이어가 맵을 생성할 동안 잠시만 기다려 주세요");
            //}
            //if (!PhotonNetwork.IsMasterClient)
            //{
            //    print("자 준비하세요 곧 시작합니다.");
            //    Invoke("GameStart", 3);
            //}
        }

        if(mapOnCount == 1)
        {
            Debug.Log($"MultyScreenMgr :: mapcount == 1 :: RpcGameStart 실행좀");
            photonView.RPC("RpcGameStart", RpcTarget.AllBuffered);
            mapOnCount = maxplayerNum;
        }
    }

    [PunRPC]
    public void RpcMapOnCountUp()
    {
        print("RpcMapOnCountUp() :: mapOnCount 올림");

        mapOnCount += 1;
        print(mapOnCount);
    }

    [PunRPC]
    public void RpcGameStart()
    {
        Debug.Log($"RpcGameStart() 들어왔어");
        hr_Background.SetActive(false);
        Debug.Log($"blackBG 껐어");

    }
}
