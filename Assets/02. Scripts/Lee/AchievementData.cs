using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AchievementData : MonoBehaviour
{
    [SerializeField] private int achievementID = 0;
    [SerializeField] private GameObject blurredImage;

    void Update()
    {
        // GameManager로부터 업적 달성 여부 확인
        bool isAchieved = GameManager.Instance.achievement[achievementID - 1];

        // blurred image를 비활성화
        blurredImage.SetActive(!isAchieved);
    }
}
