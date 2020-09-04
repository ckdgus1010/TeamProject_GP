using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMap : MonoBehaviour
{
    public GameObject[] mapList;
    public GameObject[] QuestionList;
    private GameObject map;
    private GameObject levelQuestion;
    public Transform levelPos;

    // Start is called before the first frame update
    void Start()
    {
        map = Instantiate(mapList[WatingButtonMgr.instance.mapCount]);
        map.transform.position = transform.position;


        levelQuestion = Instantiate(QuestionList[(int)WatingButtonMgr.instance.curruntLevels]);
        levelQuestion.transform.position = levelPos.position ; 
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
