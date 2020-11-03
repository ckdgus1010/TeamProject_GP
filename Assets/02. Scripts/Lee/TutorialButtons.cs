using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TutorialButtons : MonoBehaviour
{
    private int makeCount = 0;
    private int deleteCount = 0;
    private int resetCount = 0;
    private int cardCount = 0;

    [Header("AR Camera")]
    public CubeSetting cubeSetting;
    public TutorialTouchManager tutorialTouchManager;
    public PlayHelperPopup playHelpPopup;
    public AnswerManager answerManager;

    [Header("Canvas")]
    [SerializeField] private GameObject beforeTutorialCanvas;
    [SerializeField] private GameObject tutorialCanvas;
    [SerializeField] private GameObject afterTutorialCanvas;

    [Header("Camera Setting")]
    [SerializeField] private GameObject aRCam;
    [SerializeField] private GameObject mainCam;

    [Header("생성할 cube와 관련된 정보")]
    [SerializeField] private GameObject cubePrefab;
    [SerializeField] private GameObject gameboard;
    [SerializeField] private GameObject guideCube;
    [SerializeField] private GameObject cubeList;
    [SerializeField] private List<GameObject> list;

    [Header("버튼 UI")]
    [SerializeField] private ScreenShot screenshot;
    [SerializeField] private GameObject playButtons;

    [Header("카드 보드")]
    [SerializeField] private CardBoardSetting cardBoardSetting;
    public CheckBoardMgr checkBoardMgr;
    public List<int>[] playerAnswerArray = new List<int>[3];

    private void Start()
    {
        makeCount = 0;
        deleteCount = 0;
        resetCount = 0;
        cardCount = 0;
    }

    // 안내 팝업 이후 튜토리얼 시작
    public void StartTutorial()
    {
        tutorialCanvas.SetActive(true);
        aRCam.SetActive(true);
        mainCam.SetActive(false);
        beforeTutorialCanvas.SetActive(false);
    }

    // 튜토리얼 후 안내 팝업
    public void FinishTutorial()
    {
        afterTutorialCanvas.SetActive(true);
        aRCam.SetActive(false);
        mainCam.SetActive(true);
        tutorialCanvas.SetActive(false);
    }

    public void ExitTutorial()
    {
        SceneManager.LoadScene("01. Intro");
    }

    // 큐브 쌓기
    public void MakeCube()
    {
        if (guideCube.activeSelf)
        {
            GameObject cube = Instantiate(cubePrefab, guideCube.transform.position, gameboard.transform.rotation, cubeList.transform);

            list.Add(cube);

            makeCount += 1;

            if (makeCount == 3)
            {
                Debug.Log($"TutorialButtons ::: makeCount = {makeCount}");
                playHelpPopup.ChangeHelpMessageText();
            }
        }
    }

    // 큐브 빼기
    public void DeleteCube()
    {
        if (cubeSetting.currCube != null)
        {
            Destroy(cubeSetting.currCube);
            deleteCount += 1;

            if (deleteCount == 1)
            {
                Debug.Log($"TutorialButtons ::: deleteCount = {deleteCount}");
                playHelpPopup.ChangeHelpMessageText();
            }
        }
    }

    // 큐브 리셋
    public void ResetCube()
    {
        if (list.Count > 0)
        {
            for (int i = 0; i < list.Count; i++)
            {
                Destroy(list[i]);
            }
            list.Clear();
        }

        resetCount += 1;

        if (resetCount == 1)
        {
            Debug.Log($"TutorialButtons ::: resetCount = {resetCount}");
            playHelpPopup.ChangeHelpMessageText();
        }
    }

    // 정답 확인
    public void CheckAnswer()
    {
        if (list.Count != 0)
        {
            if (cardBoardSetting.isCardBoardOn)
            {
                cardBoardSetting.isCardBoardOn = false;
            }
            playerAnswerArray = checkBoardMgr.MakePlayerAnswerArray();
            answerManager.CompareAnswer_Array(playerAnswerArray);
        }
    }

    // 스크린샷
    public void ScreenShot()
    {
        Debug.Log("TutorialButtons ::: 스크린샷 버튼 누름");
        screenshot.Capture_Button();
    }

    // 맵 삭제
    public void ResetGameboard()
    {
        tutorialTouchManager.SetOrigin();

        ResetCube();
        playButtons.SetActive(false);

        gameboard.SetActive(false);
    }

    // 카드 보기 / 숨기기
    public void ConvertCardBoard()
    {
        cardBoardSetting.isCardBoardOn = !cardBoardSetting.isCardBoardOn;

        cardCount += 1;

        if (cardCount == 1)
        {
            Debug.Log($"TutorialButtons ::: cardCount = {cardCount}");
            playHelpPopup.ChangeHelpMessageText();

            Invoke("ChangeText", 10.0f);
        }
    }

    // 다음 안내 보기
    void ChangeText()
    {
        playHelpPopup.ChangeHelpMessageText();
    }

    // 다시하기
    public void RetryTutorialGame()
    {
        ResetCube();
        answerManager.isChecked = false;
    }

    // 이어하기
    public void ContinueTutorialGame()
    {
        answerManager.isChecked = false;
    }
}
