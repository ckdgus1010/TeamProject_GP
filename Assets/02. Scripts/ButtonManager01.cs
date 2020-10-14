using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Lee
{
    public class ButtonManager01 : MonoBehaviour
    {
        #region 로그인 화면

        [Header("Login Panel")]
        [SerializeField] private LoginPopupController loginPopupController;
        [SerializeField] private SignupController signupController;
        [SerializeField] private PlayFabManager playFabManager;

        // 로그인 패널
        public void ConvertLoginPanel()
        {
            Debug.Log($"ButtonManager ::: 로그인 패널 {!loginPopupController.isButtonClicked}");
            loginPopupController.isButtonClicked = !loginPopupController.isButtonClicked;
        }

        // 회원가입 패널
        public void ConvertSignupPanel()
        {
            Debug.Log($"ButtonManager ::: 회원가입 패널 {!signupController.isButtonClicked}");
            signupController.isButtonClicked = !signupController.isButtonClicked;
        }

        public void LoginButtonClick()
        {
            playFabManager.LoginButton();
        }

        #endregion


    }
}
