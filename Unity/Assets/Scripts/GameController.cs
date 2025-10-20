using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public GameView gameView;
    public PlayerModel playerModel;
    public GameAPI gameAPI;


    void Start()
    {
        gameAPI = gameObject.AddComponent<GameAPI>();
        gameView.SetRegisterButtonListener(OnRegisterButtonClicked);
        gameView.SetLoginButtonListener(OnLoginButtonClicked);
    }
    public void OnRegisterButtonClicked()
    {
        string playerName = gameView.playerNameInput.text;
        StartCoroutine(gameAPI.RegitserPlayer(playerName, "1234"));
    }

    public void OnLoginButtonClicked()
    {
        string playerName = gameView.playerNameInput.text;
        StartCoroutine(LoginPlayerCoroutine(playerName, "1234"));
    }

    private IEnumerator LoginPlayerCoroutine(string playerName, string password)
    {
        yield return gameAPI.LoginPlayer(playerName, password, player =>
        {
            playerModel = player;
            UpdateResourcesDisplay();
        });
    }
    private void UpdateResourcesDisplay()
    {
        if(playerModel != null)
        {
            gameView.SetPlayerName(playerModel.playerName);
            gameView.UpdateResource(playerModel.metal, playerModel.crystal, playerModel.deuteriurm);
        }
    }
}
