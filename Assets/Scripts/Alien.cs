using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TextRPG;

public class Alien : Enemy
{
	// Use this for initialization
	void Start () {
        Energy = 10;
        MaxEnergy = 10;
        Attack = 8;
        Defence = 3;
        Gold = 30;
        Description = "Alien";
        Inventory.Add("Black Crystal");
        
	}
}
