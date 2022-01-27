using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CharacterPreviewEndgame : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI txtPlayerid;


    public void ShowPlayers(string playerId)
    {
        txtPlayerid.text = playerId;
        //TODO: Update character's costume
        //TODO: play idle anim if needed
        
    }
}
