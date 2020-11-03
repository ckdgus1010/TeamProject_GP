using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AchievementData : MonoBehaviour
{
    [SerializeField] private int achievementID = 0;
    [SerializeField] private GameObject achievementImage;
    [SerializeField] private GameObject questionImage;

    [SerializeField] private AchievementData[] achievementArray = new AchievementData[9];

    public void UpdateAchievementStatus()
    {
        // Achievement Scroll View에서만 사용할 것
        if (achievementID == 1000)
        {
            for (int i = 0; i < achievementArray.Length; i++)
            {
                achievementArray[i].CheckAchievementStatus();
            }
        }
    }

    public void CheckAchievementStatus()
    {
        // AchievementManager 업적 달성 여부 확인
        bool isAchieved = AchievementManager.Instance.achievement[achievementID - 1];

        achievementImage.SetActive(isAchieved);
        questionImage.SetActive(!isAchieved);
    }
}
