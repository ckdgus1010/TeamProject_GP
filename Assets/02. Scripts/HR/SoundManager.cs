using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;

    bool canEffect = true;
    public AudioSource effectSound;
    public AudioClip [] effectSoundClips;
    public int esindex = 0;

    bool canBGM = true;
    public AudioSource bGM;
    public AudioClip [] bGMClips;
    public int mapindex = 0; //0, 1, 2, 3, 4 총 5개의 맵에 따라 곡3개가 랜덤하게 재생됨


    // BGM 을 버튼을 누르면 BGM 전체 소리가 꺼진다.
    // EffectSound 버튼을 누르면 EffectSound 전체 소리가 꺼진다.

    // Bgm 은 맵에 따라 랜덤하게 3곡씩 나온다

    void Awake() 
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void EnableBGM()
    {
        canBGM = !canBGM;
        if(canBGM == false)
        {
            StopBGM();
        }
    }


    public bool CanEffect()
    {
        return canEffect;
    }

    public bool CanBGM()
    {
        return canBGM;
    }

    public void EnableEffect()
    {
        canEffect = !canEffect;
    }


    public void StopBGM() // BGM 끄는 함수
    {
        bGM.Stop();
    }

    public void RandomPlay() //BGM 랜덤 플레이, 켜는 함수 
    {
        if (canBGM == true)
        {
            int random = (3 * mapindex) + Random.Range(0, 3);     
            bGM.clip = bGMClips[random];
            print(random);
            bGM.Play();
        }
    }

    public void EffefctPlay(int index)
    {     
        if (canEffect == false) return; // 위에 동일

        effectSound.PlayOneShot(effectSoundClips[index]);
    }

}