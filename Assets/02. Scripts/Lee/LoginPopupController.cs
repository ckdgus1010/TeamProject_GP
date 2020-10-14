using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoginPopupController : MonoBehaviour
{
    private RectTransform rectTr;
    private Vector2 startPos;
    private Vector2 endPos = Vector2.zero;
    private Vector2 destination;

    [HideInInspector]
    public bool isButtonClicked = false;
    public float lerpSpeed = 5.0f;

    [SerializeField]
    private InputField[] inputFields = new InputField[2];

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

    public void ResetInputFields()
    {
        for (int i = 0; i < inputFields.Length; i++)
        {
            inputFields[i].text = "";
        }
    }
}
