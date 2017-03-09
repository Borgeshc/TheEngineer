using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemEffects : MonoBehaviour 
{
	public void UsedItem(string itemUsed)
	{
		switch (itemUsed) 
		{
		case "HealthPotion":
			print ("Health Potion Used");
				break;
			case "RegenerationPotion":
			print ("Regeneration Potion Used");
				break;
			case "InsanityPotion":
			print ("Insanity Potion Used");
				break;
			case "ImmunityPotion":
			print ("Immunity Potion Used");
				break;
			case "EnragePotion":
			print ("Enrage Potion Used");
				break;
		}
	}
}
