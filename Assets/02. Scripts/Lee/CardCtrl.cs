using Lee;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardCtrl : MonoBehaviour
{
    public Slider gridSizeSlider;
    public GameObject[] cardArray = new GameObject[5];

    private GameObject currCard;

    private int num;

    public Transform originPos;
    public Transform destination;

    private void Start()
    {
        currCard = cardArray[0];
        num = 0;
    }

    public void CardMove(bool isCardOn)
    {
        if (isCardOn == true)
        {
            transform.position = Vector3.Lerp(transform.position, destination.position, 1.0f);
        }
        else
        {
            transform.position = Vector3.Lerp(transform.position, originPos.position, 1.0f);
        }
    }

    public void CardSize()
    {
        if ((int)gridSizeSlider.value != num)
        {
            currCard.SetActive(false);
            currCard = null;

            num = (int)gridSizeSlider.value;
        }

        int cardSize = (int)gridSizeSlider.value - (int)gridSizeSlider.minValue;
        currCard = cardArray[cardSize];
        currCard.SetActive(true);
    }
}
