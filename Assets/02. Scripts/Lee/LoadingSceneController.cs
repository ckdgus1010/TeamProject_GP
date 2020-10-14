using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;
using System.Threading;

public class LoadingSceneController : MonoBehaviour
{
    #region Singleton Pattern

    // 싱글톤 패턴을 사용하기 위한 인스턴스 변수
    private static LoadingSceneController _instance;

    // 인스턴스에 접근하기 위한 프로퍼티
    public static LoadingSceneController Instance
    {
        get
        {
            // 인스턴스가 없는 경우에 접근하려 하면 인스턴스를 할당해준다.
            if (!_instance)
            {
                var obj = FindObjectOfType<LoadingSceneController>();

                if (obj != null)
                {
                    _instance = obj;
                }
                else
                {
                    _instance = Create();
                }
            }
            return _instance;
        }
    }

    private static LoadingSceneController Create()
    {
        return Instantiate(Resources.Load<LoadingSceneController>("LoadingUI"));
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

    //----------------------------------------------------------------------------

    [SerializeField]
    private CanvasGroup canvasGroup;

    [SerializeField]
    private Slider progressBar;

    private string loadSceneName;
    public bool isChecked = false;
    public enum Status { None, Success, Failure };
    public Status isConverted = Status.None;

    public void LoadScene(string sceneName)
    {
        gameObject.SetActive(true);
        SceneManager.sceneLoaded += OnSceneLoaded;
        loadSceneName = sceneName;
        Debug.Log($"LoadingSceneController ::: loadSceneName = {loadSceneName}");

        StartCoroutine(LoadSceneProcess());
    }

    private IEnumerator LoadSceneProcess()
    {
        progressBar.value = 0.0f;
        Debug.Log("LoadingSceneController ::: progressBar.value = 0");

        // coroutine 안에서 StartCoroutine으로 다른 coroutine을 실행시키면서 yield return 시키면 호출한 coroutine이 끝날 때까지 기다리게 만들 수 있음
        yield return StartCoroutine(Fade(true));
        Debug.Log("LoadingSceneController ::: Fade in");

        // 로그인이 끝날 때까지 대기
        yield return new WaitUntil(() => isChecked = true && isConverted != Status.None);

        if (isConverted == Status.Success)
        {
            AsyncOperation op = SceneManager.LoadSceneAsync(loadSceneName);
            Debug.Log($"LoadingSceneController ::: {isConverted} // Scene 넘어감");
            op.allowSceneActivation = false;

            // scene의 loading 상황을 progressBar로 표시
            float timer = 0.0f;
            while (!op.isDone)
            {
                //반복문이 한 번 돌 때마다 유니티 엔진에 제어권을 넘김
                yield return null;
                if (op.progress < 0.9f)
                {
                    progressBar.value = op.progress;
                }
                else
                {
                    timer += Time.unscaledDeltaTime;
                    progressBar.value = Mathf.Lerp(0.9f, 1.0f, timer);
                    if (progressBar.value >= 1.0f)
                    {
                        op.allowSceneActivation = true;
                        yield break;
                    }
                }
            }
        }
        else
        {
            Debug.Log($"LoadingSceneController ::: {isConverted} // 로그인 실패");
            StartCoroutine(Fade(false));
        }
    }

    private IEnumerator Fade(bool isFadeIn)
    {
        float timer = 0f;
        while (timer < 1.0f)
        {
            yield return null;
            timer += Time.unscaledDeltaTime * 3f;
            canvasGroup.alpha = isFadeIn ? Mathf.Lerp(0.0f, 1.0f, timer) : Mathf.Lerp(1.0f, 0.0f, timer);
        }

        if (!isFadeIn)
        {
            gameObject.SetActive(false);
        }
    }

    // Scene Loading이 끝난 시점을 알려주는 Callback 함수
    private void OnSceneLoaded(Scene arg0, LoadSceneMode arg1)
    {
        if (arg0.name == loadSceneName)
        {
            StartCoroutine(Fade(false));

            // Callback을 제거
            // 이를 제거하지 않으면 Scene이 loading이 시작될 때 등록한 Callback이 중첩돼 문제가 발생함
            SceneManager.sceneLoaded -= OnSceneLoaded;
        }
    }
}
