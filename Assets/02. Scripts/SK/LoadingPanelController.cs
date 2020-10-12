using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadingPanelController : MonoBehaviour
{
    public GameObject loadingPanel;
    private Animator loadingPanelImg;

    private void Start()
    {
        loadingPanelImg = loadingPanel.GetComponent<Animator>();
    }

    public void FadeIn()
    {
        Debug.Log("FadeIn");
        loadingPanelImg.SetBool("FadeOut", true);
        PanelOff();
    }

    public void FadeOut()
    {
        Debug.Log("FadeOut");

        loadingPanelImg.SetBool("FadeIn", true);
        Invoke("PanelOff",1);
    }

    public void PanelOff()
    {
        loadingPanel.SetActive(false);
    }
}
