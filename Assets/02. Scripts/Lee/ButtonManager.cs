using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonManager : MonoBehaviourPun
{
    public static ButtonManager instance;
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

    private void Awake()
    {
        instance = this;
    }
    private void Start()
    {
        if (nickName_Text != null)
        {
            nickName_Text.text = Palyfab_Login.myPlayfabInfo;
        }
    }

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
    public void Photon_MakeCube()
    {
        if (guideCube.activeSelf)
        {
            // 큐브셋팅에 hitobj.GetChild(0) 가져와
            GameObject hitObj = cubeSetting.hitObj;
            Debug.Log("난 마스터 : " + hitObj.name);
            Transform cubePos = hitObj.transform.GetChild(0).transform;


            //다른애들도 hitobj.GetChild(0) 찾고 거기에 큐브 생성하라고 명령해
            photonView.RPC("RpcMakeCube", RpcTarget.AllBuffered, hitObj.name);
            print("클라이언트들에게 RpcMakeCube 보냄");

        }
        // 거기에다 생성해
        //Instantiate(mulCubeFac, cubePos.position, gameBoard.transform.rotation, cubeList.transform);
        //print("큐브 위치 보여줘 : " + cubePos.position);

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
    public void RpcMakeCube(string hitObj)
    {
        Debug.Log("난 클라이언트 : " + hitObj);
        
        GameObject obj = GameObject.Find(hitObj).gameObject;
        Transform cubePos = obj.transform.GetChild(0).transform;

        int cubeNum = GameManager.Instance.cubeNum++;
      
        GameObject cube = Instantiate(mulCubeFac, cubePos.position, gameBoard.transform.rotation, cubeList.transform);

        //생성한 CUBE의 isMine을 체크하고 내꺼면 다른 애들 큐브이름을 바꿔
        cube.name = "Cube(" + cubeNum + ")";
        Debug.Log( "생성한 큐브 이름 확인 : " + cube.name);
        Debug.Log(cubePos.position);

       
    }

   
    //Cube 삭제
    public void DeleteCube()
    {
        if (GameManager.Instance.modeID == 5 && cubeSetting.currCube != null)
        {
            WatingButtonMgr.instance.myPhotonView.RPC("RpcResetCube", RpcTarget.AllBuffered, cubeSetting.currCube);
            Debug.Log("ButtonManager ::: 큐브 삭제");
        }

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
    public void Photon_DeleteCube(GameObject currCube)
    {

        Destroy(currCube);
        Debug.Log("ButtonManager ::: 큐브 삭제");

    }
    //Create Mode 전용 - 다운로드
    public void DownLoadCube()
    {
        Debug.Log("ButtonManager ::: CreateMode 저장하기");
    }

    //Cube 리셋
    public void ResetCube()
    {
        //같이하기 지우기 
        if (GameManager.Instance.modeID == 5 && list.Count > 0)
        {
            for (int i = 0; i < list.Count; i++)
            {
                WatingButtonMgr.instance.myPhotonView.RPC("RpcResetCube", RpcTarget.AllBuffered, list[i]);
            }
        }

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
    public void Photon_ResetCube(int i)
    {
        Destroy(list[i].gameObject);

        list.Clear();
        Debug.Log("ButtonManager ::: 큐브 리셋");
    }


    //정답 확인
    public void CheckAnswer()
    {
        if (cardBoardSetting.isCardBoardOn)
        {
            cardBoardSetting.isCardBoardOn = false;
        }

        int modeID = GameManager.Instance.modeID;

        //정답 확인 ::: 혼자하기 유형 1
        if (modeID == 2 && string.IsNullOrEmpty(inputField.text) == false)
        {
            int playerAnswer = int.Parse(inputField.text);
            answerManager.CompareAnswer_Int(playerAnswer);
        }
        //정답 확인 ::: 혼자하기 유형 2 / 유형 3
        else if (modeID == 3)
        {
            Debug.Log($"ButtonManager ::: \n {modeID} 정답 체크하겠습니다.");

            playerAnswerArray = checkBoardMgr.MakePlayerAnswerArray();
            answerManager.CompareAnswer_Array(playerAnswerArray);
        }
        else if (modeID == 4 && list.Count != 0)
        {
            Debug.Log($"ButtonManager ::: \n {modeID} 정답 체크하겠습니다.");
            playerAnswerArray = checkBoardMgr.MakePlayerAnswerArray();
            answerManager.CompareAnswer_Array(playerAnswerArray);
        }
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
            questManager.ChangeQuset();
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

    public void ContinueGame()
    {
        Debug.Log("이어하기 버튼 클릭");

        answerManager.isChecked = false;
        answerManager.blurredImage.SetActive(false);
    }

    public void ScreenShot()
    {
        screenShot.Capture_Button();
    }
}
