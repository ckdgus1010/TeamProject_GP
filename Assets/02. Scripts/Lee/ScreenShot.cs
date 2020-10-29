using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;

public class ScreenShot : MonoBehaviour
{
    //카메라 셔터 소리
    public AudioSource audioSource;
    
    //스크린샷을 찍을 때 깜빡이는 이미지
    public GameObject blink;

    //스크린샷을 찍을 때 잠시 꺼놔야 하는 UI
    public GameObject[] UIArray = new GameObject[8];

    private bool isCoroutinePlaying;                // 코루틴 중복방지

    // 파일 불러올 때 필요
    string albumName = "Stack Tree";        // 생성될 앨범의 이름
    
    public GameObject shareButtons;         // 공유 버튼
    public GameObject panel;                // 찍은 사진이 뜰 패널

    private void Start()
    {
        isCoroutinePlaying = false;
    }

    // 캡쳐 버튼을 누르면 호출
    public void Capture_Button()
    {
        // 중복방지 bool
        if (isCoroutinePlaying == false)
        {
            StartCoroutine("captureScreenshot");
        }
    }

    IEnumerator captureScreenshot()
    {
        isCoroutinePlaying = true;

        // UI 없앤다...
        Debug.Log($"ScreenShot ::: {UIArray.Length}");
        for (int i = 0; i < UIArray.Length; i++)
        {
            UIArray[i].SetActive(false);
        }
        Debug.Log("ScreenShot ::: UI 비활성화 완료");
        yield return new WaitForEndOfFrame();

        // 스크린샷 + 갤러리갱신
        ScreenshotAndGallery();
        Debug.Log("ScreenShot ::: 스크린샷 + 갤러리갱신 완료");
        yield return new WaitForEndOfFrame();

        // 블링크
        //BlinkUI();
        blink.SetActive(true);
        Debug.Log("ScreenShot ::: blink 켜짐");

        // 셔터 사운드 넣기...
        audioSource.Play();
        Debug.Log("ScreenShot ::: 카메라 셔터 소리");
        yield return new WaitForEndOfFrame();
        Debug.Log("ScreenShot ::: blink 꺼짐");
        blink.SetActive(false);

        // UI 다시 나온다...
        for (int i = 0; i < UIArray.Length; i++)
        {
            UIArray[i].SetActive(true);
        }

        switch (GameManager.Instance.modeID)
        {
            case 0:
            case 2:
                UIArray[3].SetActive(false);
                break;
            case 1:
            case 3:
            case 4:
            case 5:
            case 6:
            case 7:
            case 8:
                UIArray[6].SetActive(false);
                break;
        }
        Debug.Log("ScreenShot ::: UI 활성화 완료");
        yield return new WaitForSecondsRealtime(0.3f);

        // 찍은 사진이 등장
        //GetPirctureAndShowIt();

        isCoroutinePlaying = false;
        Debug.Log("ScreenShot ::: 스크린샷 기능 완료");
    }

    // 흰색 블링크 생성
    void BlinkUI()
    {
        blink.SetActive(true);
        GameObject b = Instantiate(blink);
        b.transform.SetParent(transform);
        b.transform.localPosition = new Vector3(0, 0, 0);
        b.transform.localScale = new Vector3(1, 1, 1);
    }

    // 스크린샷 찍고 갤러리에 갱신
    void ScreenshotAndGallery()
    {
        // 스크린샷
        // 2d 텍스쳐객체 > 넓이, 높이, 포멧 RGB24 설정, true?false?
        Texture2D ss = new Texture2D(Screen.width, Screen.height, TextureFormat.RGB24, false);
        Debug.Log("ss 설정");
        // 현재 화면을 픽셀단위로 읽음
        ss.ReadPixels(new Rect(0, 0, Screen.width, Screen.height), 0, 0);
        Debug.Log("ss.ReadPixels");

        // 적용
        ss.Apply();
        Debug.Log("ss.Apply()");


        // 갤러리갱신
        Debug.Log("" + NativeGallery.SaveImageToGallery(ss, albumName,
            "Screenshot_" + System.DateTime.Now.ToString("dd-MM-yyyy-HH-mm-ss") + ".png"));
        
        // To avoid memory leaks.
        // 복사 완료됐기 때문에 원본 메모리 삭제
        Destroy(ss);
        Debug.Log("ScreenshotAndGallery() 완료");
    }

    // 찍은 사진을 Panel에 보여준다.
    void GetPirctureAndShowIt()
    {
        string pathToFile = GetPicture.GetLastPicturePath();
        if (pathToFile == null)
        {
            return;
        }
        Texture2D texture = GetScreenshotImage(pathToFile);
        Sprite sp = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f));
        panel.SetActive(true);
        shareButtons.SetActive(true);
        panel.GetComponent<Image>().sprite = sp;
    }

    // 찍은 사진을 불러온다.
    Texture2D GetScreenshotImage(string filePath)
    {
        Texture2D texture = null;
        byte[] fileBytes;
        if (File.Exists(filePath))
        {
            fileBytes = File.ReadAllBytes(filePath);
            texture = new Texture2D(2, 2, TextureFormat.RGB24, false);
            texture.LoadImage(fileBytes);
        }
        return texture;
    }
}