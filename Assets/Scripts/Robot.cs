using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TextRPG;

public class Robot : Enemy
{
	// Use this for initialization
	void Start () {
        Energy = 15;
        MaxEnergy = 15;
        Attack = 8;
        Defence = 5;
        Gold = 20;
        Description = "Robot";
        Inventory.Add("Metal Rod");
	}
}
