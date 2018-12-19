﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TextRPG;
using UnityEngine.UI;

public class Encounter : MonoBehaviour
{
    public Enemy Enemy { get; set; }
    [SerializeField]
    Player player;

    [SerializeField]
    Button[] dynamicControls;
    public delegate void OnEnemyDieHandler();
    public static OnEnemyDieHandler OnEnemyDie;

    private void Start()
    {
        OnEnemyDie += Loot;
    }

    public void ResetDynamicControls()
    {
        foreach(Button button in dynamicControls)
        {
            button.interactable = false;
        }
    }

    public void StartCombat()
    {
        this.Enemy = player.Room.Enemy;
        dynamicControls[0].interactable = true;
        dynamicControls[1].interactable = true;
        UIController.OnEnemyUpdate(this.Enemy);
    }

    public void StartChest()
    {
        dynamicControls[3].interactable = true;
    }

    public void StartExit()
    {
        dynamicControls[2].interactable = true;
    }

    public void OpenChest()
    {
        Chest chest = player.Room.Chest;
        if (chest.Trap)
        {
            player.TakeDamage(5);
            UIController.OnPlayerStatChange();
            Journal.Instance.Log("It was a trap! You take 5 damage.");
        }
        else if (chest.Heal)
        {
            player.TakeDamage(-7);
            UIController.OnPlayerStatChange();
            Journal.Instance.Log("The chest heals you for 7 energy.");
        }
        else if (chest.Enemy)
        {
            player.Room.Enemy = chest.Enemy;
            player.Room.Chest = null;
            Journal.Instance.Log("The chest contained a monster. You're undera attack!");
            player.Investigate();
        }
        else
        {
            player.Gold += chest.Gold;
            player.AddItem(chest.Item);
            UIController.OnPlayerStatChange();
            UIController.OnPlayerInventoryChange();
            Journal.Instance.Log("You found: " + chest.Item + " and <color=FFE556FF>" + chest.Gold + "g.</color>");
        }
        player.Room.Chest = null;
        dynamicControls[3].interactable = false;
    }

    public void Attack()
    {
        int playerDamageAmount = (int)(Random.value * (player.Attack - Enemy.Defence));
        int enemyDamageAmount = (int)(Random.value * (Enemy.Attack - player.Defence));
        Journal.Instance.Log("<color=#59ffa1>You attacked, dealing <b>" + playerDamageAmount + "</b> damage!</color>");
        Journal.Instance.Log("<color=#59ffa1>The enemy fights back, dealing <b>" + enemyDamageAmount + "</b> damage!</color>");
        player.TakeDamage(enemyDamageAmount);
        Enemy.TakeDamage(playerDamageAmount);
    }

    public void Flee()
    {
        int enemyDamageAmount = (int)(Random.value * (Enemy.Attack - player.Defence*.5f));
        player.Room.Enemy = null;
        UIController.OnEnemyUpdate(null);
        player.TakeDamage(enemyDamageAmount);
        Journal.Instance.Log("<color=#59ffa1>As you flee past the enemy he strikes out dealing <b>" + enemyDamageAmount + "</b> damage!</color>");
        player.Investigate();
    }

    public void ExitFloor()
    {
        StartCoroutine(player.world.GenerateFloor());
        player.Floor += 1;
        Journal.Instance.Log("You found an exit to another floor. Floor: " + player.Floor);

    }

    public void Loot()
    {
        player.AddItem(this.Enemy.Inventory[0]);
        player.Gold += this.Enemy.Gold;
        UIController.OnPlayerInventoryChange();
        Journal.Instance.Log(string.Format(
            "<color=#56FFC7FF>You've slain {0}. Searching the carcass, you find a {1} and {2} gold!</color>",
            this.Enemy.Description, this.Enemy.Inventory[0], this.Enemy.Gold
        ));
        player.Room.Enemy = null;
        player.Room.Empty = true;
        UIController.OnEnemyUpdate(this.Enemy);
        player.Investigate();
    }
}