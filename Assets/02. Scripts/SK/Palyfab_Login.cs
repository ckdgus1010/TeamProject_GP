using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using PlayFab;
using PlayFab.ClientModels;
using UnityEngine.SceneManagement;
using TMPro;

public class Palyfab_Login : MonoBehaviour
{
    public InputField ID_Input;
    public InputField PW_Input;
    public GameObject Email_Input;
    public InputField Email_InputField;

    public GameObject notePanel;

    Text noteText;
 
    private string username;
    private string password;
    private string email;

    public static string myPlayfabInfo;

    public GameObject loadingPanel;
    public GameObject signInImage;
    public GameObject signUpImage;
    public GameObject email_Active_Image;

    private float timer = 0;
    private bool isLoginTried = false;
    private bool isSignUpCount;

    private Animator id_Animator;
    private Animator pw_Animator;

    //TitleId를 세팅
    void Start()
    {
        PlayFabSettings.TitleId = "90ED5";
        noteText = notePanel.GetComponentInChildren<Text>();

        id_Animator = ID_Input.GetComponent<Animator>();
        pw_Animator = PW_Input.GetComponent<Animator>();

        Email_InputField = Email_Input.GetComponent<InputField>();
    }

    private void Update()
    {
        if (isLoginTried == true)
        {
            timer += Time.deltaTime;
        }
    }

    #region Inputfiled
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
        email = Email_InputField.text.ToString();
    }

# endregion

    #region 로그인
    public void Login()
    {
        isLoginTried = true;
        // 다음의 로그인 정보를 가지고 로그인한다. 
        var request = new LoginWithPlayFabRequest { Username = ID_Input.text, Password = PW_Input.text};
        //var request = new LoginWithEmailAddressRequest { Email = Email_InputField.text, Password = PW_Input.text };
        //PlayFab 서버로 로그인
        myPlayfabInfo = request.Username;
        PlayFabClientAPI.LoginWithPlayFab(request, OnLoginSuccess, OnLoginFailure);
        //loadingPanel.SetActive(true);
       
    //    var request2 = new UpdateUserTitleDisplayNameRequest { DisplayName = username };
    //    PlayFabClientAPI.UpdateUserTitleDisplayName(request2, DisplayNameUpdateSuccess, DisplayNameUpdateFailure);
    }

    //private void DisplayNameUpdateSuccess(LoginResult   result)
    //{
    //    Debug.Log("로그인 성공");
    //    SceneManager.LoadScene("04. MainMenu");
    //}

    //private void DisplayNameUpdateFailure(PlayFabError error)
    //{
    //    Debug.LogWarning("로그인 실패");
    //    notePanel.SetActive(true);
    //    noteText.text = "아이디 혹은 비밀번호가 일치하지 않습니다 \n 다시 확인해주세요";
    //    Debug.LogWarning(error.GenerateErrorReport());
    //}

    private void OnLoginSuccess(LoginResult result)
    {
        Debug.Log("로그인 성공");
        Debug.Log(timer);
        isLoginTried = false;
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

    public void OnClickSignInButton()
    {
        if (isSignUpCount == false)
        {
            return;
        }
        signInImage.SetActive(true);
        signUpImage.SetActive(false);
        id_Animator.SetBool("SignUP_ID", false);
        id_Animator.SetBool("SignIn_ID", true);
        pw_Animator.SetBool("SignUP_PW", false);
        pw_Animator.SetBool("SignIn_PW", true);
        Email_Input.SetActive(false);
        email_Active_Image.SetActive(false);
    }

    public void OnClickSignUpButton()
    {
        signInImage.SetActive(false);
        signUpImage.SetActive(true);
        id_Animator.SetBool("SignUP_ID", true);
        id_Animator.SetBool("SignIn_ID", false);
        pw_Animator.SetBool("SignUP_PW", true);
        pw_Animator.SetBool("SignIn_PW", false);
        Email_Input.SetActive(true);
        email_Active_Image.SetActive(true);
        isSignUpCount = true;
    }
    public void OnCilck_XBt()
    {
        notePanel.SetActive(false);
    }
}
