using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TextRPG;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    [SerializeField]
    Player player;

    [SerializeField]
    Text playerStatsText, enemyStatsText, playerInventoryText;
    public delegate void OnPlayerUpdateHandler();
    public static OnPlayerUpdateHandler OnPlayerStatChange;
    public static OnPlayerUpdateHandler OnPlayerInventoryChange;

    public delegate void OnEnemyUpdateHandler(Enemy enemy);
    public static OnEnemyUpdateHandler OnEnemyUpdate;
    // Use this for initialization
    void Start ()
    {
        OnPlayerStatChange += UpdatePlayerStats;
        OnPlayerInventoryChange += UpdatePlayerInventory;
        OnEnemyUpdate += UpdateEnemyStats;    
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void UpdatePlayerStats()
    {
        playerStatsText.text = string.Format("Player: {0} energy, {1} Attack, {2} defence, {3}g", player.Energy, player.Attack, player.Defence, player.Gold);
    }

    public void UpdatePlayerInventory()
    {
        playerInventoryText.text = "Items: ";
        foreach (string item in player.Inventory)
        {
            playerInventoryText.text += item + " / ";
        }
    }

    public void UpdateEnemyStats(Enemy enemy)
    {
        if (enemy)
        {
            enemyStatsText.text = string.Format("{0}:\n {1} energy,\n {2} Attack,\n {3} defence", enemy.Description, enemy.Energy, enemy.Attack, enemy.Defence);
            if (player.Room.Empty == true)
            {
                enemyStatsText.text = "";
            }
        }
        else
        {
            enemyStatsText.text = "";
        }
    }
}
