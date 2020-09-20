using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class BoardSetting : MonoBehaviour
{
    public Slider boardSizeSlider;
    public GameObject gameBoard;
    public GameObject guideCube;

    //Game Board 크기 설정
    private Vector3 originalBoardScale;
    private Vector3 originalGuideScale;

    //그리드 크기 설정
    public Slider gridSizeSlider;
    public GameObject[] gridArray = new GameObject[5];
    private int currGridSize;
    private GameObject currGrid;

    //Check Board 크기 설정
    public GameObject[] checkBoardArray = new GameObject[5];
    private int currCheckBoardSize;
    private GameObject currCheckBoard;

    private void Start()
    {
        originalBoardScale = gameBoard.transform.localScale;
        originalGuideScale = guideCube.transform.localScale;

        currGrid = gridArray[0];
        currGridSize = 0;

        currCheckBoard = checkBoardArray[0];
        currCheckBoardSize = 0;

        BoardSize();
        GridSize();
        CheckBoardSize();
    }

    public void BoardSize()
    {
        float scaleFactor = boardSizeSlider.value;

        gameBoard.transform.localScale = originalBoardScale * scaleFactor;
        guideCube.transform.localScale = originalGuideScale * scaleFactor;
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
