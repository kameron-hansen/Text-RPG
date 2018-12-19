using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TextRPG
{
    public class Character : MonoBehaviour
    {
        public int Energy { get; set; }
        public int MaxEnergy { get; set; }
        public int Attack { get; set; }
        public int Defence { get; set; }
        public int Gold { get; set; }
        public Vector2 RoomIndex { get; set; }
        public List<string> Inventory { get; set; } = new List<string>();

            
        public virtual void TakeDamage(int amount)
        {
            Energy -= amount;
            if (Energy <= 0)
            {
                // If your energy is <= than 0 you die
                Die();
            }
        }

        public virtual void Die()
        {
            Debug.Log("Charactered has died. Game Over.");
        }

    }
}
