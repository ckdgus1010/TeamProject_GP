using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleARCore;
using GoogleARCore.CrossPlatform;

public class Resolve_Button : MonoBehaviour
{
    public GameObject beachMap;
    private GameObject mapObj;
    AsyncTask<CloudAnchorResult> result_AsyncTask;
    public void OnClickResoleButton()
    {
        StartCoroutine(ResolveCloudAnchor(PlayerMgr.cloudID));//코루틴 실행ㅡ

    }
    IEnumerator ResolveCloudAnchor(string cloudID)
    {
        Debug.Log("리졸빙 코루틴 들어옴" + cloudID);
        result_AsyncTask = XPSession.ResolveCloudAnchor(cloudID);
        yield return new WaitUntil(() => result_AsyncTask.IsComplete);

        Debug.Log("리졸빙 응답 대기중....");
        Debug.Log(result_AsyncTask.Result.Response);
        Debug.Log(result_AsyncTask.Result.Anchor);
        Debug.Log(result_AsyncTask.Result.Anchor.CloudId);


        mapObj = Instantiate(beachMap, result_AsyncTask.Result.Anchor.transform.position, Quaternion.identity);
        mapObj.transform.SetParent(result_AsyncTask.Result.Anchor.transform);
    }
}
