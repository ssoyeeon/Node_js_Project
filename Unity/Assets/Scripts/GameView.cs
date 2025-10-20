using System.Collections;
using System.Collections.Generic;
using System.Net.Http.Headers;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class GameView : MonoBehaviour
{
    public Text playerNameText;
    public Text metalText;
    public Text crystalText;
    public Text deuteriumText;
    public InputField playerNameInput;

    public Button registerButton;
    public Button loginButton;
    public Button collectButton;
    public Button developButton;
    public Slider progressBar;
    
    public void SetPlayerName(string name)
    {
        playerNameText.text = name;
    }

    public void UpdateResource(int metal, int crystal, int deuterium)
    {
        metalText.text = $"Metal : {metal}";
        crystalText.text = $"Crystal : {crystal}";
        deuteriumText.text = $"Deuterium : {deuterium}";
    }

    public void UpdateProgressBar(float value)
    {
        progressBar.value = value;
    }

    public void SetRegisterButtonListener(UnityAction action)
    {
        registerButton.onClick.RemoveAllListeners();
        registerButton.onClick.AddListener(action);
    }
    public void SetLoginButtonListener(UnityAction action)
    {
        loginButton.onClick.RemoveAllListeners();
        loginButton.onClick.AddListener(action);
    }
    public void SetCollectrButtonListener(UnityAction action)
    {
        collectButton.onClick.RemoveAllListeners();
        collectButton.onClick.AddListener(action);
    }
    public void SetDevelopButtonListener(UnityAction action)
    {
        developButton.onClick.RemoveAllListeners();
        developButton.onClick.AddListener(action);
    }

}
