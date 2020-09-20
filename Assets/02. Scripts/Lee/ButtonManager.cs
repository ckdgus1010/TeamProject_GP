using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonManager : MonoBehaviour
{
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

    private void Start()
    {
        if(GameManager.Instance.stageID == 0)
        {
            return;
        }

        startPos = cardBoard.position;
        endPos = startPos + new Vector2(1450, 0);

        if (GameManager.Instance.modeID == 2 && GameManager.Instance.stageID != 0)
        {
            inputField.transform.gameObject.SetActive(true);
            playButtons.SetActive(false);
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
        if (guideCube.activeSelf)
        {
            GameObject cube = Instantiate(cubePrefab
                                         , cubeSetting.guideCube.transform.position
                                         , gameBoard.transform.rotation
                                         , cubeList.transform);
            list.Add(cube);

            Debug.Log("ButtonManager ::: 큐브 생성");
        }
    }

    //Cube 리셋
    public void ResetCube()
    {
        if (list.Count > 0)
        {
            for (int i = 0; i < list.Count; i++)
            {
                Destroy(list[i].gameObject);
            }
            list.Clear();
            Debug.Log("ButtonManager ::: 큐브 리셋");
        }
    }

    //Cube 삭제
    public void DeleteCube()
    {
        if (cubeSetting.currCube != null)
        {
            Destroy(cubeSetting.currCube);
            Debug.Log("ButtonManager ::: 큐브 삭제");
        }
    }

    //Create Mode 전용 - 다운로드
    public void DownLoadCube()
    {
        Debug.Log("ButtonManager ::: CreateMode 저장하기");
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
        else if (modeID != 2 && list.Count != 0)
        {
            Debug.Log($"ButtonManager ::: \n {modeID} 정답 체크하겠습니다.");

            //playerAnswerArray = checkBoardMgr.MakePlayerAnswerArray();
            //answerManager.CompareAnswer_Array(playerAnswerArray);
        }
    }

    //다음 단계로
    public void NextLevel()
    {
        Debug.Log("다음 단계로 버튼 클릭");

        //GameBoard 초기화
        RetryGame();

        //다음 문제 내기
        if (GameManager.Instance.stageID < GameManager.Instance.stageStateList.Count)
        {
            questManager.ChangeQuset();
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
}
