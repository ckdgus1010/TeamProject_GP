using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayforCheck : MonoBehaviour
{
    private MeshRenderer meshRenderer;

    [HideInInspector]
    public int count;

    public float rayDistance = 10.0f;

    void Start()
    {
        meshRenderer = GetComponent<MeshRenderer>();
        count = 0;
    }

    public void CheckingCube()
    {
        Ray ray = new Ray(transform.position, -transform.forward);

        //Ray를 쏴서 Cube를 감지
        //Cube를 감지하면 count = 1, 아니면 count = 0
        if (Physics.Raycast(ray, out RaycastHit hit, rayDistance, 1 << 8) && hit.collider.CompareTag("CUBE"))
        {
            Debug.Log("큐브 감지");
            meshRenderer.material.color = Color.yellow;
            count = 1;
        }
        else
        {
            Debug.Log("큐브 없음");
            meshRenderer.material.color = Color.white;
            count = 0;
        }
    }
}
