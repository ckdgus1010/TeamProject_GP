using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Random_TSF : MonoBehaviour
{
    private List<string> fstList = new List<string>() { "앞", "옆", "위" };
    public List<string> list;
    public List<GameObject> playerList;
    private void Start()
    {
        RandomNum();
    }
    public void RandomNum()
    {
        for (int i = 0; i < 3; i++)
        {
            int rand = Random.Range(0, fstList.Count);
            print(fstList[rand]);
            list.Add(fstList[rand]);
            GetRandomNum getRandomNum =  playerList[rand].GetComponent<GetRandomNum>();
            getRandomNum.FindMyindex();

            fstList.RemoveAt(rand);
        }
    }


}
