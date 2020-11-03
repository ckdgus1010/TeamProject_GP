using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialAnswerData : MonoBehaviour
{
    public List<int> frontAnswerList = new List<int> { 0, 0, 0
                                                     , 0, 0, 1
                                                     , 0, 1, 1 };

    public List<int> sideAnswerList = new List<int>  { 0, 0, 0
                                                     , 0, 0, 1
                                                     , 0, 1, 1 };

    public List<int> topAnswerList = new List<int>   { 0, 1, 1
                                                     , 0, 0, 1
                                                     , 0, 0, 0 };
}
