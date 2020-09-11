using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//목표: 설정 버튼을 눌렀을 때 설정 팝업창을 이동시킨다
// 1. 설정 버튼을 눌렀을때
// 2. 위치를
// 3. 이동시킨다

public class TestScript : MonoBehaviour
{
    public GameObject profile;
    public GameObject blackBG;

    public RectTransform rectTr;
    public RectTransform startPos;
    public RectTransform endPos;
    public RectTransform destination;

    public bool isProfileOn = false;
    public float lerpSpeed = 2.0f;

    private void Start()
    {
        rectTr = profile.GetComponent<RectTransform>();
        isProfileOn = false;
    }


    void Update()
    {
        if (isProfileOn == true)
        {
            destination = endPos;
        }
        else
        {
            destination = startPos;
        }

        rectTr.position = Vector2.Lerp(rectTr.position, destination.position, Time.deltaTime * lerpSpeed);
    }

    public void MoveProfile()
    {
        blackBG.SetActive(!blackBG.activeSelf);
        isProfileOn = blackBG.activeSelf;
    }
}
