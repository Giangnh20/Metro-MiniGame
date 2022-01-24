using System;
using System.Collections;
using System.Collections.Generic;
using MiniGame.Common;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PreStartGameCanvas : Dialog<PreStartGameCanvas>
{
    [SerializeField] private TMP_Dropdown ddGame;
    [SerializeField] private TMP_Dropdown ddDifficulty;
    [SerializeField] private TMP_InputField inputPlayers;
    [SerializeField] private Button btnCreate;

    public static void Show()
    {
        Open();
    }

    public static void Hide()
    {
        Close();
    }

    private void Start()
    {
        btnCreate?.onClick.AddListener(BtnCreateClicked);
    }

    private void BtnCreateClicked()
    {
        try
        {
            var game = (EGameName) ddGame.value;
            var difficulty = (EGameDifficulty) ddDifficulty.value;
            var players = int.Parse(inputPlayers.text);
            PreStartGameData data = new PreStartGameData(game, difficulty, players);
            ServiceLocator.Instance.Resolve<LocalData>().PreStartGameData = data;
            SceneManager.LoadSceneAsync("Squidgame");
        }
        catch
        {
            Debug.LogError("Config error");
        }
        
    }
    
    
}
