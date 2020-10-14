using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;


public class RoomInButton : MonoBehaviour
{
    public Text roomButton_Text;

    public void SetInfo(string roomName, int currentPlayer, int maxPlayer)
    {
        gameObject.name = roomName;
        roomButton_Text.text = roomName + " (" + currentPlayer + " / " + maxPlayer + ")";
        if (currentPlayer == 0)
        {
            Destroy(gameObject);
        }
    }
    public void OnClickJoinRoom()
    {
        PhotonNetwork.JoinRoom(gameObject.name);
        Debug.Log("joinedRoom");
    }
    
   
}
