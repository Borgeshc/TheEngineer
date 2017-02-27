using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour 
{
	public float maxHealth;
	public bool hasHealthBar;
	public Image healthBar;

	float health;

	void Start()
	{
		health = maxHealth;
	}

	public void TookDamage(int damage)
	{
		health = health - damage;

		if (hasHealthBar)
			healthBar.fillAmount = (health / maxHealth);
		
		if (health <= 0) 
		{
			Died ();
		}
	}

	void Died()
	{
		if (transform.tag == "Enemy" && TargetObject.target == this.gameObject) 
		{
			transform.GetComponent<SetTarget> ().NotTargeted ();
			TargetObject.target = null;
		}
		Destroy (gameObject);
	}
}
