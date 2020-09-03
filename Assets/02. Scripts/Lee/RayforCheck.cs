using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayforCheck : MonoBehaviour
{
    private MeshRenderer meshRenderer;

    public float rayDistance = 10.0f;
    public int count;

    void Start()
    {
        meshRenderer = GetComponent<MeshRenderer>();
        count = 0;
    }

    void Update()
    {
        Ray ray = new Ray(transform.position, -transform.forward);

        if (Physics.Raycast(ray, out RaycastHit hit, rayDistance, 1 << 8))
        {
            meshRenderer.material.color = Color.yellow;
            count = 1;
        }
        else
        {
            meshRenderer.material.color = Color.white;
            count = 0;
        }
    }
}
