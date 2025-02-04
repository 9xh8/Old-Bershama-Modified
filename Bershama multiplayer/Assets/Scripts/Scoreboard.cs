using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;

public class Scoreboard : MonoBehaviourPunCallbacks
{
    [SerializeField] Transform container;
    [SerializeField] GameObject scoreboardItemPrefab;

    [SerializeField] CanvasGroup canvasGroup;

    Dictionary<Player, ScoreboardItem> ScoreboardItems = new Dictionary<Player, ScoreboardItem>();

    void Start()
    {
        foreach(Player player in PhotonNetwork.PlayerList)
        {
            AddScoreboardItem(player);
        }    
    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        AddScoreboardItem(newPlayer);
    }

    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        RemoveScoreboardItem(otherPlayer);
    }

    void AddScoreboardItem(Player player)
    {
        ScoreboardItem item = Instantiate(scoreboardItemPrefab, container).GetComponent<ScoreboardItem>();
        item.Initialize(player);
        ScoreboardItems[player] = item;
    }

    void RemoveScoreboardItem(Player player)
    {
        Destroy(ScoreboardItems[player].gameObject);
        ScoreboardItems.Remove(player);
    }

    void Update()
    {
        if(Input.GetKey(KeyCode.Tab))
        {
            canvasGroup.alpha = 1;
        }
        else
        {
            canvasGroup.alpha = 0;
        }
    }

}
