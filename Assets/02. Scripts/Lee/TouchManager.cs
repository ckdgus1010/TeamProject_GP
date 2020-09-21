using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleARCore;


//목표: 사용자가 touch한 지점에 GameBoard 생성

public class TouchManager : MonoBehaviour
{
    public Camera cam;

    public GameObject gameBoard;

    public GameObject pointImage;
    public CubeSetting cubeSetting;
    public GameObject gridSizePanel;
    public GameObject boardSizePanel;
    public GameObject blockImg;

    //Touch 횟수
    [HideInInspector]
    public int count = 0;

    //GameBoard 위치 보정
    public float height = 0.1f;
    public float depth = 0.5f;

    void Start()
    {
        count = 0;
    }

    void Update()
    {
        if (count == 1 || Input.touchCount == 0)
        {
            return;
        }

        Touch touch = Input.GetTouch(0);
        TrackableHitFlags raycastFilter = TrackableHitFlags.PlaneWithinPolygon
                                          | TrackableHitFlags.FeaturePointWithSurfaceNormal;

        if (count == 0 && touch.phase == TouchPhase.Began && Frame.Raycast(touch.position.x, touch.position.y, raycastFilter, out TrackableHit hit))
        {
            var anchor = hit.Trackable.CreateAnchor(hit.Pose);

            //GameBoard 생성
            gameBoard.SetActive(true);

            //GameBoard 위치 설정
            gameBoard.transform.position = anchor.transform.position;

            //GameBoard 방향 설정
            //gameBoard.transform.rotation = Quaternion.Euler(Vector3.forward);

            var rot = Quaternion.LookRotation(cam.transform.position - hit.Pose.position);
            gameBoard.transform.rotation = Quaternion.Euler(cam.transform.position.x, rot.eulerAngles.y, cam.transform.position.z);

            pointImage.SetActive(true);
            cubeSetting.enabled = true;
            boardSizePanel.SetActive(true);

            if (GameManager.Instance.modeID == 0 || GameManager.Instance.modeID == 5)
            {
                gridSizePanel.SetActive(true);
                blockImg.SetActive(true);
            }

            count = 1;
        }
    }

    public void SetOrigin()
    {
        gameBoard.SetActive(false);
        gameBoard.transform.position = Vector3.zero;

        pointImage.SetActive(false);
        cubeSetting.enabled = false;
        gridSizePanel.SetActive(false);
        boardSizePanel.SetActive(false);

        count = 0;
    }
}
