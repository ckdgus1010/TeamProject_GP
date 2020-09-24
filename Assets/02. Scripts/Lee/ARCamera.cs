using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ARCamera : MonoBehaviour
{
    public GameObject blockImg;
    private SphereCollider sphereCollider;
    private Vector3 originScale; 

    public Slider boardSizeSlider;

    private void Start()
    {
        sphereCollider = GetComponent<SphereCollider>();
        originScale = sphereCollider.transform.localScale;
    }

    public void ColliderSize()
    {
        float scaleFactor = boardSizeSlider.value;

        sphereCollider.transform.localScale = originScale * scaleFactor;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("CUBE"))
        {
            Debug.Log("ARCamera ::: 큐브와 충돌");

            blockImg.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("CUBE"))
        {
            Debug.Log("ARCamera ::: 큐브밖으로 나옴 ");

            blockImg.SetActive(false);
        }
    }
}
