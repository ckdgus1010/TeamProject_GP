using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeSetting : MonoBehaviour
{
    public float rayDistance = 10.0f;

    public GameObject gameBoard;

    public GameObject guideCube;
    [HideInInspector]
    public bool isGuideOn;

    [HideInInspector]
    public GameObject currCube;

    private void Start()
    {
        isGuideOn = false;
        currCube = null;
    }

    void FixedUpdate()
    {
        Ray ray = new Ray(transform.position, transform.forward);

        if (Physics.Raycast(ray, out RaycastHit hit, rayDistance, 1 << 8))
        {
            //Cube를 감지했을 때
            if (hit.collider.CompareTag("CUBE"))
            {
                GuideCubeOff();

                GameObject hitcube = hit.collider.gameObject;

                if (hitcube != currCube && currCube != null)
                {
                    currCube.GetComponent<MeshRenderer>().material.color = Color.white;
                }

                currCube = hitcube;
                currCube.GetComponent<MeshRenderer>().material.color = Color.yellow;

                Vector3 normalVec = hit.normal;

                //윗면만 감지할 경우 - 모든 면을 감지할 경우는 주석처리 해야 함
                if (normalVec == currCube.transform.up)
                {
                    Transform objTr = currCube.transform.GetChild(0).transform;
                    GuideCubeOn(objTr);
                }

                ////모든 면을 감지할 경우 - 윗면만 감지할 경우는 주석처리 해야 함
                //AllSideDetection(normalVec);
            }
            else
            {
                //Grid를 감지했을 때
                if (currCube != null)
                {
                    currCube.GetComponent<MeshRenderer>().material.color = Color.white;
                    currCube = null;
                }

                Transform pos = hit.collider.transform.Find("CubePos").transform;
                GuideCubeOn(pos);
            }
        }
        else
        {
            if (currCube != null)
            {
                currCube.GetComponent<MeshRenderer>().material.color = Color.white;
                currCube = null;
            }

            GuideCubeOff();
        }
    }

    void GuideCubeOn(Transform _pos)
    {
        if (GameManager.Instance.modeID == 2)
        {
            return;
        }

        guideCube.SetActive(true);
        guideCube.transform.position = _pos.position;
        guideCube.transform.rotation = gameBoard.transform.rotation;

        isGuideOn = true;
    }

    public void GuideCubeOff()
    {
        guideCube.transform.position = Vector3.zero;
        guideCube.SetActive(false);

        isGuideOn = false;
    }
}
