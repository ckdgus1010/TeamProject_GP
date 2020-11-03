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
    private int order = 0;

    public string[] helpMessages = new string[9] { "좌우로 천천히 움직여 \n 평면을 인식하세요"
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
        helperMessage.text = helpMessages[order];

        // 나중에 평면이 인식되면 그 후에 바꾸는 것으로 method 변경하기
        Invoke("ChangeText", 4.0f);
    }

    void ChangeText()
    {
        order += 1;
        helperMessage.text = helpMessages[order];
        Debug.Log($"PlayHelpPopup ::: order = {order}");
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
                Debug.Log($"PlayHelperPopup ::: order = {order}");
                break;
            case 2:     // "[+] 버튼을 눌러 \n 맵 위에 큐브를 3개 생성해 보세요."
                makeCubeButton.interactable = true;
                break;
            case 3:     // "[-] 버튼을 눌러 \n 생성한 큐브를 삭제해 보세요."
                makeCubeButton.interactable = false;
                deleteCubeButton.interactable = true;
                break;
            case 4:     // "[리셋] 버튼을 눌러 \n 생성한 큐브를 모두 지워보세요."
                deleteCubeButton.interactable = false;
                resetCubeButton.interactable = true;
                break;
            case 5:     // "[카드 보기] 버튼을 눌러 \n 문제 카드를 확인하세요."
                resetCubeButton.interactable = false;
                cardButton.interactable = true;
                break;
            case 6:     // "카드를 보고 \n 알맞은 입체도형을 만들어 보세요."
                makeCubeButton.interactable = true;
                deleteCubeButton.interactable = true;
                resetCubeButton.interactable = true;
                checkButton.interactable = true;
                break;
            case 7:     // "[정답 확인] 버튼을 눌러 \n 정답을 확인하세요."
                Debug.Log($"PlayHelperPopup ::: order = {order}");
                break;
            case 8:
                break;
        }
    }
}
