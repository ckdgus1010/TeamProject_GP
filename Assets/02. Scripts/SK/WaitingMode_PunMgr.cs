using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class WaitingMode_PunMgr : MonoBehaviourPunCallbacks
{
    
    //public override void OnPlayerEnteredRoom(Player newPlayer)
    //{
    //    print("OnPlayerEnteredRoom " + newPlayer.ActorNumber);
    //    if (PhotonNetwork.IsMasterClient)
    //    {
    //        WatingButtonMgr.instance.AddPlayer(newPlayer.ActorNumber);
    //    }
    //}
    //public override void OnPlayerLeftRoom(Player otherPlayer)
    //{
    //    print("OnPlayerLeftRoom " + otherPlayer.ActorNumber);
    //    if (PhotonNetwork.IsMasterClient)
    //    {
    //        WatingButtonMgr.instance.RemovePlayer(otherPlayer.ActorNumber);
    //    }
    //}

}
