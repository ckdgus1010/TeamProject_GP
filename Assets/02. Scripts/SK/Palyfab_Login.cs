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

    public GameObject notePanel;
    Text noteText;
    private string username;
    private string password;
    private string email;

    public static string myPlayfabInfo;
    //TitleId를 세팅
    void Start()
    {
        PlayFabSettings.TitleId = "90ED5";
        noteText = notePanel.GetComponentInChildren<Text>();
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
        myPlayfabInfo = request.Username;
        PlayFabClientAPI.LoginWithPlayFab(request, OnLoginSuccess, OnLoginFailure);
    }

    private void OnLoginSuccess(LoginResult result)
    {
        Debug.Log("로그인 성공");
        SceneManager.LoadScene("04. MainMenu");
    }

    private void OnLoginFailure(PlayFabError error)
    {
        Debug.LogWarning("로그인 실패");
        notePanel.SetActive(true);
        noteText.text = "아이디 혹은 비밀번호가 일치하지 않습니다 \n 다시 확인해주세요";
        Debug.LogWarning(error.GenerateErrorReport());
    }
    #endregion


    #region 회원가입
    public void Register()
    {      
        // 다음의 로그인 정보를 가지고 회원가입을 한다. 
        var request = new RegisterPlayFabUserRequest { Username = username, Password = password, Email = email, DisplayName = username};
        PlayFabClientAPI.RegisterPlayFabUser(request, RegisterSuccess, RegisterFailure);
    }

    private void RegisterSuccess(RegisterPlayFabUserResult result)
    {
        Debug.Log("가입 성공");
 
    }

    private void RegisterFailure(PlayFabError error)
    {
        Debug.LogWarning("가입 실패");
        notePanel.SetActive(true);
        noteText.text = "비밀번호 6자 이상 \n xxx@xxx.xxx 양식으로 다시 시도하세요";

        Debug.LogWarning(error.GenerateErrorReport());
        
    }

    //RequireBothUsernameAndEmail = false로 설정하면 아이디 (Username) 하나만 받는다.
    #endregion

    public void OnCilck_XBt()
    {
        notePanel.SetActive(false);
    }
}
