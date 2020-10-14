using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class MultyScreenMgr : MonoBehaviourPun
{
    public TouchManager touchManager;
    public CloudAnchorController cloudAnchorController;
    public ButtonManager buttonManager;
    public PhotonView myPhotonView;

    public RectTransform master_PlayButtons;
    public RectTransform client_PlayButtons;
    public RectTransform startPos_PlayButtons;
    public RectTransform endPos_PlayButtons;

    public GameObject blackBG;
    public GameObject notePanel;
    public GameObject hr_Background;
    public GameObject waitingClientPopup;
    public GameObject waitingMasterPopup;
    public GameObject masterMapCreateHelp;

    public int mapOnCount = 0;
    public float lerpSpeed = 3.0f;
    public bool isNotePanelOff;

    public int maxplayerNum;

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
        if (PhotonNetwork.IsMasterClient)
        {
            //바닥을 충분히 인식했다면,원하는 위치를 눌러 맵을 생성하세요.
            masterMapCreateHelp.SetActive(true);
        }
        else
        {
            //방장이 맵을 생성할 때까지 잠시만 기다려주세요! 켜기
            waitingMasterPopup.SetActive(true);
        }
    }

    void Update()
    {
        // 플레이어들이 맵을 생성하면
        if(cloudAnchorController.isResolvingFinish == true || cloudAnchorController.isHostingFinish == true)
        {
            if (PhotonNetwork.IsMasterClient)
            {
                master_PlayButtons.anchoredPosition = Vector2.Lerp(master_PlayButtons.anchoredPosition, endPos_PlayButtons.anchoredPosition,Time.deltaTime*lerpSpeed);
            }
            if (!PhotonNetwork.IsMasterClient)
            {
                client_PlayButtons.anchoredPosition = Vector2.Lerp(client_PlayButtons.anchoredPosition, endPos_PlayButtons.anchoredPosition, Time.deltaTime * lerpSpeed);
            }
        }

        if (mapOnCount == 1)
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
        if (PhotonNetwork.IsMasterClient)
        {
            //다른 플레이어가 맵을 생성할 때까지 잠시만 기다려주세요! 끄기
            waitingClientPopup.SetActive(false);
        }

        Debug.Log($"blackBG 껐어");

    }
}
