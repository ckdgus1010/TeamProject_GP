using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnswerDataEasy : MonoBehaviour
{
    //3*3
    public int stageID;

    public List<int>[] answerArray;

    public List<int> frontAnswerList;
    public List<int> sideAnswerList;
    public List<int> topAnswerList;

    public List<int>[] ChooseAnswerList(int _stageID)
    {
        switch (_stageID)
        {
            case 1:
            case 7:
                Stage01();
                break;
            case 2:
            case 8:
                Stage02();
                break;
            case 3:
            case 9:
                Stage03();
                break;
            case 4:
                Stage04();
                break;
            case 5:
                Stage05();
                break;
            case 6:
                Stage06();
                break;           
        }

        answerArray = new List<int>[3] { frontAnswerList, sideAnswerList, topAnswerList };
        Debug.Log("AnswerData_Easy");
        Debug.Log(frontAnswerList);
        return answerArray;
    }

    //---------------------------------------------------------------------------


    void Stage01()
    {
        frontAnswerList = new List<int> { 0, 0, 1
                                        , 0, 1, 1
                                        , 1, 1, 1 };

        sideAnswerList = new List<int>  { 0, 1, 0
                                        , 0, 1, 1
                                        , 1, 1, 1 };

        topAnswerList = new List<int>   { 1, 1, 0
                                        , 0, 1, 1
                                        , 0, 1, 0 };
    }

    void Stage02()
    {
        frontAnswerList = new List<int> { 0, 0, 0
                                        , 1, 1, 1
                                        , 1, 1, 1 };

        sideAnswerList = new List<int>  { 0, 0, 0
                                        , 1, 1, 1
                                        , 1, 1, 1 };

        topAnswerList = new List<int>   { 1, 0, 1
                                        , 0, 1, 0
                                        , 0, 1, 1 };
    }

    void Stage03()
    {
        frontAnswerList = new List<int> { 0, 1, 0
                                        , 0, 1, 0
                                        , 1, 1, 0 };

        sideAnswerList = new List<int>  { 0, 1, 0
                                        , 0, 1, 1
                                        , 1, 1, 1 };

        topAnswerList = new List<int>   { 0, 1, 0
                                        , 1, 1, 0
                                        , 0, 1, 0 };
    }

    void Stage04()
    {
        frontAnswerList = new List<int> { 0, 0, 1
                                        , 0, 1, 1
                                        , 1, 1, 1 };

        sideAnswerList = new List<int>  { 0, 1, 0
                                        , 0, 1, 1
                                        , 1, 1, 1 };

        topAnswerList = new List<int>   { 1, 1, 0
                                        , 0, 1, 1
                                        , 0, 1, 0 };
    }

    void Stage05()
    {
        frontAnswerList = new List<int> { 0, 0, 0
                                        , 1, 1, 1
                                        , 1, 1, 1 };

        sideAnswerList = new List<int>  { 0, 0, 0
                                        , 1, 1, 1
                                        , 1, 1, 1 };

        topAnswerList = new List<int>   { 1, 0, 1
                                        , 0, 1, 0
                                        , 0, 1, 1 };
    }

    void Stage06()
    {
        frontAnswerList = new List<int> { 0, 1, 0
                                        , 0, 1, 0
                                        , 1, 1, 0 };

        sideAnswerList = new List<int>  { 0, 1, 0
                                        , 0, 1, 1
                                        , 1, 1, 1 };

        topAnswerList = new List<int>   { 0, 1, 0
                                        , 1, 1, 0
                                        , 0, 1, 0 };
    }
 
}
