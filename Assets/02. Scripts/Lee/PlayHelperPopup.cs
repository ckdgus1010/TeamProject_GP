using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayHelperPopup : MonoBehaviour
{
    [Header("버튼 관리")]
    [SerializeField] private Button makeCubeButton;
    [SerializeField] private Button deleteCubeButton;
    [SerializeField] private Button resetCubeButton;
    [SerializeField] private Button cardButton;
    [SerializeField] private Button checkButton;
    [SerializeField] private Button resetMapButton;
    [SerializeField] private Button screenShotButton;

    [Header("안내 메세지 관리")]
    [SerializeField] private Text helperMessage;
    public int order = 0;
    public TutorialTouchManager touchManager;
    private int count = 0;

    public string[] helpMessages = new string[12] { "좌우로 천천히 움직여 \n 평면을 인식하세요"
                                                  , "원하는 곳을 터치해 맵을 생성하세요."
                                                  , "맵 크기가 너무 크다구요? \n 맵 크기 슬라이더를 이용해 조절해 보세요."
                                                  , "맵 삭제를 눌러 \n 맵을 다시 생성할 수 있습니다."
                                                  , "원하는 곳을 터치해 맵을 생성하세요."
                                                  , "[+] 버튼을 눌러 \n 맵 위에 큐브를 3개 생성해 보세요."
                                                  , "[-] 버튼을 눌러 \n 생성한 큐브를 삭제해 보세요."
                                                  , "[리셋] 버튼을 눌러 \n 생성한 큐브를 모두 지워보세요."
                                                  , "[카드 보기] 버튼을 눌러 \n 문제 카드를 확인하세요."
                                                  , "카드를 보고 \n 알맞은 입체도형을 만들어 보세요."
                                                  , "[정답 확인] 버튼을 눌러 \n 정답을 확인하세요."
                                                  , "튜토리얼은 '옵션 팝업창 >> 튜토리얼'을 \n 통해 다시 할 수 있습니다." };       // 정답일 경우

    private void Start()
    {
        order = 0;
        count = 0;
        helperMessage.text = helpMessages[order];
        touchManager.enabled = false;

        // 나중에 평면이 인식되면 그 후에 바꾸는 것으로 method 변경하기
        Invoke("ChangeText", 6.0f);
    }

    public void SetOrigin()
    {
        makeCubeButton.interactable = true;
        deleteCubeButton.interactable = false;
        resetCubeButton.interactable = false;
        cardButton.interactable = false;
        checkButton.interactable = false;
        resetMapButton.interactable = false;
        screenShotButton.interactable = false;
    }

    public void ChangeText()
    {
        order += 1;
        helperMessage.text = helpMessages[order];
        Debug.Log($"PlayHelpPopup ::: ChangeText // order = {order}");

        if (order == 1)
        {
            touchManager.enabled = true;
        }
    }

    public void ChangeHelpMessageText()
    {
        order += 1;

        if (order != helpMessages.Length)
        {
            helperMessage.text = helpMessages[order];
            ChangeButtonState(order);
        }
        else
        {
            Debug.Log("PlayHelperPopup ::: 튜토리얼 메세지 끝");
        }
    }

    void ChangeButtonState(int order)
    {
        switch (order)
        {
            case 0:
            case 1:
                Debug.Log($"PlayHelperPopup ::: ChangeButtonState // order = {order}");
                break;
            case 2:     // "맵 크기가 너무 크다구요? \n 맵 크기 슬라이더를 이용해 조절해 보세요."
                Invoke("ChangeHelpMessageText", 7.0f);
                break;
            case 3:     // "맵 삭제를 눌러 맵을 다시 생성할 수 있습니다."
                resetMapButton.interactable = true;
                break;
            case 4:     // "원하는 곳을 터치해 맵을 생성하세요."
                break;
            case 5:     // "[+] 버튼을 눌러 \n 맵 위에 큐브를 3개 생성해 보세요."
                makeCubeButton.interactable = true;
                break;
            case 6:     // "[-] 버튼을 눌러 \n 생성한 큐브를 삭제해 보세요."
                makeCubeButton.interactable = false;
                deleteCubeButton.interactable = true;
                break;
            case 7:     // "[리셋] 버튼을 눌러 \n 생성한 큐브를 모두 지워보세요."
                deleteCubeButton.interactable = false;
                resetCubeButton.interactable = true;
                break;
            case 8:     // "[카드 보기] 버튼을 눌러 \n 문제 카드를 확인하세요."
                resetCubeButton.interactable = false;
                cardButton.interactable = true;
                break;
            case 9:     // "카드를 보고 \n 알맞은 입체도형을 만들어 보세요."
                makeCubeButton.interactable = true;
                deleteCubeButton.interactable = true;
                resetCubeButton.interactable = true;
                checkButton.interactable = true;
                cardButton.interactable = true;
                break;
            case 10:     // "[정답 확인] 버튼을 눌러 \n 정답을 확인하세요."
                Debug.Log($"PlayHelperPopup ::: order = {order}");
                break;
        }
    }
}
