using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SignupController : MonoBehaviour
{
    private RectTransform rectTr;
    private Vector2 startPos;
    private Vector2 endPos = Vector2.zero;
    private Vector2 destination;

    public bool isButtonClicked = false;
    public float lerpSpeed = 3.0f;

    [SerializeField]
    private InputField[] inputFields = new InputField[3];

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

    // 회원가입 패널을 숨길 때 ID / PW / Nickname 입력란 초기화
    public void ResetInputFields()
    {
        for (int i = 0; i < inputFields.Length; i++)
        {
            inputFields[i].text = "";
        }
    }
}
