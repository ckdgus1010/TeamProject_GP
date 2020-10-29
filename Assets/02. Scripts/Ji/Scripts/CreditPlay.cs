using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class CreditPlay : MonoBehaviour
{
    private VideoPlayer videoPlayer;
    float currentTime;
    [Range(1, 5)]
    public float creatTime = 0.5f;

    public GameObject skip;

    // 스크립트
    public CanvasController canCtrl;
    public ButtonSound buttonSound;
    void Start()
    {
        videoPlayer = GetComponent<VideoPlayer>();
    }

    void Update()
    {
        buttonSound.bgmSlider.value = 0f;
        if (videoPlayer.isPrepared)
        {
            currentTime += Time.deltaTime;
            if (currentTime >= 27f)
            {
                Debug.Log("영상 끝");
                skip.SetActive(true);
                
            }
        }
    }

    public void SkipButtonOn()
    {
        canCtrl.canvasArray[6].SetActive(false);
        canCtrl.canvasArray[2].SetActive(true);

        buttonSound.bgmSlider.value = 1f;
    }
}
