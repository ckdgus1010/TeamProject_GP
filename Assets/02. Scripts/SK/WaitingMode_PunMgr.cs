using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;

public class WaitingMode_PunMgr : MonoBehaviourPunCallbacks
{
    public Text gameStart_Ready;

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        print("WaitingMode_PunMgr  :: OnPlayerEnteredRoom " + newPlayer.ActorNumber);
        if (PhotonNetwork.IsMasterClient)
        {
            gameStart_Ready.text = "Game Start";
            print("WatingButtonMgr.AddPlayer 실행해");
            WatingButtonMgr.instance.AddPlayer(newPlayer.ActorNumber);
        }
    }
    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        print("WaitingMode_PunMgr ::  OnPlayerLeftRoom " + otherPlayer.ActorNumber);
        if (PhotonNetwork.IsMasterClient)
        {
            gameStart_Ready.text = "Game Start";
            print("WatingButtonMgr.RemovePlayer 실행해");
            WatingButtonMgr.instance.RemovePlayer(otherPlayer.ActorNumber);
        }
    }

}
