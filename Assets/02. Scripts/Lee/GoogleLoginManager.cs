using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using GooglePlayGames;

public class GoogleLoginManager : MonoBehaviour
{
    [SerializeField] private GameObject loginCanvas;
    [SerializeField] private GameObject mainMenuCanvas;

    // 코루틴 중복 방지
    private bool isChecking = false;

    void Start()
    {
        PlayGamesPlatform.DebugLogEnabled = true;
        PlayGamesPlatform.Activate();
    }

    public void Login()
    {
        if (isChecking == false)
        {
            Debug.Log($"GoogleLoginManager ::: 구글 로그인 시작");
            StartCoroutine(GoogleLogin());
        }
    }

    IEnumerator GoogleLogin()
    {
        isChecking = true;
        Debug.Log($"GoogleLoginManager ::: 구글 로그인 코루틴 들어옴");

        yield return null;
        Social.localUser.Authenticate((bool success) =>
        {
            if (success)
            {
                Debug.Log($"{Social.localUser.id} \n {Social.localUser.userName}");
                GameManager.Instance.username = Social.localUser.userName;

                mainMenuCanvas.SetActive(true);
                loginCanvas.SetActive(false);
            }
            else
            {
                Debug.Log("구글 로그인 실패");
            }
        });
    }

    public void Logout()
    {
        ((PlayGamesPlatform)Social.Active).SignOut();
        Debug.Log("구글 로그아웃");
    }
}
