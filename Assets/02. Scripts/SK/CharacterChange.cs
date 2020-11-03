using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterChange : MonoBehaviour
{
    public Sprite[] charArry;
    Image image;
    private void Start()
    {
         image = GetComponent<Image>();
    }
    public void ChangeCharacter()
    {
        int randomchar = Random.Range(0, 7);
        image.sprite = charArry[randomchar];
    }
}
