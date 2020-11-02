using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StageData : MonoBehaviour
{
    public int stageID = 0;
    private int status;
    private int stageStatus;

    [SerializeField]
    private Image image;
    [SerializeField]
    private GameObject profileImage;

    ////나중에 Start 함수로 바꿀 것
    //private void Start()
    //{
    //    List<GameManager.StageState> stageStateList = GameManager.Instance.stageStateList;

    //    //Stage Clear 여부 확인
    //    status = (int)stageStateList[stageID - 1];

    //    Image img = GetComponent<Image>();
    //    //img.color = GameManager.Instance.buttonColor[status];
    //    img.sprite = GameManager.Instance.buttonImageArray[status];
    //}

    public void ChangeButtonImage()
    {
        // Stage 상태 받아오기
        stageStatus = (int)GameManager.Instance.stageStateList[stageID - 1];
        Debug.Log($"{this.gameObject.name} ::: {stageStatus}");

        if (stageStatus == (int)GameManager.StageState.Current)
        {
            Image _image = profileImage.GetComponent<Image>();
            if (GameManager.Instance.profileImage == null)
            {
                _image.sprite = GameManager.Instance.profileImageList[0];
            }
            else
            {
                _image.sprite = GameManager.Instance.profileImage;
            }

            profileImage.SetActive(true);
        }
        else
        {
            profileImage.SetActive(false);
        }

        // 알맞은 Sprite로 교체
        image.sprite = GameManager.Instance.buttonImageArray[stageStatus];
    }

    public void UpdateStageData()
    {
        List<GameManager.StageState> stageStateList = GameManager.Instance.stageStateList;

        //Stage Clear 여부 확인
        status = (int)stageStateList[stageID - 1];

        Image img = GetComponent<Image>();
        img.color = GameManager.Instance.buttonColorArray[status];
        //img.sprite = GameManager.Instance.buttonImageArray[status];
    }

    public void ConvertScene()
    {
        if (stageStatus != (int)GameManager.StageState.Forbidden)
        {
            //GameManager에게 정보 전달
            GameManager.Instance.stageID = stageID;
            LoadingSceneController.Instance.LoadScene("14. Play Scene");
            //SceneManager.LoadScene("14. Play Scene");
        }
        else
        {
            Debug.Log($"{gameObject.name} ::: 이전 단계를 먼저 클리어하세요.");
        }
    }
}
