using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Use : MonoBehaviour
{
    [SerializeField] private AndroidPlugin androidPlugin;
    [SerializeField] private Image image;
    [SerializeField] private ProfileImageData profileButtonImage;

    public void GalleryOpen()
    {
        Debug.Log("Use ::: 갤러리 오픈");
        StartCoroutine(androidPlugin.ShowGallery(
            (_tex, _spr) => { image.sprite = _spr; }
            ));
        Debug.Log("Use ::: 프로필 사진 변경");

        GameManager.Instance.profileImage = image.sprite;
        profileButtonImage.ShowProfileImage();
        Debug.Log("Use ::: 프로필 버튼 사진 변경");
    }
}
