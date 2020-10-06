using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class GameMap : MonoBehaviour
{
    public GameObject[] mapList;
    public GameObject[] questionList;
    public GameObject map;
    private GameObject levelQuestion;
    public Transform levelPos;

    private TouchManager touchManager;
    // Start is called before the first frame update
    void Start()
    {
        touchManager = GetComponent<TouchManager>();
        //map = Instantiate(mapList[WatingButtonMgr.instance.mapCount]);
        map = mapList[WatingButtonMgr.instance.mapCount];
        //map.transform.position = transform.position;

        // 터치매니저에서 클라이언트가 리졸브를 다 받았다면
        // 퀘스트매니저에서 watingmgr에서 mlevel를 확인하고 
        // 그에 맞는 문제를 낸다.

    }
    private void Update()
    {
        //if (touchManager.resolveFinish == true)
        //{
                
        //}
    }

    public void OncilckBackBt()
    {
        SceneManager.LoadScene("05. PlayMode");
    }
}
