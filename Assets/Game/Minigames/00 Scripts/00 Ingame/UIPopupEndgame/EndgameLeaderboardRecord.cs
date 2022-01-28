using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EndgameLeaderboardRecord : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI txtName;
    [SerializeField] private TextMeshProUGUI txtTime;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void Populate(string playerId, int time)
    {
        txtName.text = playerId;
        txtTime.text = $"{time}s";
    }
    
}
