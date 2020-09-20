using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SaveData
{
    //여러가지 타입이 가능하지만, Dictionary는 안된다.
    public List<GameManager.StageState>[] stageStateArray;
    public List<GameManager.StageState> stageStateList01;
    public List<GameManager.StageState> stageStateList02;
    public List<GameManager.StageState> stageStateList03;

    //생성자
    public SaveData(List<GameManager.StageState>[] _stageStatusArray)
    {
        stageStateList01 = new List<GameManager.StageState>();
        stageStateList02 = new List<GameManager.StageState>();
        stageStateList03 = new List<GameManager.StageState>();
        stageStateArray = new List<GameManager.StageState>[3] { stageStateList01, stageStateList02, stageStateList03 };

        for (int i = 0; i < stageStateArray.Length; i++)
        {
            for (int j = 0; j < _stageStatusArray[i].Count; j++)
            {
                stageStateArray[i].Add(_stageStatusArray[i][j]);
            }
        }
    }
}

