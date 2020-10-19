using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;
public class Profile : MonoBehaviourPun
{
    public Text nameText;
    public Image img_Ready;
    private bool isReady;
    private int masterArId;
   // public GameObject masterMark;
   // public GameObject clientMark;
   // public GameObject mineMark;
    // Start is called before the first frame update
    void Awake()
    {
        nameText = GetComponentInChildren<Text>();

        // img_Ready = GetComponentInChildren<Image>();
    }
    public void Start()
    {

        if (WatingButtonMgr.instance.myPhotonView.IsMine)
        {
            gameObject.tag = "MINEPROFILE";
        }

    }

    //public void SetInfo(string nickName)
    //{
       
    //    if (PhotonNetwork.IsMasterClient && photonView.IsMine)
    //    {
    //        nameText.text = nickName;
    //        if(nickName == PhotonNetwork.NickName)
    //        {
    //            masterMark.SetActive(true);
    //        }
    //        else
    //        {
    //            clientMark.SetActive(true);
    //        }
    //        //print(nameText.text);
    //    }
    //    else
    //    {
    //       clientMark.SetActive(true);
    //       nameText.text = nickName;

    //        if (nameText.text == PhotonNetwork.MasterClient.NickName)
    //        {
    //            mineMark.SetActive(true);
    //        }
    //    }
    //}
    //public void OnClickReady()
    //{
    //    Debug.Log(" OnClickReady");
    //    WatingButtonMgr.instance.myPhotonView.RPC("RpcSetReady", RpcTarget.AllBuffered, nameText.text, !isReady);

    //}

    public void ChangeReadyState(string nickName)
    {
        print("ChangeReadyState실행함");
        if (nameText.text != nickName) return;
        isReady = !isReady;
        //ready on -> 노랑색
        if (isReady == true)
        {
            img_Ready.color = Color.yellow;
            Debug.Log("isready is true");
            //WatingButtonMgr.instance.myPhotonView.RPC("RpcReadyCountUp", RpcTarget.MasterClient);
        }
        //ready off -> 하얀색
        else if (isReady == false)
        {
            img_Ready.color = Color.white;
            Debug.Log("isready is false");
            //WatingButtonMgr.instance.myPhotonView.RPC("RpcReadyCountDown", RpcTarget.MasterClient);

        }
    }
}
