using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AuthUI : MonoBehaviour
{
    public InputField usernameInput;
    public InputField passwordInput;

    public Button registerButton;
    public Button LoginButton;

    public Text statusText;
    
    public AuthManager authManager;


    // Start is called before the first frame update
    void Start()
    {
        authManager = GetComponent<AuthManager>();
        registerButton.onClick.AddListener(OnRegisterClick);
        LoginButton.onClick.AddListener(OnLoginClick);
    }

    private void OnRegisterClick()
    {
        StartCoroutine(RegisterCoroutine());
    }
    private void OnLoginClick()
    {
        StartCoroutine(LoginCoroutine());
    }


    private IEnumerator RegisterCoroutine()
    {
        statusText.text = "ȸ�� ���� ��...";
        yield return StartCoroutine(authManager.Register(usernameInput.text, passwordInput.text));
        statusText.text = "ȸ�� ���� ����, �α��� ���ּ���";
    }
    private IEnumerator LoginCoroutine()
    {
        statusText.text = "�α��� ��...";
        yield return StartCoroutine(authManager.Register(usernameInput.text, passwordInput.text));
        statusText.text = " �α��� ����";
    }
}
