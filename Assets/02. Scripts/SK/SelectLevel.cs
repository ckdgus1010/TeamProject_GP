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
    // Start is called before the first frame update
    void Start()
    {
        image = GetComponent<Image>();
        instance = this;
        // if (WatingButtonMgr.instance.curruntLevels == mLevel) SelectColor();
        //else UnSelectColor();
    }

    public void OnClick()
    {

        if (!PhotonNetwork.IsMasterClient) return;
        {
            //SelectColor();
            for (int i = 0; i < selectLevels.Length; i++)
            {
                WatingButtonMgr.instance.myPhotonView.RPC("RpcSendLevel", RpcTarget.AllBuffered, mLevel,i);
                if (selectLevels[i] != this)
                {
                    //selectLevels[i].UnSelectColor();
                    WatingButtonMgr.instance.myPhotonView.RPC("RpcUnSelectColor", RpcTarget.AllBuffered, i);
                }

            }
        }

    }
    public void SelectColor()
    {
        image.color = new Color(image.color.r, image.color.g, image.color.b, 1.0f);
    }

    public void UnSelectColor()
    {
        image.color = new Color(image.color.r, image.color.g, image.color.b, 0.3f);
    }
}
