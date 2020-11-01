using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProfileImageData : MonoBehaviour
{
    [SerializeField] private int profileID = 1000;
    [SerializeField] private Image profileImage = null;

    public void ShowProfileImage()
    {
        if (profileID == 0)
        {
            if (GameManager.Instance.profileImage == null)
            {
                Debug.Log("ProfileImageData ::: \n profileID = {profileID} // GameManager.Instance.profileImage 없음");
                profileImage.sprite = GameManager.Instance.profileImageList[0];
            }
            else
            {
                Debug.Log($"ProfileImageData ::: \n profileID = {profileID} // 프로필 사진 불러오기");
                profileImage.sprite = GameManager.Instance.profileImage;
            }
        }
    }

    public void ChangeProfileImage()
    {
        Debug.Log($"ProfileData {profileID} ::: 프로필 사진 변경 {GameManager.Instance.profileImageList[profileID].name}");
        GameManager.Instance.profileImage = GameManager.Instance.profileImageList[profileID];
    }
}
