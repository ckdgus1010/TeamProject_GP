using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoginMgr : MonoBehaviour
{
    public GameObject signInImage;
    public GameObject signUpImage;

    public GameObject id_InputField;
    public GameObject pw_InputField;
    public GameObject Email_InputField;

    private Animator id_Animator;
    private Animator pw_Animator;

    public GameObject email_Active_Image;
    private bool signUpCount;


    public void Start()
    {
        id_Animator = id_InputField.GetComponent<Animator>();
        pw_Animator = pw_InputField.GetComponent<Animator>();
    }
    public void OnClickSignInButton()
    {
        if(signUpCount == false)
        {
            return;
        }
        signInImage.SetActive(true);
        signUpImage.SetActive(false);
        id_Animator.SetBool("SignUP_ID", false);
        id_Animator.SetBool("SignIn_ID", true);
        pw_Animator.SetBool("SignUP_PW", false);
        pw_Animator.SetBool("SignIn_PW", true);
        Email_InputField.SetActive(false);
        email_Active_Image.SetActive(false);
        
    }
    public void OnClickSignUpButton()
    {
        signInImage.SetActive(false);
        signUpImage.SetActive(true);
        id_Animator.SetBool("SignUP_ID", true);
        id_Animator.SetBool("SignIn_ID", false);
        pw_Animator.SetBool("SignUP_PW", true);
        pw_Animator.SetBool("SignIn_PW", false);
        Email_InputField.SetActive(true);
        email_Active_Image.SetActive(true);
        signUpCount = true;
    }
}
