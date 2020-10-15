using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonSound : MonoBehaviour
{
    public AudioClip audioClrip;

    private AudioSource buttonSound;
    void Start()
    {
        buttonSound = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    public void ButtonClicked()
    {
        buttonSound.clip = audioClrip;
        buttonSound.Play();
    }
}
