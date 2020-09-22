using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class GameMap : MonoBehaviour
{
    public GameObject[] mapList;
    public GameObject[] QuestionList;
    public GameObject map;
    private GameObject levelQuestion;
    public Transform levelPos;

    // Start is called before the first frame update
    void Start()
    {
        //map = Instantiate(mapList[WatingButtonMgr.instance.mapCount]);
        map = mapList[WatingButtonMgr.instance.mapCount];
        map.transform.position = transform.position;


       // levelQuestion = Instantiate(QuestionList[(int)WatingButtonMgr.instance.curruntLevels]);
       // levelQuestion.transform.position = levelPos.position ; 
    }

    public void OncilckBackBt()
    {
        SceneManager.LoadScene("05. PlayMode");
    }
}
