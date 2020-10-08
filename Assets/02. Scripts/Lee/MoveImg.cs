using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveImg : MonoBehaviour
{
    private RectTransform rectTr;
    public RectTransform endPos;
    public float speed = 2.5f;
    public Vector2 dir;
    public bool isButtonClicked = false;

    private void Start()
    {
        rectTr = GetComponent<RectTransform>();
        dir = endPos.anchoredPosition - rectTr.anchoredPosition;
    }

    void Update()
    {
        if (isButtonClicked)
        {
            rectTr.anchoredPosition += dir * speed * Time.deltaTime;
        }
    }

    public void MoveImage()
    {
        isButtonClicked = true;
    }
}
