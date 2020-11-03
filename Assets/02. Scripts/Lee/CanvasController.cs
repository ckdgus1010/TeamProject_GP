using Lee;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.ExceptionServices;
using UnityEngine;
using UnityEngine.UI;

public class CanvasController : MonoBehaviour
{
    // 설정팝업
    public GameObject settingCan;

    // 스크립트 
    public ButtonSound buttonSound;

    [SerializeField] private ButtonManager01 buttonManager01;
    [SerializeField] private ModeTitle modetitle;
    public GameObject[] canvasArray = new GameObject[6];
    // 0: 인트로
    // 1: 로그인 화면
    // 2: 메인 메뉴 화면
    // 3: 플레이 모드 선택 화면
    // 4: 혼자하기 모드 유형 선택 화면
    // 5: 혼자하기 모드 스테이지 선택 화면
    // 6: 크레딧 영상
    public enum CanvasID { Intro, Login, MainMenu, PlayMode, AloneMode, AloneStage };
    private GameObject currCanvas = null;

    [Header("다른 Scene에서 Intro Scene으로 넘어올 때")]
    [SerializeField] private SwipeMenu swipeMenu;
    [SerializeField] private Scrollbar horizontalScrollbar;
    [SerializeField] private ModeData[] aloneMode = new ModeData[3];
    [SerializeField] private Toggle[] paginations = new Toggle[3];

    [Header("Credit Canvas")]
    [SerializeField] private GameObject creditExitButton;


    public void Start()
    {
        int modeID = GameManager.Instance.modeID;
        Debug.Log($"CanvasController ::: modeID = {modeID}");

        switch (modeID)
        {
            case 1000:      // 앱 최초 실행 시
                Debug.Log("CanvasController ::: 인트로 영상 준비");
                canvasArray[0].SetActive(true);
                break;
            case 1:         // 혼자하기 모드 버튼 클릭 시
                Debug.LogError($"CanvasController ::: GameManager.Instance.modeID // {modeID} 확인 요망");
                break;
            case 0:         // Create Mode에서 Intro Scene으로 넘어올 때
            case 999:
                canvasArray[0].SetActive(false);
                canvasArray[2].SetActive(true);
                canvasArray[4].SetActive(true);

                swipeMenu.value = 0.0f;
                paginations[0].isOn = true;
                buttonSound.bgmSource.Play();
                break;
            case 2:
            case 3:
            case 4:
                Debug.Log("CanvasController ::: 혼자하기 모드 스테이지 선택 화면 오픈");

                canvasArray[0].SetActive(false);
                canvasArray[2].SetActive(true);
                canvasArray[4].SetActive(true);

                swipeMenu.value = 0.5f;
                paginations[1].isOn = true;
                buttonSound.bgmSource.Play();

                buttonManager01.SelectAloneModePanel();
                buttonManager01.ConvertStageSelectionPanel();
                aloneMode[modeID - 2].ChangeModeID();
                break;
            case 5:
            case 6:
            case 7:
            case 8:
            case 9:
                Debug.Log($"CanvasController ::: modeID = {modeID}");

                canvasArray[0].SetActive(false);
                canvasArray[2].SetActive(true);
                canvasArray[4].SetActive(true);

                buttonSound.bgmSource.Play();
                swipeMenu.value = 1.0f;
                paginations[2].isOn = true;
                break;
        }

        GameManager.Instance.modeID = 1000;
    }

    public void LoadCanvas(CanvasID canvasID)
    {
        int _canvasID = (int)canvasID;

        canvasArray[_canvasID].SetActive(true);
        currCanvas.SetActive(false);
    }

    // 혼자하기 모드 유형 선택 화면으로 이동
    public void OpenAloneModeCanvas()
    {
        canvasArray[4].SetActive(true);
        canvasArray[3].SetActive(false);
    }

    // 혼자하기 모드 스테이지 선택 화면으로 이동
    public void OpenStageSelectCanvas()
    {
        canvasArray[5].SetActive(true);
        canvasArray[4].SetActive(false);
    }

    // 홈버튼
    public void HomeButton()
    {
        for (int i = 0; i < canvasArray.Length; i++)
        {
            canvasArray[i].SetActive(false);
        }
        canvasArray[2].SetActive(true);
    }

    // 크레딧
    public void CreditVideo()
    {
        for (int i = 0; i < canvasArray.Length; i++)
        {
            canvasArray[i].SetActive(false);
        }

        canvasArray[3].SetActive(true);
        creditExitButton.SetActive(false);

        settingCan.SetActive(false);
    }
}
