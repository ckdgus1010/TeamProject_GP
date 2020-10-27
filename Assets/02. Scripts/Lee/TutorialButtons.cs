using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialButtons : MonoBehaviour
{
    public CubeSetting cubeSetting;
    public TutorialManager tutorialManager;
    public PlayHelperPopup playHelpPopup;
    public TutorialAnswerManager answerManager;

    private int makeCount = 0;
    private int deleteCount = 0;
    private int resetCount = 0;
    private int cardCount = 0;

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

    public void CheckAnswer()
    {
        if (list.Count != 0)
        {
            if (cardBoardSetting.isCardBoardOn)
            {
                cardBoardSetting.isCardBoardOn = false;
            }
            playerAnswerArray = checkBoardMgr.MakePlayerAnswerArray();
            answerManager.CheckAnswer(playerAnswerArray);
        }
    }

    public void ScreenShot()
    {
        Debug.Log("TutorialButtons ::: 스크린샷 버튼 누름");
        screenshot.Capture_Button();
    }

    public void ResetGameboard()
    {
        tutorialManager.SetOrigin();

        ResetCube();
        playButtons.SetActive(false);

        gameboard.SetActive(false);
    }

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

    void ChangeText()
    {
        playHelpPopup.ChangeHelpMessageText();
    }
}
