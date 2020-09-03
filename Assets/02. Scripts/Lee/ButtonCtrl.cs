using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonCtrl : MonoBehaviour
{
    public BoardSetting boardSetting;

    public GameObject gridSettingPanel;
    public GameObject playButtons;

    public GameObject pointImage;

    public CubeSetting cubeSetting;
    public GameObject guideCube;
    public GameObject cubePrefab;
    public GameObject cubeList;
    public List<GameObject> list = new List<GameObject>();

    public CheckAnswerMgr checkAnswerMgr;

    public void ResetGameBoard()
    {
        boardSetting.SetOrigin();
        ResetCube();

        guideCube.SetActive(false);
        guideCube.transform.position = Vector3.zero;

        playButtons.SetActive(false);
        pointImage.SetActive(false);
        cubeSetting.enabled = false;
    }

    public void SetGridSize()
    {
        gridSettingPanel.SetActive(false);

        pointImage.SetActive(true);
        cubeSetting.enabled = true;
        playButtons.SetActive(true);
    }

    public void MakeCube()
    {
        if (cubeSetting.isGuideOn)
        {
            GameObject cube = Instantiate(cubePrefab
                                        , guideCube.transform.position
                                        , guideCube.transform.rotation
                                        , cubeList.transform);
            list.Add(cube);
            Debug.Log("큐브 생성");
        }
    }

    public void ResetCube()
    {
        for (int i = 0; i < list.Count; i++)
        {
            Destroy(list[i].gameObject);
        }
        list.Clear();
        Debug.Log("큐브 리셋");
    }

    public void DeleteCube()
    {
        Destroy(cubeSetting.currCube);
        Debug.Log("큐브 삭제");
    }

    public void CheckAnswer()
    {
        Debug.Log("정답 확인");
        checkAnswerMgr.CheckingAnswer();
    }
}
