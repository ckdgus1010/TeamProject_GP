using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Lee
{
    public class ButtonManager01 : MonoBehaviour
    {
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
        [SerializeField] private GameObject settingPanel;
        [SerializeField] private AndroidPlugin androidPlugin;

        // 프로필 팝업창
        public void ConvertProfilePanel()
        {
            Debug.Log($"ButtonManager01 ::: profile panel // {!profilePanel.activeSelf}");
            profilePanel.SetActive(!profilePanel.activeSelf);
        }

        // 옵션 팝업창
        public void ConvertSettingPanel()
        {
            Debug.Log($"ButtonManager01 ::: setting panel // {!settingPanel.activeSelf}");
            settingPanel.SetActive(!settingPanel.activeSelf);
        }

        // 프로필 사진 바꾸기
        public void ChangeProfilePhoto()
        {
            androidPlugin.GalleryOpen();
        }

        #endregion
    }
}
