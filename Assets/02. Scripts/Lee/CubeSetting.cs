using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CubeSetting : MonoBehaviour
{
    public GameObject guideCube;
    public GameObject gameBoard;
    public Slider boardSizeSlider;
    private float rayDistance = 20.0f;

    [HideInInspector]
    public bool isGuideOn;
    [HideInInspector]
    public GameObject currCube;

    private SphereCollider coll;
    private Vector3 collScale;
    public Button[] playButtons = new Button[4];

    private void Start()
    {
        coll = GetComponent<SphereCollider>();
        collScale = coll.transform.localScale;

        isGuideOn = false;
        currCube = null;
    }

    void FixedUpdate()
    {
        coll.transform.localScale = collScale * boardSizeSlider.value;

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

                //윗면만 감지할 경우
                if (normalVec == currCube.transform.up)
                {
                    Transform objTr = currCube.transform.GetChild(0).transform;
                    GuideCubeOn(objTr);
                }

                ////모든 면을 감지할 경우
                //AllSideDetection(normalVec);
            }
            else
            {
                //Grid를 감지했을 때
                if (currCube != null)
                {
                    currCube.GetComponent<MeshRenderer>().material.color = Color.white;
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
            }

            GuideCubeOff();
        }
    }

    void GuideCubeOn(Transform _pos)
    {
        guideCube.SetActive(true);
        guideCube.transform.position = _pos.position;
        guideCube.transform.rotation = gameBoard.transform.rotation;

        isGuideOn = true;
    }

    void GuideCubeOff()
    {
        guideCube.transform.position = Vector3.zero;
        guideCube.SetActive(false);

        isGuideOn = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("CUBE"))
        {
            Debug.Log("씨발");
            for (int i = 0; i < playButtons.Length; i++)
            {
                playButtons[i].GetComponent<Button>().enabled = false;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("CUBE"))
        {
            Debug.Log("꺼져");
            for (int i = 0; i < playButtons.Length; i++)
            {
                playButtons[i].GetComponent<Button>().enabled = true;
            }
        }
    }

    void AllSideDetection(Vector3 _normalVec)
    {
        GameObject obj = null;

        if (_normalVec == currCube.transform.up)
        {
            obj = currCube.transform.GetChild(0).gameObject;
        }
        else if (_normalVec == currCube.transform.forward)
        {
            obj = currCube.transform.GetChild(1).gameObject;
        }
        else if (_normalVec == -currCube.transform.right)
        {
            obj = currCube.transform.GetChild(4).gameObject;
        }
        else if (_normalVec == currCube.transform.right)
        {
            obj = currCube.transform.GetChild(3).gameObject;
        }
        else if (_normalVec == -currCube.transform.forward)
        {
            obj = currCube.transform.GetChild(2).gameObject;
        }

        GuideCubeOn(obj.transform);
    }
}
