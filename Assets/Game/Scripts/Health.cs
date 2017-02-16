using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour 
{
	public int maxHealth;
	int health;

	void Start()
	{
		health = maxHealth;
	}

	public void TookDamage(int damage)
	{
		health = health - damage;

		if (health <= 0) 
		{
			Died ();
		}
	}

	void Died()
	{
		Destroy (gameObject);
	}
}
