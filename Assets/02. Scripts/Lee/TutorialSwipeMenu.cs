using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialSwipeMenu : MonoBehaviour
{
    [SerializeField] private Scrollbar horizontalScrollbar;
    [SerializeField] private Toggle[] paginations = new Toggle[4];

    [SerializeField] private float sensitivity = 150.0f;
    [SerializeField] private float lerpSpeed = 10.0f;
    private float value;
    private float initialValue;
    private Vector2 startPos;
    private Vector2 endPos;

    [SerializeField] private Button startButton;

    private void Start()
    {
        startButton.interactable = false;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            startPos = Input.mousePosition;
            initialValue = horizontalScrollbar.value;
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
            //Debug.Log($"dir.x ::: {dir.x} // scrollbar.value = {horizontalScrollbar.value} \n startPos: {startPos} // endPos: {endPos}");

            // startPos && endPos 초기화
            startPos = Vector2.zero;
            endPos = Vector2.zero;

            // 단순 터치인 경우
            if (dir.x == 0)
            {
                return;
            }

            if (dir.x > sensitivity)
            {
                //Debug.Log("왼쪽으로 이동");

                if (initialValue < 0.35f)
                {
                    value = 0.0f;
                    paginations[0].isOn = true;
                }
                else if (initialValue > 0.35f && initialValue < 0.7f)
                {
                    value = 0.3333f;
                    paginations[1].isOn = true;
                }
                else if (initialValue > 0.75f)
                {
                    value = 0.6666f;
                    paginations[2].isOn = true;
                }
            }
            else if (dir.x < -sensitivity)
            {
                //Debug.Log("오른쪽으로 이동");

                if (initialValue < 0.15f)
                {
                    value = 0.3333f;
                    paginations[1].isOn = true;
                }
                else if (initialValue > 0.15f && initialValue < 0.45f)
                {
                    value = 0.6666f;
                    paginations[2].isOn = true;
                }
                else if (initialValue > 0.45f && initialValue < 0.85f)
                {
                    value = 1.0f;
                    paginations[3].isOn = true;
                    startButton.interactable = true;
                }
            }
            else
            {
                //Debug.Log($"SwipeMenu ::: {dir.x} 단순 터치");
                return;
            }
        }

        horizontalScrollbar.value = Mathf.Lerp(horizontalScrollbar.value, value, lerpSpeed * Time.deltaTime);
    }
}
