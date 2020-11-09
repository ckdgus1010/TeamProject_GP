using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;


public class BoardSetting : MonoBehaviour
{
    public Slider boardSizeSlider;
    public Slider[] sliders = new Slider[2];
    public GameObject gameBoard;
    public GameObject guideCube;

    //Game Board 크기 설정
    private Vector3 originalBoardScale;
    private Vector3 originalGuideScale;

    //그리드 크기 설정
    public Slider gridSizeSlider;
    public GameObject[] gridArray = new GameObject[5];
    private int currGridSize;
    public GameObject currGrid;

    //Check Board 크기 설정
    public GameObject[] checkBoardArray = new GameObject[5];
    private int currCheckBoardSize;
    private GameObject currCheckBoard;

    public int modeID;

    [Header("Board Size Slider")]
    public Slider masterBoardSizeSlider;
    public Slider clientBoardSizeSlider;

    [SerializeField] private GameObject[] maps = new GameObject[3];
    public AudioClip[] mapSounds = new AudioClip[3];
    public SoundManager soundMgr;
    private void Start()
    {
        modeID = GameManager.Instance.modeID;
        originalBoardScale = gameBoard.transform.localScale;
        originalGuideScale = guideCube.transform.localScale;

        switch (modeID)
        {
            case 0:
                currGrid = gridArray[2];
                currGridSize = 0;

                currCheckBoard = checkBoardArray[2];
                currCheckBoardSize = 0;
                break;
            case 1:
                Debug.LogError("BoardSetting ::: modeID 확인 좀...");
                break;
            case 2:
            case 3:
            case 4:
                maps[modeID - 2].SetActive(true);
                soundMgr.bGM.clip = mapSounds[modeID - 2];
                soundMgr.bGM.Play();
                SetGrid();
                break;
            case 5:
            case 6:
                SetGrid();
                break;
            case 7:
                currGrid = gridArray[1];
                currGridSize = 0;

                currCheckBoard = checkBoardArray[1];
                currCheckBoardSize = 0;
                break;
            case 8:
                currGrid = gridArray[2];
                currGridSize = 0;

                currCheckBoard = checkBoardArray[2];
                currCheckBoardSize = 0;
                break;

            case 1000:
                currGrid = gridArray[0];
                currGridSize = 0;

                currCheckBoard = checkBoardArray[0];
                currCheckBoardSize = 0;
                break;
        }


        currGrid.SetActive(true);
        currCheckBoard.SetActive(true);
        BoardSize();
    }

    void SetGrid()
    {
        currGrid = gridArray[0];
        currGridSize = 0;

        currCheckBoard = checkBoardArray[0];
        currCheckBoardSize = 0;
    }

    public void BoardSize()
    {
        int _modeID = GameManager.Instance.modeID;

        switch (_modeID)
        {
            case 1:
            case 2:
                boardSizeSlider = sliders[0];
                break;
            case 3:
            case 4:
                boardSizeSlider = sliders[1];
                break;
            case 5:
            case 6:
            case 7:
            case 8:
                if (PhotonNetwork.IsMasterClient)
                {
                    boardSizeSlider = masterBoardSizeSlider;
                }
                else
                {
                    boardSizeSlider = clientBoardSizeSlider;
                }
                break;
        }

        float scaleFactor = boardSizeSlider.value;

        gameBoard.transform.localScale = originalBoardScale * scaleFactor;
        guideCube.transform.localScale = originalGuideScale * scaleFactor;

        //if (_modeID == 6 || _modeID == 7 || _modeID == 8)
        //{
        //    if (PhotonNetwork.IsMasterClient)
        //    {
        //        boardSizeSlider = masterBoardSizeSlider;
        //    }
        //    else
        //    {
        //        boardSizeSlider = clientBoardSizeSlider;
        //    }

        //    float scaleFactor = boardSizeSlider.value;

        //    gameBoard.transform.localScale = originalBoardScale * scaleFactor;
        //    guideCube.transform.localScale = originalGuideScale * scaleFactor;
        //}
        //else
        //{
        //    float scaleFactor = boardSizeSlider.value;

        //    gameBoard.transform.localScale = originalBoardScale * scaleFactor;
        //    guideCube.transform.localScale = originalGuideScale * scaleFactor;
        //}
    }

    public void GridSize()
    {
        if (currGridSize != (int)gridSizeSlider.value)
        {
            currGridSize = (int)gridSizeSlider.value;

            currGrid.SetActive(false);
            currGrid = null;
        }

        int gridSize = (int)gridSizeSlider.value - (int)gridSizeSlider.minValue;

        currGrid = gridArray[gridSize];
        currGrid.SetActive(true);

        Debug.Log($"BoardSetting ::: gridSize = {gridSize} // currGrid = {currGrid.gameObject.name}");
    }

    public void CheckBoardSize()
    {
        if (currCheckBoardSize != (int)gridSizeSlider.value)
        {
            currCheckBoardSize = (int)gridSizeSlider.value;

            currCheckBoard.SetActive(false);
            currCheckBoard = null;
        }

        int checkBoardSize = (int)gridSizeSlider.value - (int)gridSizeSlider.minValue;

        currCheckBoard = checkBoardArray[checkBoardSize];
        currCheckBoard.SetActive(true);
    }
}
