using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using PlayFab;
using PlayFab.ClientModels;
using Lee;
using PlayFab.GroupsModels;

public class PlayFabManager : MonoBehaviour
{
    public ButtonManager01 buttonManager01;
    public GameObject loginPanel;
    public GameObject mainMenuPanel;

    // 로그인 팝업창
    [Header("Login Popup")]
    public InputField username;
    public InputField password;

    // 로그인 코루틴 중복 실행 방지
    private bool isChecking = false;

    // 로그인 코루틴이 끝났는지 확인
    private bool isLoginCheckFinished = false;

    // 회원 가입 팝업창
    [Header("Signup Popup")]
    public InputField emailInput;
    public InputField passwordInput;
    public InputField nicknameInput;

    private void Start()
    {
        PlayFabSettings.TitleId = "90ED5";
    }

    #region 로그인

    //로그인
    public void LoginButton()
    {
        if (isChecking == false)
        {
            Debug.Log($"로그인 버튼 누름");

            StartCoroutine(LoginSequence());
        }
    }

    IEnumerator LoginSequence()
    {
        isChecking = true;

        Debug.Log($"PlayFabManager ::: 로그인 코루틴 들어옴");

        var request = new LoginWithPlayFabRequest { Username = username.text, Password = password.text};
        PlayFabClientAPI.LoginWithPlayFab(request, OnLoginSuccess, OnLoginFailure);
        Debug.Log("PlayerFabManager ::: 로그인 요청");

        // 로딩 화면
        LoadingSceneController.Instance.LoadScene("04. MainMenu");

        yield return new WaitUntil(() => isLoginCheckFinished == true);

        isChecking = false;
        isLoginCheckFinished = false;
    }

    //로그인 성공
    private void OnLoginSuccess(LoginResult result)
    {
        isLoginCheckFinished = true;

        // LoadingUI에 로그인이 성공했다고 전달
        LoadingSceneController.Instance.isChecked = true;
        LoadingSceneController.Instance.status = LoadingSceneController.Status.Success;

        Debug.Log("PlayFabManager ::: 로그인 성공");
        Debug.Log($"PlayFabManager ::: {result.PlayFabId}");

        //서버에서 player profile 받아오기
        GetPlayerProfile(result.PlayFabId);

        //mainMenuPanel.SetActive(true);
        //loginPanel.SetActive(false);
    }

    //로그인 실패
    private void OnLoginFailure(PlayFabError error)
    {
        isLoginCheckFinished = true;

        // LoadingUI에 로그인이 실패했다고 전달
        LoadingSceneController.Instance.isChecked = true;
        LoadingSceneController.Instance.status = LoadingSceneController.Status.Failure;

        Debug.LogError($"PlayFabManager ::: 로그인 실패 \n {error.GenerateErrorReport()}");
    }

    #endregion


    //-------------------------------------------------------------------------


    #region 회원가입

    // 회원가입
    // 입력 받는 정보: Email, pw, 유저 이름, 닉네임
    public void RegisterButton()
    {
        var request = new RegisterPlayFabUserRequest { Email = emailInput.text, Password = passwordInput.text, Username = nicknameInput.text, DisplayName = nicknameInput.text };
        PlayFabClientAPI.RegisterPlayFabUser(request, OnRegisterSuccess, OnRegisterFailure);
    }

    //회원 가입 성공
    private void OnRegisterSuccess(RegisterPlayFabUserResult result)
    {
        Debug.Log("PlayFabManager ::: 회원 가입 성공");
        Debug.Log($"PlayerFabManger ::: {result.Username}");

    }

    //회원 가입 실패
    private void OnRegisterFailure(PlayFabError error)
    {
        Debug.Log("PlayFabManager ::: 회원 가입 실패");
        Debug.LogError(error.GenerateErrorReport());
    }

    #endregion


    //-------------------------------------------------------------------------


    #region 로그인 시 가져와야 할 정보

    // 로그인 시 가져와야 할 정보
    // 1. 유저 이름
    // 2. 닉네임
    // 3. 가입한 email
    // 4. 저장한 데이터 (혼자하기 모드 클리어 정보, 업적, 권한 정보)

    void GetPlayerProfile(string playFabId)
    {
        var request = new GetPlayerProfileRequest { PlayFabId = playFabId, ProfileConstraints = new PlayerProfileViewConstraints() { ShowDisplayName = true } };
        PlayFabClientAPI.GetPlayerProfile(request, OnGetPlayerProfileSuccess, OnGetPlayerProfileFailure);

        //PlayFabClientAPI.GetPlayerProfile(new GetPlayerProfileRequest(){ PlayFabId = playFabId, ProfileConstraints = new PlayerProfileViewConstraints() { ShowDisplayName = true } }
        //                                                               , result => Debug.Log("The player's DisplayName profile data is: " + result.PlayerProfile.DisplayName)
        //                                                               , error => Debug.LogError( error.GenerateErrorReport() ) );
    }

    void OnGetPlayerProfileSuccess(GetPlayerProfileResult result)
    {
        Debug.Log("The player's DisplayName profile data is: " + result.PlayerProfile.DisplayName);
        GameManager.Instance.username = result.PlayerProfile.DisplayName;
    }

    void OnGetPlayerProfileFailure(PlayFabError error)
    {
        Debug.LogError(error.GenerateErrorReport());
    }

    #endregion
}
