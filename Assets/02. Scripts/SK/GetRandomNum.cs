using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetRandomNum : MonoBehaviour
{
    private Random_TSF random_TSF;


    public void FindMyindex()
    {
       string myindex =  random_TSF.list[WatingButtonMgr.instance.myIndexNumber];
        print("GetRandomNum :::" + myindex);
    }
}
