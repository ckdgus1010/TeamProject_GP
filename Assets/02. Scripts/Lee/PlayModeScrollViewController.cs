using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayModeScrollViewController : MonoBehaviour
{
    [SerializeField]
    private RectTransform rectTr;
    private Vector2 startPos;
    private Vector2 endPos = Vector2.zero;
    private Vector2 destination;

    public float lerpSpeed = 5.0f;
    public bool isButtonClicked = false;

    [SerializeField]
    private GameObject backgroundImg;

    void Start()
    {
        isButtonClicked = false;
        startPos = rectTr.anchoredPosition;

        backgroundImg.SetActive(isButtonClicked);
    }

    void Update()
    {
        destination = isButtonClicked ? endPos : startPos;
        rectTr.anchoredPosition = Vector2.Lerp(rectTr.anchoredPosition, destination, lerpSpeed * Time.deltaTime);

        backgroundImg.SetActive(isButtonClicked);
    }
}
