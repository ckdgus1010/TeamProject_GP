using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProfileImageData : MonoBehaviour
{
    [SerializeField] private int profileID = 1000;
    [SerializeField] private Image profileImage = null;
    [SerializeField] private ProfileUsername profileUsername;

    public void ShowProfileImage()
    {
        if (profileID == 0)
        {
            if (GameManager.Instance.profileImageNum == 1000)
            {
                Debug.Log("ProfileImageData ::: \n profileID = {profileID} // GameManager.Instance.profileImage 없음");
                profileImage.sprite = GameManager.Instance.profileImageList[0];
            }
            else
            {
                Debug.Log($"ProfileImageData ::: \n profileID = {profileID} // 프로필 사진 불러오기");

                int index = GameManager.Instance.profileImageNum;
                profileImage.sprite = GameManager.Instance.profileImageList[index];
            }

            profileUsername.ShowUsername();
        }
    }

    public void ChangeProfileImage()
    {
        Debug.Log($"ProfileData {profileID} ::: 프로필 사진 변경 {GameManager.Instance.profileImageList[profileID].name}");

        GameManager.Instance.profileImageNum = profileID;
    }
}
