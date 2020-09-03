using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;
public class Profile : MonoBehaviourPun
{
    Text nameText;
    public Image img_Ready;
    private bool isReady;
    private int masterArId;
  
    // Start is called before the first frame update
    void Awake()
    {
        nameText = GetComponentInChildren<Text>();

        // img_Ready = GetComponentInChildren<Image>();
    }
    public void SetInfo(string nickName)
    {
        nameText.text = nickName;
    }
    public void OnClickReady()
    {
        Debug.Log(" OnClickReady");

            WatingButtonMgr.instance.myPhotonView.RPC("RpcSetReady", RpcTarget.AllBuffered, nameText.text, !isReady);

    }

    public void ChangeReadyState(string nickName, bool ready)
    {
        if (nameText.text != nickName) return;
        isReady = ready;
        //ready on -> 노랑색
        if (isReady == true)
        {
            WatingButtonMgr.instance.readyCount += 1;
            img_Ready.color = Color.yellow;
            Debug.Log("isready is true");
        }
        //ready off -> 하얀색
        else if (isReady == false)
        {
            WatingButtonMgr.instance.readyCount -= 1;
            img_Ready.color = Color.white;
            Debug.Log("isready is false");
        }
    }
}
