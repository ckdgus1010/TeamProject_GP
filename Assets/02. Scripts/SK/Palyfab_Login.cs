using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using PlayFab;
using PlayFab.ClientModels;
using UnityEngine.SceneManagement;
public class Palyfab_Login : MonoBehaviour
{
    public InputField ID_Input;
    public InputField PW_Input;
    public InputField Email_Input;
    public Text ErrorText;

    private string username;
    private string password;
    private string email;

    //TitleId를 세팅
    void Start()
    {
        PlayFabSettings.TitleId = "90ED5";
    }

    //InputField의 Value값이 변경되면 해당함수를 실행
    public void ID_value_Changed()
    {
        username = ID_Input.text.ToString();
    }

    public void PW_value_Changed()
    {
        password = PW_Input.text.ToString();
    }

    public void Email_value_Changed()
    {
        email = Email_Input.text.ToString();
    }
    #region 로그인
    public void Login()
    {
        // 다음의 로그인 정보를 가지고 로그인한다. 
        var request = new LoginWithPlayFabRequest { Username = username, Password = password };
        //PlayFab 서버로 로그인
        PlayFabClientAPI.LoginWithPlayFab(request, OnLoginSuccess, OnLoginFailure);
    }

    private void OnLoginSuccess(LoginResult result)
    {
        Debug.Log("로그인 성공");
        SceneManager.LoadScene("04. MainMenu");
        ErrorText.text = "로그인 성공";
    }

    private void OnLoginFailure(PlayFabError error)
    {
        Debug.LogWarning("로그인 실패");
        Debug.LogWarning(error.GenerateErrorReport());
        ErrorText.text = error.GenerateErrorReport();
    }
    #endregion


    #region 회원가입
    public void Register()
    {      
        // 다음의 로그인 정보를 가지고 회원가입을 한다. 
        var request = new RegisterPlayFabUserRequest { Username = username, Password = password, Email = email };
        PlayFabClientAPI.RegisterPlayFabUser(request, RegisterSuccess, RegisterFailure);
    }

    private void RegisterSuccess(RegisterPlayFabUserResult result)
    {
        Debug.Log("가입 성공");
        ErrorText.text = "가입 성공";
    }

    private void RegisterFailure(PlayFabError error)
    {
        Debug.LogWarning("가입 실패");
        Debug.LogWarning(error.GenerateErrorReport());
        ErrorText.text = error.GenerateErrorReport();
    }

    //RequireBothUsernameAndEmail = false로 설정하면 아이디 (Username) 하나만 받는다.
    #endregion
}
