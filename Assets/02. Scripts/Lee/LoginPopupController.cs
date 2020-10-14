using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoginPopupController : MonoBehaviour
{
    private RectTransform rectTr;
    private Vector2 startPos;
    private Vector2 endPos = Vector2.zero;
    private Vector2 destination;

    [HideInInspector]
    public bool isButtonClicked = false;
    public float lerpSpeed = 5.0f;

    private void Start()
    {
        rectTr = GetComponent<RectTransform>();
        startPos = rectTr.anchoredPosition;
        isButtonClicked = false;
    }

    private void Update()
    {
        destination = isButtonClicked ? endPos : startPos;
        rectTr.anchoredPosition = Vector2.Lerp(rectTr.anchoredPosition, destination, lerpSpeed * Time.deltaTime);
    }
}
