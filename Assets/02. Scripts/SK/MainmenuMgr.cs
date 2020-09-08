using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class MainmenuMgr : MonoBehaviour
{
    public Text nickName_Text;
    // Start is called before the first frame update
    void Start()
    {
        nickName_Text.text = Palyfab_Login.myPlayfabInfo;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
