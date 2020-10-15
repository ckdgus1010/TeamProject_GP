using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProfileUsername : MonoBehaviour
{
    [SerializeField] private Text username;

    void Start()
    {
        username.text = GameManager.Instance.username;
    }
}
