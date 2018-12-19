using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TextRPG;

public class EnemyDatabase : MonoBehaviour {

    public List<Enemy> Enemies { get; set; } = new List<Enemy>();
    public static EnemyDatabase Instance { get; set; }

    private void Awake()
    {
        Instance = this;

        foreach(Enemy enemy in GetComponents<Enemy>())
        {
            Debug.Log("Found Enemy");
            Enemies.AddRange(GetComponents<Enemy>());
        }
    }

    public Enemy GetRandomEnemy()
    {
        return Enemies[Random.Range(0, Enemies.Count)];
    }

}
