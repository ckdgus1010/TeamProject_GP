using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using GooglePlayGames;

public class GoogleManager : MonoBehaviour
{
    public Text logText;

    void Start()
    {
        PlayGamesPlatform.DebugLogEnabled = true;
        PlayGamesPlatform.Activate();
    }

    public void Login()
    {
        Social.localUser.Authenticate((bool success) =>
        {
            if (success) logText.text = Social.localUser.id + "\n" + Social.localUser.userName;
            else logText.text = "구글 로그인 실패";
        });
    }

    public void Logout()
    {
        ((PlayGamesPlatform)Social.Active).SignOut();
        logText.text = "구글 로그아웃";
    }
}
