using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class EndGameScreenData : IScreenData
{
    public bool IsWin;
    public List<PlayerServerData> Players;

    public EndGameScreenData(bool isWin, List<PlayerServerData> players)
    {
        IsWin = isWin;
        Players = players;
    }
}

public class UIPopupWinCanvas : Dialog<UIPopupWinCanvas>
{
    [SerializeField] private Button btnOk;
    [SerializeField] private List<CharacterPreviewEndgame> characters;
    [SerializeField] private GameObject prefab;
    [SerializeField] private Transform holder;
    
    public static void Show()
    {
        Open();
    }

    public static void Hide()
    {
        Close();
    }
    
    // Start is called before the first frame update
    void Start()
    {
        btnOk?.onClick.AddListener(()=> SceneManager.LoadSceneAsync(StringConstants.SCENE_LOBBY));
    }

    public override void Populate(IScreenData screenData)
    {
        var data = (EndGameScreenData) screenData;
        var players = data.Players;
        var sorted = players.OrderByDescending(w => w.WinTime).ToList();
        var top3 = sorted.GetRange(0, Math.Min(players.Count, 3));

        for (int i = 0; i < characters.Count; i++)
        {
            var characterView = characters[i];
            if (i < top3.Count)
            {
                characterView.gameObject.SetActive(true);
                characterView.ShowPlayers(top3[i].PlayerId);
            }
            else
            {
                characterView.gameObject.SetActive(false);
            }
        }
        
        // Leaderboard
        foreach (var player in sorted)
        {
            GameObject go = Instantiate(prefab, holder);
            go.transform.localScale = Vector3.one;

            var record = go.GetComponent<EndgameLeaderboardRecord>();
            record.Populate(player.PlayerId, player.WinTime);
        }
    }
}
