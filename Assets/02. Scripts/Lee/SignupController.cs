using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SignupController : MonoBehaviour
{
    private RectTransform rectTr;
    private Vector2 startPos;
    private Vector2 endPos = Vector2.zero;
    private Vector2 destination;

    public bool isButtonClicked = false;
    public float lerpSpeed = 3.0f;

    void Start()
    {
        rectTr = GetComponent<RectTransform>();
        isButtonClicked = false;
        startPos = rectTr.anchoredPosition;
    }

    void Update()
    {
        destination = isButtonClicked ? endPos : startPos;
        rectTr.anchoredPosition = Vector2.Lerp(rectTr.anchoredPosition, destination, lerpSpeed * Time.deltaTime);
    }
}
