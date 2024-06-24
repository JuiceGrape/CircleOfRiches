using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerDisplay : MonoBehaviour
{
    public TextResourceListener roundScore;
    public TextResourceListener bankScore;
    public TMP_Text playerName;
    public Image background;

    static float xPosMod = 50;

    public void Init(Player player)
    {
        roundScore.resource = player.roundCash;
        bankScore.resource = player.gameCash;
        playerName.text = player.profile.Name;
        background.color = player.profile.playerColor;
    }

    public void Activate()
    {
        transform.Translate(new Vector3(xPosMod, 0, 0));
    }

    public void Deactivate() 
    {
        transform.Translate(new Vector3(-xPosMod, 0, 0));
    }
}
