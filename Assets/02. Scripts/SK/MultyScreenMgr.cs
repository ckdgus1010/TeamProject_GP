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
    public GameObject front_Hidden;
    public GameObject side_Hidden;
    public GameObject up_Hidden;

    public int mapOnCount = 0;
    public float lerpSpeed = 3.0f;
    public bool isNotePanelOff;

    public int maxplayerNum;

    private List<string> fstList3 = new List<string>() { "앞", "옆", "위" };
    private List<string> fstList2 = new List<string>() { "앞", "옆" };
    public List<string> list;
    public string[] fstListlist;
    public string myRole;

    void Start()
    {
        myPhotonView = GetComponent<PhotonView>();
        //Invoke("OffNotePanel", 3);
        maxplayerNum = 2;
        hr_Background.SetActive(true);
        if (PhotonNetwork.IsMasterClient && PhotonNetwork.CurrentRoom.MaxPlayers == 3)
        {
            RandomNum3();
        }
        if (PhotonNetwork.IsMasterClient && PhotonNetwork.CurrentRoom.MaxPlayers == 2)
        {
            RandomNum2();
        }
    }
    //방장이 앞,옆,위 역할을 랜덤으로 만들어 리스트에 저장한다.
    public void RandomNum3()
    {
        for (int i = 0; i < 3; i++)
        {
            int rand = Random.Range(0, fstList3.Count);
            //print(fstList3[rand]);
            list.Add(fstList3[rand]);

            fstList3.RemoveAt(rand);
        }
        fstListlist = list.ToArray();
        //리스트를 모든 사람에게 뿌린다.
        photonView.RPC("GetNum3", RpcTarget.All, fstListlist);
    }
    public void RandomNum2()
    {
        for (int i = 0; i < 2; i++)
        {
            int rand = Random.Range(0, fstList2.Count);
            //print(fstList2[rand]);
            list.Add(fstList2[rand]);

            fstList2.RemoveAt(rand);
        }
        fstListlist = list.ToArray();
        photonView.RPC("GetNum2", RpcTarget.All, fstListlist);

    }

    // 내 인덱스번호와 맞는 리스트의 값을 내 역할에 넣는다.
    [PunRPC]
    public void GetNum3(string[] list)
    {
        fstListlist = list;
        myRole = fstListlist[WatingButtonMgr.instance.myIndexNumber];

        if (myRole == "앞")
        {
            print("나는 앞이야");
            front_Hidden.SetActive(false);
        }
        if (myRole == "옆")
        {
            print("나는 옆이야");
            side_Hidden.SetActive(false);
        }
        if (myRole == "위")
        {
            print("나는 위야");
            up_Hidden.SetActive(false);
        }

    }

    [PunRPC]

    public void GetNum2(string[] list)
    {
        fstListlist = list;
        myRole = fstListlist[WatingButtonMgr.instance.myIndexNumber];

        if (myRole == "앞")
        {
            print("나는 앞이야");
            front_Hidden.SetActive(false);
        }
        if (myRole == "옆")
        {
            print("나는 옆이야");
            side_Hidden.SetActive(false);
        }

        up_Hidden.SetActive(false);
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
        if (cloudAnchorController.isResolvingFinish == true || cloudAnchorController.isHostingFinish == true)
        {
            if (PhotonNetwork.IsMasterClient)
            {
                master_PlayButtons.anchoredPosition = Vector2.Lerp(master_PlayButtons.anchoredPosition, endPos_PlayButtons.anchoredPosition, Time.deltaTime * lerpSpeed);
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
