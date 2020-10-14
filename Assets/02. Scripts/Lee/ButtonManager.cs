using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonManager : MonoBehaviourPun
{
    //public static ButtonManager instance;
    //프로필 
    public Text nickName_Text;

    //Game Board 리셋
    public TouchManager touchManager;
    public Slider boardSizeSlider;
    public Slider gridSizeSlider;

    public GameObject gridSizePanel;
    public GameObject blockImage;

    public GameObject profilePanel;

    //게임 중 옵션 팝업창
    public GameObject gameOption;
    public GameObject blackBG;

    //문제 카드
    public RectTransform cardBoard;
    private Vector2 startPos;
    private Vector2 endPos;
    private Vector2 destination;
    public float lerpSpeed = 5.0f;

    //Play Button - 큐브 생성 / 삭제 / 리셋
    public GameObject guideCube;
    public GameObject cubePrefab;
    public CubeSetting cubeSetting;
    public GameObject gameBoard;
    public GameObject cubeList;
    public List<GameObject> list;

    //정답 확인
    public QuestManager questManager;
    public AnswerManager answerManager;

    public InputField inputField;
    public GameObject playButtons;

    public CardBoardSetting cardBoardSetting;
    public CheckBoardMgr checkBoardMgr;
    public List<int>[] playerAnswerArray = new List<int>[3];

    //스크린샷
    public ScreenShot screenShot;

    //같이하기
    public GameObject mulCubeFac;
    private int cubeNum;

    private bool _isCorrect;

    private void Awake()
    {
       // instance = this;
    }

    private void Start()
    {
        if (nickName_Text != null)
        {
            nickName_Text.text = Palyfab_Login.myPlayfabInfo;
        }
        PhotonNetwork.AutomaticallySyncScene = true;
    }

    #region 혼자하기 모드
    //Game Board 리셋
    public void ResetGameBoard()
    {
        ResetCube();
        cubeSetting.GuideCubeOff();
        touchManager.SetOrigin();
        boardSizeSlider.value = 0.1f;
        gridSizeSlider.value = gridSizeSlider.minValue;
        blockImage.SetActive(true);
    }

    public void Multiy_ResetGameBoard()
    {
        ResetCube();
        cubeSetting.GuideCubeOff();
        touchManager.SetOrigin();
        boardSizeSlider.value = 0.1f;
        gridSizeSlider.value = gridSizeSlider.minValue;
        blockImage.SetActive(true);
        Destroy(touchManager.mapObj);
    }

    //Grid Size 확정
    public void SetGridSize()
    {
        gridSizePanel.gameObject.SetActive(false);
        blockImage.SetActive(false);
    }

    //게임 중 옵션 팝업창
    public void ConvertGameOption()
    {
        Debug.Log("ButtonManager ::: ConvertGameOption");
        gameOption.SetActive(!gameOption.activeSelf);
        blackBG.SetActive(gameOption.activeSelf);
    }

    //프로필 팝업창
    public void ConvertProfile()
    {
        profilePanel.SetActive(!profilePanel.activeSelf);
        blackBG.SetActive(profilePanel.activeSelf);
    }

    //재도전
    public void RetryGame()
    {
        ResetGameBoard();
        gameOption.SetActive(false);
        blackBG.SetActive(false);
    }

    //문제 카드 확인하기
    public void ConvertCardBoard()
    {
        cardBoardSetting.ShowCard();
    }

    //Cube 생성
    public void MakeCube()
    {
        if (guideCube.activeSelf && GameManager.Instance.modeID != 5)
        {
            GameObject cube = Instantiate(cubePrefab
                                         , cubeSetting.guideCube.transform.position
                                         , gameBoard.transform.rotation
                                         , cubeList.transform);
            list.Add(cube);

            Debug.Log("ButtonManager MakeCube() ::: 큐브 생성");
        }
    }

    //Cube 삭제
    public void DeleteCube()
    {
        if (GameManager.Instance.modeID != 5 && cubeSetting.currCube != null)
        {
            if (GameManager.Instance.modeID == 3)
            {
                cubeSetting.currCube.SetActive(false);
            }
            else
            {
                Destroy(cubeSetting.currCube);
                Debug.Log("ButtonManager ::: 큐브 삭제");
            }
        }
    }

    //Cube 리셋
    public void ResetCube()
    {
        if (GameManager.Instance.modeID != 5 && list.Count > 0)
        {
            for (int i = 0; i < list.Count; i++)
            {
                Destroy(list[i].gameObject);
            }
            list.Clear();
            Debug.Log("ButtonManager ::: 큐브 리셋");
        }

        if (GameManager.Instance.modeID == 3)
        {
            for (int i = 0; i < 27; i++)
            {
                GameObject obj = questManager.currStage.transform.GetChild(i).gameObject;
                obj.SetActive(true);
            }
        }
    }

    //Create Mode 전용 - 다운로드
    public void DownLoadCube()
    {
        Debug.Log("ButtonManager ::: CreateMode 저장하기");
    }


    //정답 확인 // // Rpc 로 패널 넘기기 

    public void CheckAnswer()
    {
        if (cardBoardSetting.isCardBoardOn)
        {
            cardBoardSetting.isCardBoardOn = false;
        }

        int modeID = GameManager.Instance.modeID;

        switch (modeID)
        {
            //정답 확인 ::: 혼자하기 유형 1
            case 2:
                if (string.IsNullOrEmpty(inputField.text) == false)
                {
                    int playerAnswer = int.Parse(inputField.text);
                    answerManager.CompareAnswer_Int(playerAnswer);
                }
                break;
            //정답 확인 ::: 혼자하기 유형 2 / 유형 3
            case 3:
                Debug.Log($"ButtonManager ::: \n {modeID} 정답 체크하겠습니다.");

                playerAnswerArray = checkBoardMgr.MakePlayerAnswerArray();
                answerManager.CompareAnswer_Array(playerAnswerArray);
                break;
            case 4:
                if (list.Count != 0)
                {
                    Debug.Log($"ButtonManager ::: \n {modeID} 정답 체크하겠습니다.");
                    playerAnswerArray = checkBoardMgr.MakePlayerAnswerArray();
                    answerManager.CompareAnswer_Array(playerAnswerArray);
                }
                break;
            case 5:
                Debug.Log($"ButtonManager ::: \n {modeID}");
                break;

            case 6:
            case 7:
            case 8:
                if (PhotonNetwork.IsMasterClient)
                {
                    Debug.Log($"방장 정답체크 버튼 누름");
                    photonView.RPC("RpcCheckAnswerTogether", RpcTarget.AllBuffered, modeID);
                }
                break;
        }
    }

    //같이하기 정답체크
    [PunRPC]
    public void RpcCheckAnswerTogether(int modeID)
    {
        Debug.Log($"ButtonManager ::: \n {modeID} 정답 체크하겠습니다.");

        gridSizeSlider.value = modeID - 3;
        playerAnswerArray = checkBoardMgr.MakePlayerAnswerArray();
        answerManager.CompareAnswer_Array(playerAnswerArray);
    }

    [PunRPC]
    public void RpcSendAnswerManagerInfo(bool isCorrect, bool isChecked)
    {
        answerManager.SendAnswerManagerInfo(isCorrect, isChecked);
        answerManager.blurredImage.SetActive(isChecked);
        cardBoardSetting.isCardBoardOn = false;
    }

    //다음 단계로
    public void NextLevel()
    {
        Debug.Log("다음 단계로 버튼 클릭");

        //GameBoard 초기화
        RetryAloneMode();

        //다음 문제 내기
        if (GameManager.Instance.stageID < GameManager.Instance.stageStateList.Count)
        {
            questManager.ChangeQuset(GameManager.Instance.stageID);
            cardBoardSetting.ChangeCard();

        }
        else
        {
            Debug.Log($"Stage 0{GameManager.Instance.stageID} ::: 축하합니다. \n {GameManager.Instance.stageStateList.Count} 개의 스테이지를 모두 클리어했습니다.");

            GameManager.Instance.stageID = 0;
            //changeScene.AloneStageSelect();
        }
    }

    public void RetryAloneMode()
    {
        Debug.Log("다시하기 버튼 클릭");

        answerManager.isChecked = false;
        answerManager.blurredImage.SetActive(false);

        if (GameManager.Instance.modeID == 2)
        {
            inputField.text = null;
        }
        else if (GameManager.Instance.modeID == 3 || GameManager.Instance.modeID == 4)
        {
            ResetCube();
        }
    }

    //혼자하기 모드 - 이어하기
    public void ContinueGame()
    {
        Debug.Log("이어하기 버튼 클릭");

        answerManager.isChecked = false;
        bool isChecked = answerManager.isChecked;

        Debug.Log($"ButtonManager ::: {answerManager.isChecked} // {isChecked}");

        answerManager.blurredImage.SetActive(false);
    }

    //스크린샷
    public void ScreenShot()
    {
        screenShot.Capture_Button();
    }

    #endregion 

    #region 같이하기 모드 - 마스터만 누를 수 있음


    // 같이하기 Cube 생성
    public void Photon_MakeCube()
    {
        if (guideCube.activeSelf)
        {
            // 큐브셋팅에 hitobj.GetChild(0) 가져와
            GameObject hitObj = cubeSetting.hitObj;
            Debug.Log("히트 된 오브젝트 이름 : " + hitObj.name);
            Transform cubePos = hitObj.transform.GetChild(0).transform;

            cubeNum++;

            GameObject cube = Instantiate(mulCubeFac, cubePos.position, gameBoard.transform.rotation, cubeList.transform);
            list.Add(cube);

            cube.name = PhotonNetwork.NickName + "Cube(" + cubeNum + ")";
            Debug.Log("생성한  큐브 이름 확인 : " + cube.name);
            Debug.Log(cubePos.position);

            //다른애들도 hitobj.GetChild(0) 찾고 거기에 큐브 생성하라고 명령해
            photonView.RPC("RpcMakeCube", RpcTarget.Others, hitObj.name, PhotonNetwork.NickName, cubeNum);
            print("클라이언트들에게 RpcMakeCube 보냄");
        }

        #region Mr.Gun
        /////////////////////////////////
        // 1. 그 자리에 큐브가 있는지 확인

        // 1-1. 큐브가 있다


        // 1-2. 큐브가 없다
        {
            // 1-2-2. 그 자리에 큐브를 만든다.
            //확인배열 채우기 +1
            //함수호출

        }

        //pos의 위치에 큐브 생성
        //확인배열[x][y][z]이 +1이면 return
        //확인배열[x][y][z]이 0이면 -1

        //remove
        //1. 그 위치의 확인배열이 1인지 확인
        //함수호출 rpcremove

        //rpcremove
        //1-1. 1이면 없애고 배열을 0, 너도 없애라
        #endregion
    }

    [PunRPC]
    public void RpcMakeCube(string hitObj, string otherName, int cubeNum)
    {
        GameObject obj = GameObject.Find(hitObj).gameObject;
        Transform cubePos = obj.transform.GetChild(0).transform;

        GameObject cube = Instantiate(mulCubeFac, cubePos.position, gameBoard.transform.rotation, cubeList.transform);
        list.Add(cube);
        //생성한 CUBE의 isMine을 체크하고 내꺼면 다른 애들 큐브이름을 바꿔

        cube.name = otherName + "Cube(" + cubeNum + ")";
        Debug.Log("생성한  큐브 이름 확인 : " + cube.name);
        Debug.Log(cubePos.position);
    }

    //같이하기 모드 - 큐브 삭제
    public void Photon_DeleteCube()
    {
        if (cubeSetting.currCube != null)
        {
            Destroy(cubeSetting.currCube);
            photonView.RPC("RpcDeleteCube", RpcTarget.Others, cubeSetting.currCube.name);

            Debug.Log("ButtonManager ::: 큐브 삭제");

        }
    }

    [PunRPC]
    public void RpcDeleteCube(string currCubeName)
    {
        GameObject currCube = GameObject.Find(currCubeName).gameObject;
        Destroy(currCube);
    }

    //같이하기 모드 - 다음 단계로
    public void NextLevel_Together()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            //쌓여있는 큐브 리셋하기
            Photon_ResetCube();

            bool isSameID = true;
            int randStageID = 1000;

            while (isSameID)
            {
                randStageID = Random.Range(1, 10);

                if (randStageID != GameManager.Instance.stageID)
                {
                    isSameID = false;
                }
            }
            Debug.Log($"ButtonManager ::: \n GameManager.Instance.stageID // randStageID ::: {GameManager.Instance.stageID} // {randStageID}");

            photonView.RPC("RpcChangeQuest_Card", RpcTarget.AllBuffered, randStageID);
        }
    }

    [PunRPC]
    public void RpcChangeQuest_Card(int randStageID)
    {
        questManager.ChangeQuset(randStageID);
        cardBoardSetting.ChangeCard();
        answerManager.isChecked = false;
        cardBoardSetting.isCardBoardOn = false;
        answerManager.blurredImage.SetActive(false);
    }

    //같이하기 모드 - 다시하기
    public void Rertry_Together()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            Debug.Log($"Rertry_Together() ::: 여기도 디버그하나 찍자");
            Photon_ResetCube();

            answerManager.isChecked = false;
            bool isChecked = answerManager.isChecked;

            photonView.RPC("RpcSendAnswerManagerInfo", RpcTarget.Others, _isCorrect, isChecked);
            answerManager.blurredImage.SetActive(false);
        }
    }

    public void Photon_ResetCube()
    {
        if (PhotonNetwork.IsMasterClient)
        { 
            print("Photon_ResetCube클릭");
            photonView.RPC("RpcResetCube", RpcTarget.AllBuffered, (int)list.Count);
        }
    }

    [PunRPC]
    public void RpcResetCube(int listcount)
    {
        for (int i = 0; i < listcount; i++)
        {
            Destroy(list[i].gameObject);
        }
        list.Clear();
        Debug.Log("ButtonManager ::: 큐브 리셋");
    }

    //같이하기 모드 - 이어하기
    public void ContinueGame_Together()
    {
        //같이하기 모드에서 이어하기
        //클라이언트들에게 방장이 이어하기 버튼을 눌렀다는 사실을 전달
        if (PhotonNetwork.IsMasterClient)
        {
            Debug.Log("이어하기 버튼 클릭");

            answerManager.isChecked = false;
            bool isChecked = answerManager.isChecked;

            Debug.Log($"ButtonManager(ContinueGame_Together) ::: {answerManager.isChecked} // {isChecked}");
            Debug.Log("ButtonManager ::: 방장님 이어하기 버튼 클릭");

            photonView.RPC("RpcSendAnswerManagerInfo", RpcTarget.Others, _isCorrect, isChecked);

            answerManager.blurredImage.SetActive(false);
        }
    }

    //같이하기 모드 - 대기화면으로 
    public void LeftRoom_Together()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            PhotonNetwork.LoadLevel("12. TogetherModeWait");
        }
    }
    #endregion
}
