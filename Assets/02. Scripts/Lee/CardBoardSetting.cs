using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardBoardSetting : MonoBehaviour
{
    private int modeID;
    private int stageID;

    private GameObject currStageCard;

    public float lerpSpeed = 5.0f;
    private int touchCount;
    public bool isCardBoardOn;
    private RectTransform rectTr;
    private Vector2 startPos_CB;
    private Vector2 endPos_CB;

    public GameObject[] modeArray02 = new GameObject[9];
    public GameObject[] modeArray03 = new GameObject[9];

    private void Start()
    {
        touchCount = 0;
        isCardBoardOn = false;

        rectTr = GetComponent<RectTransform>();
        startPos_CB = rectTr.position;
        endPos_CB = startPos_CB + new Vector2(800, 0);
        ChangeCard();
    }
    private void Update()
    {
        //문제 카드 팝업창 위치 조절
        Vector2 posCB = isCardBoardOn ? endPos_CB : startPos_CB;
        rectTr.anchoredPosition = Vector2.Lerp(rectTr.anchoredPosition, posCB, Time.deltaTime * lerpSpeed);
    }

    public void ChangeCard()
    {
        modeID = GameManager.Instance.modeID;
        stageID = GameManager.Instance.stageID;

        if (currStageCard != null)
        {
            currStageCard = null;
        }

        if (modeID == 3)
        {
            currStageCard = modeArray02[stageID - 1];
            currStageCard.SetActive(true);
        }
        else if (modeID == 4)
        {
            currStageCard = modeArray03[stageID - 1];
            currStageCard.SetActive(true);
        }
    }

    public void ShowCard()
    {
        if (touchCount == 0)
        {
            isCardBoardOn = true;
            touchCount = 1;
        }
        else
        {
            isCardBoardOn = false;
            touchCount = 0;
        }
    }
}
