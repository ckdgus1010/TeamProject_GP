using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.UI;

public class SelectLevel : MonoBehaviour
{
    public static SelectLevel instance;
    public Levels mLevel;
    public SelectLevel[] selectLevels;
    private Image image;
    // public bool levelCilck;
    // Start is called before the first frame update
    void Start()
    {
        //levelCilck = false;
        image = GetComponent<Image>();
        instance = this;
    }

    public void OnClick()
    {
        //print(levelCilck);
        if (!PhotonNetwork.IsMasterClient)
        {
            Debug.Log("넌 클라이언트 잖아 돌아가");
            return;
        }

        // 마스터만 난이도 선택이 가능하다
        if (PhotonNetwork.IsMasterClient) 
        {
            //SelectColor();
            for (int i = 0; i < selectLevels.Length  ; i++)
            {
                WatingButtonMgr.instance.myPhotonView.RPC("RpcSendLevel", RpcTarget.AllViaServer, mLevel,i);
                Debug.Log("SelectLevel ::: Before if문");

                if (selectLevels[i] != this)
                {
                    //selectLevels[i].UnSelectColor();
                    Debug.Log($"SelectLevel ::: i = {i} ");
                    WatingButtonMgr.instance.myPhotonView.RPC("RpcUnSelectColor", RpcTarget.AllBuffered, i);
                }
            }
        }
       // levelCilck = true;
       // print(levelCilck);

    }
    public void SelectColor()
    {
        print("선택색");
        if(WatingButtonMgr.instance.curruntLevels == mLevel)
        image.color = new Color(image.color.r, image.color.g, image.color.b, 1.0f);
    }

    public void UnSelectColor()
    {
        image.color = new Color(image.color.r, image.color.g, image.color.b, 0.3f);
    }
}
