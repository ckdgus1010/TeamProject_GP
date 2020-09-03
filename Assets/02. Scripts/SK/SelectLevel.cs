using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class SelectLevel : MonoBehaviour
{
    public static SelectLevel instance;
    public Levels mLevel;
    // Start is called before the first frame update
    void Start()
    {
        instance = this;
    }

    public void OnClick()
    {
        
        if (!PhotonNetwork.IsMasterClient) return;

        WatingButtonMgr.instance.myPhotonView.RPC("RpcSendLevel", RpcTarget.AllBuffered, mLevel);

    }
}
