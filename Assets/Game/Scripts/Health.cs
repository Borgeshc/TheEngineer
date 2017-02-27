using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour 
{
	public Animator deathAnim;
	public float deathTime;
	public float maxHealth;
	public bool hasHealthBar;
	public Image healthBar;
	[HideInInspector]
	public float health;

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
			StartCoroutine(Died ());
		}
	}

	IEnumerator Died()
	{
		if (transform.tag == "Enemy" && TargetObject.target == this.gameObject) 
		{
			transform.GetComponent<SetTarget> ().NotTargeted ();
			TargetObject.target = null;
		}

		deathAnim.SetBool ("Died", true);
		yield return new WaitForSeconds (deathTime);
		Destroy (gameObject);
	}
}
