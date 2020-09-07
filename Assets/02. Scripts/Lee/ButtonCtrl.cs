using Photon.Pun.UtilityScripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Lee
{
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

        public bool isCountQuest = false;
        public CheckBoardMgr checkBoardMgr;

        public GameObject card;
        public bool isCardOn = false;

        //GameBoard 초기화
        public void ResetGameBoard()
        {
            //GameBoard 초기화
            boardSetting.SetOrigin();

            //Guide Cube 및 Cube 관련 사항 초기화
            ResetCube();
            guideCube.SetActive(false);
            guideCube.transform.position = Vector3.zero;

            //Ray casting 비활성화
            pointImage.SetActive(false);
            cubeSetting.enabled = false;

            //게임 플레이 관련 panel 및 button 비활성화
            isCardOn = true;
            ShowCard();
            playButtons.SetActive(false);
        }

        //Grid Size 설정 완료
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
            if (cubeSetting.currCube != null)
            {
                Destroy(cubeSetting.currCube);
                Debug.Log("큐브 삭제");
            }
        }

        public void CheckAnswer()
        {
            //'유형1 - 개수 맞추기'인 경우
            if (isCountQuest)
            {

            }
            else
            {
                //'유형2, 유형3'인 경우
                checkBoardMgr.CheckingAnswer();
            }
        }

        public void ShowCard()
        {
            if (isCardOn == false)
            {
                Debug.Log("카드를 보여줍니다.");
                isCardOn = true;
            }
            else
            {
                Debug.Log("카드를 숨깁니다.");
                isCardOn = false;
            }
            card.SetActive(isCardOn);
        }
    }
}
