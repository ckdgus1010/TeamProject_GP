using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

// 가능하면 Singleton으로 처리할 것
public class AndroidPlugin : MonoBehaviour
{
    private AndroidJavaObject Kotlin;
    private (Texture2D _tex, Sprite _spr) Callback = (null, null);

    // 갤러리에서 불러온 이미지를 표시할 object
    public Image img;

    private void Awake()
    {
        Kotlin = new AndroidJavaObject("com.on_off.myapplication.unityGallery");
    }

    public void GalleryOpen()
    {
        Debug.Log("AndroidPlugin ::: 갤러리 오픈");
        StartCoroutine(ShowGallery(
            (_tex, _spr) => { img.sprite = _spr; }
            ));
    }

    public IEnumerator ShowGallery(UnityAction<Texture2D, Sprite> val)
    {
        Kotlin.Call("Open");
        yield return new WaitUntil(() => Callback._tex != null && Callback._spr != null);
        val(Callback._tex, Callback._spr);
        Callback = (null, null);
    }

    private void getImage(string path)
    {
        var www = new WWW($"file://{path}");
        var _tex = www.texture;
        var _spr = Sprite.Create(_tex, new Rect(0f, 0f, _tex.width, _tex.height), new Vector2(0.5f, 0.5f), 100);

        Callback = (_tex, _spr);
    }
}
