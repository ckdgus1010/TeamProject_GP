using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using System.Threading;

public class Scroll_Manager : MonoBehaviourPun
{
    public Color[] colors;
    public GameObject scrollbar, imageContent;
    private float scroll_pos = 0;
    private float[] pos;
    public bool runIt = false;
    private float time;
    private Toggle takeTheBtn;
    private int btnNumber;
    public GameObject ARCamera;
    public GameObject scrollview;
    public GameObject notePanel;
    public GameObject waitingClientPopup;
    public GameObject waitingMasterPopup;
    public GameObject masterMapCreateHelp;
    private bool isNotePanelOff;

    [SerializeField]
    private Scrollbar horizontalScrollbar;
    [SerializeField]
    private float lerpSpeed = 10.0f;
    private Vector2 startPos;
    private Vector2 endPos;
    private float value = 0.0f;
    [SerializeField]
    private Toggle[] toggles = new Toggle[4];

    void Update()
    {
        if (runIt)
        {
            time += Time.deltaTime;

            if (time > 2.0f)
            {
                runIt = false;
            }

            return;
        }

        Debug.Log("asdf");

        if (Input.GetMouseButtonDown(0))
        {
            startPos = Input.mousePosition;
        }

        if (Input.GetMouseButton(0) || Input.GetMouseButton(1))
        {
            return;
        }

        if (Input.GetMouseButtonUp(0))
        {
            endPos = Input.mousePosition;

            // 방향 확인
            Vector2 dir = endPos - startPos;
            Debug.Log($"dir.x ::: {dir.x} // scrollbar.value = {horizontalScrollbar.value} \n startPos: {startPos} // endPos: {endPos}");

            // startPos && endPos 초기화
            startPos = Vector2.zero;
            endPos = Vector2.zero;

            // 단순 터치인 경우
            if (dir.x == 0)
            {
                return;
            }

            if (dir.x > 100)
            {
                Debug.Log("왼쪽으로 이동");

                //if (horizontalScrollbar.value < 0.15f)
                //{
                //    value = 0.0f;
                //}
                //else if (horizontalScrollbar.value > 0.15f && horizontalScrollbar.value < 0.45f)
                //{
                //    value = 0.0f;
                //}
                if (horizontalScrollbar.value > 0.5f && horizontalScrollbar.value < 0.77f)
                {
                    value = 0.333f;
                    toggles[1].isOn = true;
                }
                else if (horizontalScrollbar.value > 0.9f)
                {
                    value = 0.666f;
                    toggles[2].isOn = true;
                }
                else
                {
                    value = 0.0f;
                    toggles[0].isOn = true;
                }
            }
            else if (dir.x < -100)
            {
                Debug.Log("오른쪽으로 이동");

                if (horizontalScrollbar.value < 0.1f)
                {
                    value = 0.333f;
                    toggles[1].isOn = true;
                }
                else if (horizontalScrollbar.value > 0.3f && horizontalScrollbar.value < 0.4f)
                {
                    value = 0.666f;
                    toggles[2].isOn = true;
                }
                else
                {
                    value = 1.0f;
                    toggles[3].isOn = true;
                }
                //else if (horizontalScrollbar.value > 0.45f && horizontalScrollbar.value < 0.75f)
                //{
                //    value = 1.0f;
                //}
                //else if (horizontalScrollbar.value > 0.75f)
                //{
                //    value = 1.0f;
                //}
            }
            else
            {
                Debug.Log($"SwipeMenu ::: {dir.x} 단순 터치");
                return;
            }
        }

        horizontalScrollbar.value = Mathf.Lerp(horizontalScrollbar.value, value, lerpSpeed * Time.deltaTime);

        //pos = new float[transform.childCount];
        //float distance = 1f / (pos.Length - 1f);

        //if (runIt)
        //{
        //    GecisiDuzenle(distance, pos, takeTheBtn);
        //    time += Time.deltaTime;

        //    if (time > 1f)
        //    {
        //        time = 0;
        //        runIt = false;
        //    }
        //}

        //for (int i = 0; i < pos.Length; i++)
        //{
        //    pos[i] = distance * i;
        //}

        //if (Input.GetMouseButton(0))
        //{
        //    scroll_pos = scrollbar.GetComponent<Scrollbar>().value;
        //}
        //else
        //{
        //    for (int i = 0; i < pos.Length; i++)
        //    {
        //        if (scroll_pos < pos[i] + (distance / 2) && scroll_pos > pos[i] - (distance / 2))
        //        {
        //            scrollbar.GetComponent<Scrollbar>().value = Mathf.Lerp(scrollbar.GetComponent<Scrollbar>().value, pos[i], 0.1f);
        //        }
        //    }
        //}


        //for (int i = 0; i < pos.Length; i++)
        //{
        //    if (scroll_pos < pos[i] + (distance / 2) && scroll_pos > pos[i] - (distance / 2))
        //    {
        //        //Debug.LogWarning("Current Selected Level" + i);
        //        transform.GetChild(i).localScale = Vector2.Lerp(transform.GetChild(i).localScale, new Vector2(1f, 1f), 0.1f);
        //        imageContent.transform.GetChild(i).localScale = Vector2.Lerp(imageContent.transform.GetChild(i).localScale, new Vector2(1.2f, 1.2f), 0.1f);
        //        imageContent.transform.GetChild(i).GetComponent<Image>().color = colors[1];
        //        for (int j = 0; j < pos.Length; j++)
        //        {
        //            if (j != i)
        //            {
        //                imageContent.transform.GetChild(j).GetComponent<Image>().color = colors[0];
        //                imageContent.transform.GetChild(j).localScale = Vector2.Lerp(imageContent.transform.GetChild(j).localScale, new Vector2(0.8f, 0.8f), 0.1f);
        //                transform.GetChild(j).localScale = Vector2.Lerp(transform.GetChild(j).localScale, new Vector2(0.8f, 0.8f), 0.1f);
        //            }
        //        }
        //    }
        //}
    }

    private void GecisiDuzenle(float distance, float[] pos, Button btn)
    {
        // btnSayi = System.Int32.Parse(btn.transform.name);

        for (int i = 0; i < pos.Length; i++)
        {
            if (scroll_pos < pos[i] + (distance / 2) && scroll_pos > pos[i] - (distance / 2))
            {
                scrollbar.GetComponent<Scrollbar>().value = Mathf.Lerp(scrollbar.GetComponent<Scrollbar>().value, pos[btnNumber], 1f * Time.deltaTime);

            }
        }

        for (int i = 0; i < btn.transform.parent.transform.childCount; i++)
        {
            btn.transform.name = ".";
        }

    }
    public void WhichBtnClicked(int order)
    {
        //btn.transform.name = "clicked";
        //for (int i = 0; i < btn.transform.parent.transform.childCount; i++)
        //{
        //    if (btn.transform.parent.transform.GetChild(i).transform.name == "clicked")
        //    {
        //        btnNumber = i;
        //        takeTheBtn = btn;
        //        time = 0;
        //        scroll_pos = (pos[btnNumber]);
        //        runIt = true;
        //    }
        //}
    }
    public void OnClickSkip()
    {
        print("OnClickSkip");
        scrollview.SetActive(false);
        ARCamera.SetActive(true);
        notePanel.SetActive(true);
        Invoke("WaitingNotePanel", 5.0f);
    }

    public void WaitingNotePanel()
    {
        notePanel.SetActive(false);
        isNotePanelOff = true;
        if (PhotonNetwork.IsMasterClient)
        {
            //바닥을 충분히 인식했다면,원하는 위치를 눌러 맵을 생성하세요.
            masterMapCreateHelp.SetActive(true);
        }
        else
        {
            //방장이 맵을 생성할 때까지 잠시만 기다려주세요! 켜기
            waitingMasterPopup.SetActive(true);
        }
    }
   

}