using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadingController : MonoBehaviour
{
    public bool isLoadingStatus = false;

    [SerializeField]
    private RectTransform rectTr;
    private Vector2 startPos;
    private Vector2 endPos = new Vector2 (0, -1250);
    private Vector2 destination;
    private float lerpSpeed = 2.5f;

    private void Start()
    {
        startPos = rectTr.anchoredPosition;
    }

    void Update()
    {
        destination = isLoadingStatus ? endPos : startPos;
        rectTr.anchoredPosition = Vector2.Lerp(rectTr.anchoredPosition, destination, lerpSpeed * Time.deltaTime);
    }
}
