using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnswerDataNomal : MonoBehaviour
{ 
    //4*4
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
                Stage01();
                break;
            case 2:
                Stage02();
                break;
            case 3:
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
            case 7:
                Stage01();
                break;
            case 8:
                Stage02();
                break;
            case 9:
                Stage03();
                break;

        }
        answerArray = new List<int>[3] { frontAnswerList, sideAnswerList, topAnswerList };
        Debug.Log($"AnswerData_Normal ::: {_stageID}");
        return answerArray;
    }

    //---------------------------------------------------------------------------


    void Stage01()
    {
        frontAnswerList = new List<int> { 0, 0, 0, 0
                                        , 0, 1, 0, 0
                                        , 1, 1, 1, 0
                                        , 1, 1, 1, 1};

        sideAnswerList = new List<int>  { 0, 0, 0, 0
                                        , 0, 0, 1, 0
                                        , 0, 1, 1, 0
                                        , 0, 1, 1, 0};

        topAnswerList = new List<int>   { 0, 0, 0, 0
                                        , 1, 1, 1, 0
                                        , 1, 1, 1, 1
                                        , 0, 0, 0, 0};
    }

    void Stage02()
    {
        frontAnswerList = new List<int> { 0, 0, 0, 0
                                        , 0, 0, 1, 0
                                        , 0, 1, 1, 1
                                        , 1, 1, 1, 1 };

        sideAnswerList = new List<int>  { 0, 0, 0, 0
                                        , 0, 1, 0, 0
                                        , 0, 1, 0, 1
                                        , 1, 1, 1, 1 };

        topAnswerList = new List<int>   { 0, 1, 1, 1
                                        , 0, 1, 0, 0
                                        , 1, 1, 1, 1
                                        , 0, 1, 0, 0 };
    }

    void Stage03()
    {
        frontAnswerList = new List<int> { 0, 0, 0, 0
                                        , 0, 0, 1, 0
                                        , 1, 0, 1, 0
                                        , 1, 1, 1, 1 };

        sideAnswerList = new List<int>  { 0, 0, 0, 1
                                        , 0, 0, 0, 1
                                        , 1, 1, 0, 1
                                        , 1, 1, 1, 1 };

        topAnswerList = new List<int>   { 1, 1, 1, 1
                                        , 1, 0, 0, 0
                                        , 0, 1, 1, 0
                                        , 1, 0, 0, 1 };
    }

    void Stage04()
    {
        frontAnswerList = new List<int> { 0, 1, 0, 0
                                        , 0, 1, 0, 0
                                        , 1, 1, 1, 0
                                        , 1, 1, 1, 0 };

        sideAnswerList = new List<int>  { 0, 1, 0, 0
                                        , 0, 1, 0, 0
                                        , 0, 1, 0, 1
                                        , 1, 1, 1, 1 };

        topAnswerList = new List<int>   { 1, 1, 0, 0
                                        , 0, 1, 0, 0
                                        , 0, 1, 1, 0
                                        , 1, 1, 0, 0 };
    }

    void Stage05()
    {
        frontAnswerList = new List<int> { 1, 0, 0, 0
                                        , 1, 1, 0, 0
                                        , 1, 1, 1, 0
                                        , 1, 1, 1, 1 };

        sideAnswerList = new List<int>  { 0, 0, 0, 1
                                        , 0, 0, 1, 1
                                        , 0, 1, 1, 1
                                        , 1, 1, 1, 1 };

        topAnswerList = new List<int>   { 1, 1, 1, 1
                                        , 1, 1, 0, 0
                                        , 1, 0, 0, 0
                                        , 1, 0, 0, 0 };
    }

    void Stage06()
    {
        frontAnswerList = new List<int> { 0, 0, 0, 0
                                        , 0, 1, 1, 0
                                        , 0, 1, 1, 0
                                        , 1, 1, 1, 1};

        sideAnswerList = new List<int>  { 0, 0, 0, 0
                                        , 0, 1, 1, 0
                                        , 0, 1, 1, 0
                                        , 1, 1, 1, 1 };

        topAnswerList = new List<int>   { 0, 0, 1, 0
                                        , 1, 1, 1, 0
                                        , 0, 1, 1, 1
                                        , 0, 1, 0, 0 };
    }

}
