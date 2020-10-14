using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SwipeMenu : MonoBehaviour
{
    public GameObject scrollbar;
    private float scroll_pos = 0;
    private float[] pos;
    private float distance;

    [Range(1,2)]
    public float maxScale = 1.0f;
    [Range(0.5f, 0.9f)]
    public float minScale = 0.8f;
    public float lerpSpeed = 0.1f;

    void Start()
    {
        pos = new float[transform.childCount];
        distance = 1f / (pos.Length - 1.0f);

        for (int i = 0; i < pos.Length; i++)
        {
            pos[i] = distance * i;
        }
    }

    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            scroll_pos = scrollbar.GetComponent<Scrollbar>().value;
        }
        else
        {
            for (int i = 0; i < pos.Length; i++)
            {
                if (scroll_pos < pos[i] + (distance / 2) && scroll_pos > pos[i] - (distance / 2))
                {
                    scrollbar.GetComponent<Scrollbar>().value = Mathf.Lerp(scrollbar.GetComponent<Scrollbar>().value, pos[i], lerpSpeed);
                    transform.GetChild(i).localScale = Vector3.Lerp(transform.GetChild(i).localScale, Vector3.one * maxScale, lerpSpeed);

                    for (int a = 0; a < pos.Length; a++)
                    {
                        if (a != i)
                        {
                            transform.GetChild(a).localScale = Vector3.Lerp(transform.GetChild(a).localScale, Vector3.one * minScale, lerpSpeed);
                        }
                    }
                }
            }
        }
    }
}
