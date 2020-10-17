using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    #region Singleton Pattern

    // 싱글톤 패턴을 사용하기 위한 인스턴스 변수
    private static GameManager _instance;

    // 인스턴스에 접근하기 위한 프로퍼티
    public static GameManager Instance
    {
        get
        {
            // 인스턴스가 없는 경우에 접근하려 하면 인스턴스를 할당해준다.
            if (!_instance)
            {
                _instance = FindObjectOfType(typeof(GameManager)) as GameManager;

                if (_instance == null)
                    Debug.Log("GameManager ::: no Singleton obj");
            }
            return _instance;
        }
    }

    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
        }
        // 인스턴스가 존재하는 경우 새로생기는 인스턴스를 삭제한다.
        else if (_instance != this)
        {
            Destroy(gameObject);
        }
        // 아래의 함수를 사용하여 씬이 전환되더라도 선언되었던 인스턴스가 파괴되지 않는다.
        DontDestroyOnLoad(gameObject);
    }

    #endregion

    //---------------------------------------------

    [Header("User Infomation")]
    public string username = "Guest";
    public Sprite profileImage = null;
    public List<Sprite> profileImageList = new List<Sprite>();      // 프로필 사진

    [Header("User Achievement")]
    public int masterCount = 0;     // 같이하기 모드에서 방장을 한 횟수
    public bool[] achievement = new bool[10];

    [Header("Stage Infomation")]
    public int modeID = 1000;       // Play Mode의 정보
    public int stageID = 0;         // Stage의 정보

    //Stage Clear 여부 정보 생성
    public enum StageState { Forbidden, Cleared, Current };

    //혼자하기 모드 유형별 진행 상황
    public List<StageState> stageStateList;

    //혼자히기 모드 스테이지 상태 - 그림
    public Sprite[] buttonImageArray = new Sprite[3];

    //혼자하기 모드 유형별 저장된 진행 상황
    public List<StageState>[] stageStateArray;
    public List<StageState> mode01_StageStatusList;           //혼자하기 모드 - 유형1
    public List<StageState> mode02_StageStatusList;           //혼자하기 모드 - 유형2
    public List<StageState> mode03_StageStatusList;           //혼자하기 모드 - 유형3


   
    private void Start()
    {
        // 플레이어 기본 이름 생성
        int num = Random.Range(0, 1000);
        username = username + num;

        //GenerateData();
        SaveManager.Load();
        //Debug.Log($"GameManager.stageStateList // SaveManager.stageStatus ::: {GameManager.Instance.stageStateList.Count} // {SaveManager.stageStatus.Count}");
    }

    public void GenerateData()
    {
        stageStateArray = new List<StageState>[] { mode01_StageStatusList
                                                 , mode02_StageStatusList
                                                 , mode03_StageStatusList };

        for (int i = 0; i < stageStateArray.Length; i++)
        {
            MakeDefaultList(stageStateArray[i]);
        }

        modeID = 1000;
        stageID = 0;

        masterCount = 0;
    }

    void MakeDefaultList(List<StageState> list)
    {
        list[0] = StageState.Current;

        for (int i = 1; i < list.Count; i++)
        {
            list[i] = StageState.Forbidden;
        }
    }
}
