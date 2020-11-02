using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using GoogleARCore;

public class TutorialTouchManager : MonoBehaviour
{
    public PlayHelperPopup playHelpPopup;

    [Header("AR Camera")]
    public Camera cam;
    public GameObject pointImage;
    public CubeSetting cubeSetting;

    [Header("기타 UI")]
    public GameObject playButtons;
    public GameObject cardButton;
    public CardBoardSetting cardBoardSetting;

    //Touch 횟수
    [HideInInspector] public int count = 0;

    //GameBoard
    [Header("Gameboard")]
    public GameObject gameBoard;
    public float height = 0.1f;
    public float depth = 1.0f;

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

        // UI를 터치한 경우 터치 무시
        if (EventSystem.current.IsPointerOverGameObject(touch.fingerId))
        {
            return;
        }

        if (count == 0
            && touch.phase == TouchPhase.Began
            && Frame.Raycast(touch.position.x, touch.position.y, raycastFilter, out TrackableHit hit))
        {
            Anchor anchor = hit.Trackable.CreateAnchor(hit.Pose);

            //GameBoard 생성
            gameBoard.SetActive(true);

            //GameBoard 위치 설정
            gameBoard.transform.position = anchor.transform.position;

            //GameBoard 방향 설정
            //gameBoard.transform.rotation = Quaternion.Euler(Vector3.forward);

            var rot = Quaternion.LookRotation(cam.transform.position - hit.Pose.position);
            gameBoard.transform.rotation = Quaternion.Euler(cam.transform.position.x, rot.eulerAngles.y, cam.transform.position.z);

            //기타 UI 조정
            playButtons.SetActive(true);
            playHelpPopup.ChangeHelpMessageText();

            pointImage.SetActive(true);
            cubeSetting.enabled = true;

            count = 1;
        }
    }

    public void SetOrigin()
    {
        pointImage.SetActive(false);
        cubeSetting.enabled = false;
        count = 0;
    }
}
