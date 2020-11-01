using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;


public class RoomInButton : MonoBehaviour
{
    //public Text roomButton_Text;

    public Text roomName;
    public Text playerNumber;
    [SerializeField]
    private Image roomImage;
    [SerializeField]
    private Sprite[] sprites = new Sprite[2];

    public void SetInfo(string _roomName, int currentPlayer, int maxPlayer)
    {
        gameObject.name = _roomName;

        roomName.text = _roomName;
        playerNumber.text = currentPlayer + " / " + maxPlayer + " 명";
        roomImage.sprite = sprites[maxPlayer - 2];

        //roomButton_Text.text = roomName + " (" + currentPlayer + " / " + maxPlayer + ")";

        if (currentPlayer == 0)
        {
            Destroy(gameObject);
        }
    }

    public void OnClickJoinRoom()
    {
        PhotonNetwork.JoinRoom(gameObject.name);
        Debug.Log("joinedRoom");
        //여기에도 로딩패널 켜주세요!
    }
}
