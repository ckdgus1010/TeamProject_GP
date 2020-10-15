using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using GooglePlayGames;

public class GoogleManager : MonoBehaviour
{
    [SerializeField] private GameObject loginPanel;
    [SerializeField] private GameObject mainMenuPanel;

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
                GameManager.Instance.username = Social.localUser.userName;

                //mainMenuPanel.SetActive(true);
                //loginPanel.SetActive(false);
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
