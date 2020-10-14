using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using GooglePlayGames;

public class GoogleManager : MonoBehaviour
{
    void Start()
    {
        PlayGamesPlatform.DebugLogEnabled = true;
        PlayGamesPlatform.Activate();
    }

    public void Login()
    {
        Social.localUser.Authenticate((bool success) =>
        {
            if (success)
            {
                Debug.Log($"{Social.localUser.id} \n {Social.localUser.userName}");
                SceneManager.LoadScene("04. MainMenu");
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
