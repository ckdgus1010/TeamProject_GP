using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using GoogleARCore;

public class BoardSetting : MonoBehaviour
{
    private Camera cam;

    public GameObject gameBoard;
    public GameObject guideCube;
    public GameObject gridSettingPanel;
    public GameObject boardSetting;

    //Touch 횟수
    [HideInInspector]
    public int count;

    //GameBoard 위치 보정
    public float height = 0.1f;

    //GameBoard 및 GuideCube 크기 설정
    public Slider boardSizeSlider;
    private Vector3 originalBoardScale;
    private Vector3 originalGuideScale;

    //Grid 크기 설정
    public Slider gridSizeSlider;
    public GameObject[] gridArray = new GameObject[5];
    private int num;
    private GameObject currGrid;

    void Start()
    {
        cam = GetComponent<Camera>();

        originalBoardScale = gameBoard.transform.localScale;
        originalGuideScale = guideCube.transform.localScale;

        count = 0;

        num = 0;
        currGrid = gridArray[0];
    }

    void Update()
    {
        if (count == 1 || Input.touchCount == 0)
        {
            return;
        }

        Touch touch = Input.GetTouch(0);
        TrackableHitFlags raycastFilter = TrackableHitFlags.PlaneWithinPolygon | TrackableHitFlags.FeaturePointWithSurfaceNormal;

        if (count == 0 && touch.phase == TouchPhase.Began && Frame.Raycast(touch.position.x, touch.position.y, raycastFilter, out TrackableHit hit))
        {
            var anchor = hit.Trackable.CreateAnchor(hit.Pose);

            //GameBoard 크기 설정 슬라이더 활성화
            boardSetting.SetActive(true);

            //GameBoard 생성
            gameBoard.SetActive(true);
            BoardSize();

            //GameBoard 위치 설정
            gameBoard.transform.position = anchor.transform.position;

            //GameBoard 방향 설정
            gameBoard.transform.rotation = Quaternion.Euler(Vector3.forward);

            //var rot = Quaternion.LookRotation(cam.transform.position - hit.Pose.position);
            //gameBoard.transform.rotation = Quaternion.Euler(cam.transform.position.x, rot.eulerAngles.y, cam.transform.position.z);

            //Grid 크기 조절 패널 활성화
            gridSettingPanel.SetActive(true);

            count += 1;
        }
    }

    public void SetOrigin()
    {
        boardSizeSlider.value = 0.1f;
        boardSetting.SetActive(false);

        gameBoard.SetActive(false);
        BoardSize();

        gridSettingPanel.SetActive(false);
        gridSizeSlider.value = gridSizeSlider.minValue;

        count = 0;
    }

    public void BoardSize()
    {
        float scaleFactor = boardSizeSlider.value;

        gameBoard.transform.localScale = originalBoardScale * scaleFactor;
        guideCube.transform.localScale = originalGuideScale * scaleFactor;
    }

    public void GridSize()
    {
        if ((int)gridSizeSlider.value != num)
        {
            currGrid.SetActive(false);
            currGrid = null;

            num = (int)gridSizeSlider.value;
        }

        int gridSize = (int)gridSizeSlider.value - (int)gridSizeSlider.minValue;
        currGrid = gridArray[gridSize];
        currGrid.SetActive(true);
    }
}
