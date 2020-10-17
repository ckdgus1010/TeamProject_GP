using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class VideoPlayerController : MonoBehaviour
{
    [SerializeField]
    private VideoPlayer videoPlayer;
    public float timer = 1.5f;

    // 영상이 끝났는지 확인
    private bool isFinished = false;

    // 영상이 끝난 후 활성화 시킬 Canvas
    [SerializeField] private GameObject loginCanvas;

    // 영상이 끝난 후 비활성화 시킬 object
    [SerializeField] private GameObject introVideoCanvas;

    // 필수 권한 확인
    [SerializeField] private PermissionManager permissionManager;

    void Start()
    {
        isFinished = false;
    }

    void Update()
    {
        if (isFinished == false && videoPlayer.isPrepared && videoPlayer.isPlaying == false)
        {
            Debug.Log("VideoCtrl ::: 영상 끝, 로그인 화면으로 이동");
            
            isFinished = true;
            Invoke("ShowLoginCanvas", timer);
        }
    }

    void ShowLoginCanvas()
    {
        loginCanvas.SetActive(true);
        //permissionManager.RequestPermission();

        introVideoCanvas.SetActive(false);
    }
}
