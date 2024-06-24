using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPreviewManager : MonoBehaviour
{
    public PlayerDisplay PlayerDisplayPrefab;

    private static int spawnPosStart = 315;
    private static int spawnPosIncrement = -100;

    private int spawnPosCurrent = spawnPosStart;

    private List<PlayerDisplay> playerList = new List<PlayerDisplay>();

    PlayerDisplay activePlayer;

    public void Init(Player player)
    {
        PlayerDisplay display = Instantiate(PlayerDisplayPrefab, transform, false);
        display.transform.SetLocalPositionAndRotation(new Vector3(0, spawnPosCurrent), Quaternion.identity);
        display.Init(player);
        spawnPosCurrent += spawnPosIncrement;
        playerList.Add(display);
    }

    public void setActivePlayer(int player)
    {
        if (player >= playerList.Count)
        {
            throw new System.IndexOutOfRangeException("Not enough player displays");
        }

        if (activePlayer != null)
            activePlayer.Deactivate();

        activePlayer = playerList[player];

        activePlayer.Activate();
    }
}
