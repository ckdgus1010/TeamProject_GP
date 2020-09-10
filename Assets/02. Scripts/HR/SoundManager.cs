using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    // 싱글톤 패턴을 사용하기 위한 인스턴스 변수
    private static SoundManager _instance;

    // 인스턴스에 접근하기 위한 프로퍼티
    public static SoundManager instance
    {
        get
        {
            // 인스턴스가 없는 경우에 접근하려 하면 인스턴스를 할당해준다.
            if (!_instance)
            {
                _instance = FindObjectOfType(typeof(SoundManager)) as SoundManager;

                if (_instance == null)
                    Debug.Log("SoundManager ::: no Singleton obj");
            }
            return _instance;
        }
    }

    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
        }
        // 인스턴스가 존재하는 경우 새로생기는 인스턴스를 삭제한다.
        else if (_instance != this)
        {
            Destroy(gameObject);
        }
        // 아래의 함수를 사용하여 씬이 전환되더라도 선언되었던 인스턴스가 파괴되지 않는다.
        DontDestroyOnLoad(gameObject);
    }


    //---------------------------------------------

    public bool canEffect = true;
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