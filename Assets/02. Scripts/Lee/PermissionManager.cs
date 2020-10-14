using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Android;

public class PermissionManager : MonoBehaviour
{
    //권한 요청 팝업창
    public GameObject permissionPanel;

    //Coroutine 중복 방지
    private bool isChecking = false;

    //카메라, 저장소 쓰기 권한 허용 유무
    private bool isCameraAllowed = false;
    private bool isStorageAllowed = false;

    //필수 권한 상태 확인하는 동안 버튼 눌림 방지
    public Button[] buttons = new Button[3];

    void Start()
    {
        // 로그인 버튼 비활성화
        ControlLoginButton(false);

        // 권한 확인
        CheckPermissionStatus();
    }

    void CheckPermissionStatus()
    {
        // 카메라 권한 확인
        if (Permission.HasUserAuthorizedPermission(Permission.Camera))
        {
            isCameraAllowed = true;
            Debug.Log($"Permission Manager ::: 카메라 권한 승인됨");
        }
        
        // 저장소 쓰기 권한 확인
        if (Permission.HasUserAuthorizedPermission(Permission.ExternalStorageWrite))
        {
            isStorageAllowed = true;
            Debug.Log($"Permission Manager ::: 저장소 쓰기 권한 승인됨");
        }

        // 저장소 쓰기 권한 확인
        if (Permission.HasUserAuthorizedPermission(Permission.ExternalStorageRead))
        {
            Debug.Log($"Permission Manager ::: 저장소 읽기 권한 승인됨");
        }

        if (isCameraAllowed && isStorageAllowed)
        {
            Debug.Log($"Permission Manager ::: 모든 권한 승인됨");

            // 권한 요청 팝업창 비활성화
            permissionPanel.SetActive(false);

            // 로그인 버튼 활성화
            ControlLoginButton(true);
        }
        else
        {
            Debug.Log($"Permission Manager ::: 필수 권한 요청 \n Camera // Write // Read = {Permission.HasUserAuthorizedPermission(Permission.Camera)} // {Permission.HasUserAuthorizedPermission(Permission.ExternalStorageWrite)} // {Permission.HasUserAuthorizedPermission(Permission.ExternalStorageRead)}");

            //권한 요청 팝업창 활성화
            permissionPanel.SetActive(true);
        }
    }

    // 로그인 버튼 활성화 / 비활성화
    void ControlLoginButton(bool value)
    {
        for (int i = 0; i < buttons.Length; i++)
        {
            buttons[i].interactable = value;
        }
    }

    // 권한 요청 팝업창에서 [확인] 버튼을 눌렀을 경우
    public void RequestPermission()
    {
        if (isChecking == false)
        {
            StartCoroutine(CheckPermissionCoroutine());
        }
    }

    IEnumerator CheckPermissionCoroutine()
    {
        isChecking = true;

        // 카메라 권한 요청
        RequestPermission_Camera();

        yield return new WaitForSeconds(0.2f);
        yield return new WaitUntil(() => Application.isFocused == true);
        // 저장소 쓰기 권한 요청
        RequestPermission_ExternalStorageWrite();

        yield return new WaitForSeconds(0.2f);
        yield return new WaitUntil(() => Application.isFocused == true);
        // 저장소 읽기 권한 요청
        RequestPermission_ExternalStorageRead();

        Debug.Log("Permission Manager ::: 권한 요청 완료");
        CheckPermissionStatus();

        isChecking = false;
    }

    // 카메라 권한 요청
    void RequestPermission_Camera()
    {
        if (!Permission.HasUserAuthorizedPermission(Permission.Camera))
        {
            Debug.Log($"Permission Manager ::: 카메라 권한 없음 // 권한 요청");
            Permission.RequestUserPermission(Permission.Camera);
        }
    }

    // 저장소 쓰기 권한 요청
    void RequestPermission_ExternalStorageWrite()
    {
        if (!Permission.HasUserAuthorizedPermission(Permission.ExternalStorageWrite))
        {
            Debug.Log($"Permission Manager ::: 저장소 쓰기 권한 없음 // 권한 요청");
            Permission.RequestUserPermission(Permission.ExternalStorageWrite);
        }
    }

    // 저장소 읽기 권한 요청
    void RequestPermission_ExternalStorageRead()
    {
        if (!Permission.HasUserAuthorizedPermission(Permission.ExternalStorageRead))
        {
            Debug.Log($"Permission Manager ::: 저장소 쓰기 권한 없음 // 권한 요청");
            Permission.RequestUserPermission(Permission.ExternalStorageRead);
        }
    }
}
