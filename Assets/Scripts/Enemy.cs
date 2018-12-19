using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TextRPG;

public class Enemy : Character
{
    public string Description { get; set; }
    public override void TakeDamage(int amount)
    {
        
        base.TakeDamage(amount);
        // We can tack on whatever we want to the TakeDamage() method in character
        // It will do everything in character plus whatever we add
        UIController.OnEnemyUpdate(this);
        Debug.Log("Enemy has been attacked.");
    }

    public override void Die()
    {
        Encounter.OnEnemyDie();
        Energy = MaxEnergy;
        UIController.OnEnemyUpdate(null);

        // This will do something differently than what character Die() does
        Debug.Log("Enemy has died");
    }
}
