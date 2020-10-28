using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
namespace Lee
{
    public class ButtonManager01 : MonoBehaviour
    {
        [SerializeField] private GameObject mainMenuCanvas;
        [SerializeField] private GameObject playModeCanvas;
        [SerializeField] private GameObject aloneModeCanvas;
        [SerializeField] private GameObject aloneStageCanvas;

        #region 로그인 화면

        [Header("Login Panel")]
        [SerializeField] private PermissionManager permissionManager;
        [SerializeField] private LoginPopupController loginPopupController;
        [SerializeField] private PlayFabManager playFabManager;
        [SerializeField] private GoogleLoginManager googleLoginManager;
        [SerializeField] private GameObject loginErrorPanel;

        [Header("Register Panel")]
        [SerializeField] private SignupController signupController;
        [SerializeField] private GameObject registerErrorPanel;

        // 필수 권한 요청
        public void RequestPermission()
        {
            permissionManager.RequestPermission();
        }

        // 로그인 패널
        public void ConvertLoginPanel()
        {
            Debug.Log($"ButtonManager ::: 로그인 패널 {!loginPopupController.isButtonClicked}");
            loginPopupController.isButtonClicked = !loginPopupController.isButtonClicked;

            // 로그인 패널을 숨길 때 ID / PW 입력란 초기화
            if (loginPopupController.isButtonClicked == false)
            {
                loginPopupController.ResetInputFields();
            }
        }

        // PlayFab 로그인
        public void Login_PlayFab()
        {
            playFabManager.LoginButton();
        }

        // 구글 로그인
        public void Login_Google()
        {
            googleLoginManager.Login();
        }

        // 로그인 오류 패널
        public void ConvertErrorPanel_Login()
        {
            Debug.Log($"ButtonManager01 ::: 로그인 오류 패널 {!loginErrorPanel.activeSelf}");
            loginErrorPanel.SetActive(!loginErrorPanel.activeSelf);
        }

        // 회원가입 패널
        public void ConvertSignupPanel()
        {
            Debug.Log($"ButtonManager ::: 회원가입 패널 {!signupController.isButtonClicked}");
            signupController.isButtonClicked = !signupController.isButtonClicked;

            // 회원가입 패널을 숨길 때 ID / PW / Nickname 입력란 초기화
            if (signupController.isButtonClicked == false)
            {
                signupController.ResetInputFields();
            }
        }

        // 회원가입 오류 패널
        public void ConvertErrorPanel_Register()
        {
            Debug.Log($"ButtonManager01 ::: 회원가입 오류 패널 {registerErrorPanel.activeSelf}");
            registerErrorPanel.SetActive(!registerErrorPanel.activeSelf);
        }

        #endregion



        #region 메인 메뉴 화면

        [Header("Main Menu Panel")]
        [SerializeField] private GameObject profilePanel;
        [SerializeField] private GameObject settingCanvas;
        [SerializeField] private Use use;
        [SerializeField] private ProfileImageData profileButtonImage;


        // 프로필 팝업창
        public void ConvertProfilePanel()
        {
            Debug.Log($"ButtonManager01 ::: profile panel // {!profilePanel.activeSelf}");
            profilePanel.SetActive(!profilePanel.activeSelf);
        }

        // 프로필 사진 바꾸기
        public void ChangeProfilePhoto()
        {
            Debug.Log("ButtonManager01 ::: 프로필 사진 바꾸기 버튼 클릭");
            use.GalleryOpen();
        }

        // 옵션 팝업창
        public void ConvertSettingPanel()
        {
            Debug.Log($"ButtonManager01 ::: setting panel // {!settingCanvas.activeSelf}");
            settingCanvas.SetActive(!settingCanvas.activeSelf);
        }

        // 배경음악 조절
        public void ControlBGM()
        {
            Debug.Log($"ButtonManager01 ::: BGM // ");
        }

        // 효과음 조절
        public void ControlEffectSound()
        {
            Debug.Log($"ButtonManager01 ::: Effect Sound // ");
        }

        // 튜토리얼 버튼
        public void OpenTutorialCanvas()
        {
            Debug.Log($"ButtonManager01 ::: 튜토리얼 버튼 클릭");
            SceneManager.LoadScene("03. Tutorial");
        }

        // 만든이 버튼
        public void OpenCreditCanvas()
        {
            Debug.Log($"ButtonManager01 ::: 만든이 버튼 클릭");
        }


        #endregion



        #region 혼자하기 모드 유형 선택화면

        [SerializeField] private StageData[] stageButtons = new StageData[9];

        // 스테이지 정보 갱신
        public void UpdateStageData()
        {
            Debug.Log("ButtonManager01 ::: 스테이지 정보 갱신");

            for (int i = 0; i < stageButtons.Length; i++)
            {
                stageButtons[i].UpdateStageData();
            }
        }

        #endregion




        // 메인 메뉴 화면 → 플레이 모드 선택 화면으로 이동
        public void ChangePlayModeCanvas()
        {
            Debug.Log("ButtonManager01 ::: 플레이 모드 선택화면으로 이동");

            playModeCanvas.SetActive(true);
            mainMenuCanvas.SetActive(false);
            GameManager.Instance.modeID = 9;
        }

        // 뒤로 가기 (플레이 모드 선택 화면 → 메인 메뉴 화면)
        public void BacktoMainMenuCanvas()
        {
            Debug.Log("ButtonManager01 ::: 뒤로가기 버튼 클릭 // 메인 메뉴로 이동");
            GameManager.Instance.modeID = 1000;

            mainMenuCanvas.SetActive(true);
            playModeCanvas.SetActive(false);
        }

        // 뒤로 가기(혼자하기 모드 유형 선택 화면 → 플레이 모드 선택 화면)
        public void BacktoPlayModeSelectCanvas()
        {
            Debug.Log("ButtonManager01 ::: 뒤로가기 버튼 클릭 // 플레이모드 선택 화면으로 이동");
            GameManager.Instance.modeID = 1000;

            playModeCanvas.SetActive(true);
            aloneModeCanvas.SetActive(false);
        }

        // 뒤로 가기 (혼자하기 모드 스테이지 선택 화면 → 혼자하기 모드 유형 선택 화면)
        public void BacktoAloneModeSelectCanvas()
        {
            Debug.Log("ButtonManager01 ::: 뒤로가기 버튼 클릭 // 혼자하기 모드 유형 선택 화면으로 이동");
            GameManager.Instance.modeID = 1;

            aloneModeCanvas.SetActive(true);
            aloneStageCanvas.SetActive(false);
        }
    }
}
