using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Lee
{
    public class AnswerMgr : MonoBehaviour
    {
        public Slider gridSizeSlider;
        public Text inputFieldText;
        public GameObject oPanel;
        public GameObject xPanel;

        public int[] countQuestAnswer = new int[] { 1, 3, 5, 7, 9 };
        public int stageId;

        public List<int> forwardAnswerList;     //앞면
        public List<int> sideAnswerList;        //옆면
        public List<int> topAnswerList;         //윗면

        public void AnswerListSize()
        {
            int gridSize = (int)gridSizeSlider.value - (int)gridSizeSlider.minValue;

            switch (gridSize)
            {
                case 0:
                    Size3();
                    break;
                case 1:
                    Size4();
                    break;
                case 2:
                    Size5();
                    break;
                case 3:
                    Size6();
                    break;
                case 4:
                    Size7();
                    break;
                default:
                    Size3();
                    break;
            }
        }

        void Size3()
        {
            Debug.Log("AnswerList의 크기는 3x3 입니다.");
            forwardAnswerList = new List<int> { 0, 0, 0
                                              , 0, 0, 1
                                              , 0, 1, 1 };

            sideAnswerList = new List<int>    { 0, 0, 0
                                              , 0, 0, 1
                                              , 0, 1, 1 };

            topAnswerList = new List<int>     { 0, 1, 1
                                              , 0, 0, 1
                                              , 0, 0, 0 };
        }

        void Size4()
        {
            Debug.Log("AnswerList의 크기는 4x4 입니다.");
            forwardAnswerList = new List<int> { 0, 0, 0, 0
                                              , 0, 1, 0, 0
                                              , 1, 1, 1, 0
                                              , 1, 1, 1, 1 };

            sideAnswerList = new List<int>    { 0, 0, 0, 0
                                              , 0, 0, 1, 0
                                              , 0, 1, 1, 0
                                              , 0, 1, 1, 0 };

            topAnswerList = new List<int>     { 0, 0, 0, 0
                                              , 1, 1, 1, 0
                                              , 1, 1, 1, 1
                                              , 0, 0, 0, 0 };
        }

        void Size5()
        {
            Debug.Log("AnswerList의 크기는 5x5 입니다.");
            forwardAnswerList = new List<int> { 0, 0, 0, 0, 0
                                              , 0, 0, 1, 0, 0
                                              , 0, 0, 1, 0, 0
                                              , 1, 0, 1, 0, 0
                                              , 1, 1, 1, 1, 0 };

            sideAnswerList = new List<int>    { 0, 0, 0, 0, 0
                                              , 0, 0, 0, 0, 1
                                              , 0, 0, 1, 0, 1
                                              , 1, 0, 1, 0, 1
                                              , 1, 1, 1, 1, 1 };

            topAnswerList = new List<int>     { 0, 0, 1, 1, 0
                                              , 1, 0, 0, 1, 0
                                              , 1, 1, 1, 1, 0
                                              , 1, 0, 0, 1, 0
                                              , 1, 0, 0, 0, 0 };
        }

        void Size6()
        {
            Debug.Log("AnswerList의 크기는 6x6 입니다.");
            forwardAnswerList = new List<int> { 0, 0, 0, 0, 0, 0
                                              , 0, 0, 0, 0, 0, 0
                                              , 0, 0, 0, 1, 0, 0
                                              , 1, 0, 0, 1, 0, 0
                                              , 1, 0, 1, 1, 0, 0
                                              , 1, 1, 1, 1, 1, 1 };

            sideAnswerList = new List<int>    { 0, 0, 0, 0, 0, 0
                                              , 0, 0, 0, 0, 0, 0
                                              , 0, 0, 0, 0, 1, 0
                                              , 0, 0, 1, 0, 1, 0
                                              , 1, 0, 1, 0, 1, 0
                                              , 1, 1, 1, 1, 1, 1 };

            topAnswerList = new List<int>     { 0, 0, 0, 1, 0, 0
                                              , 0, 1, 0, 1, 0, 0
                                              , 0, 1, 1, 1, 1, 1
                                              , 1, 1, 1, 1, 0, 0
                                              , 1, 0, 1, 1, 1, 0
                                              , 1, 0, 0, 0, 0, 0 };
        }

        void Size7()
        {
            Debug.Log("AnswerList의 크기는 7x7 입니다.");
            forwardAnswerList = new List<int> { 0, 0, 0, 0, 0, 0, 0
                                              , 0, 0, 0, 0, 0, 0, 0
                                              , 0, 0, 0, 0, 0, 0, 0
                                              , 0, 0, 0, 0, 0, 0, 0
                                              , 0, 0, 1, 0, 1, 0, 0
                                              , 0, 0, 1, 1, 1, 0, 0
                                              , 0, 0, 1, 1, 1, 0, 0 };

            sideAnswerList = new List<int>    { 0, 0, 0, 0, 0, 0, 0
                                              , 0, 0, 0, 0, 0, 0, 0
                                              , 0, 0, 0, 0, 0, 0, 0
                                              , 0, 0, 0, 0, 0, 0, 0
                                              , 0, 0, 0, 1, 0, 0, 0
                                              , 0, 1, 0, 1, 1, 1, 0
                                              , 1, 1, 1, 1, 1, 1, 1 };

            topAnswerList = new List<int>     { 0, 0, 1, 0, 0, 0, 0
                                              , 0, 0, 1, 1, 0, 0, 0
                                              , 0, 0, 1, 1, 1, 0, 0
                                              , 0, 0, 1, 1, 1, 0, 0
                                              , 0, 0, 1, 1, 1, 0, 0
                                              , 0, 0, 1, 1, 0, 0, 0
                                              , 0, 0, 1, 0, 0, 0, 0 };
        }
    }
}
