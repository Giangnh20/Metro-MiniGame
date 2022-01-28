using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using MiniGame.Common;
using TMPro;
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

public class UIPopupEndgameCanvas : Dialog<UIPopupEndgameCanvas>
{
    [SerializeField] private TextMeshProUGUI txtTitle;
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
        
        var winners = players.Where(p => p.IsWinner.Value).OrderByDescending(w => w.WinTime).ToList();
        // Top 3 winners
        var top3 = winners.GetRange(0, Math.Min(winners.Count, 3));

        // Other players
        var cloner = players.CopyList();
        top3.ForEach(p => { 
            if (cloner.Contains(p)) 
                cloner.Remove(p); 
        });
        var sortedOthers = cloner.OrderByDescending(w => w.WinTime).ToList();
        
        bool isWin = data.IsWin;
        var myUserId = ServiceLocator.Instance.Resolve<UserData>().UserId;
        
        if (isWin)        // WIN POPUP
        {
            txtTitle.text = "YOU WIN";
            // Render top3 winner characters
            for (int i = 0; i < characters.Count; i++)
            {
                var characterView = characters[i];
                if (i < top3.Count)
                {
                    characterView.gameObject.SetActive(true);
                    characterView.ShowPlayer(true, top3[i].PlayerId);
                }
                else
                {
                    characterView.gameObject.SetActive(false);
                }
            }
        }
        else             // LOSE POPUP
        {
            txtTitle.text = "YOU LOSE";
            // Render my character only
            var characterView = characters[0];
            characterView.gameObject.SetActive(true);
            characterView.ShowPlayer(false, myUserId);
        }
        
        
        // Leaderboard
        List<PlayerServerData> displayList = new List<PlayerServerData>();
        displayList.AddRange(top3);
        var myPlayer = sortedOthers.FirstOrDefault(p => p.PlayerId == myUserId);
        if (myPlayer != null)
        {
            int myPlayerIndex = sortedOthers.IndexOf(myPlayer);
            displayList.AddRange(sortedOthers.GetRange(myPlayerIndex, sortedOthers.Count - myPlayerIndex));
        }
        else
        {
            displayList.AddRange(sortedOthers);   
        }
        
        ShowLeaderBoard(displayList);
    }

    private void ShowLeaderBoard(List<PlayerServerData> sortedListPlayers)
    {
        foreach (var player in sortedListPlayers)
        {
            GameObject go = Instantiate(prefab, holder);
            go.transform.localScale = Vector3.one;

            var record = go.GetComponent<EndgameLeaderboardRecord>();
            record.Populate(player.PlayerId, player.WinTime);
        }
    }


    
}
