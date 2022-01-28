using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CharacterPreviewEndgame : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI txtPlayerid;

    [SerializeField]
    private GameObject goTextRank;


    public void ShowPlayer(bool isWin, string playerId)
    {
        goTextRank.SetActive(isWin);
        txtPlayerid.text = playerId;
        //TODO: Update character's costume
        //TODO: play idle anim if needed
        
    }
}
