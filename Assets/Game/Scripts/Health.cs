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
	public float health;
	public Image deathPanel;
	public Text deathText;
	float timer;

	void Start()
	{
		health = maxHealth;
	}

	public void TookDamage(int damage)
	{
		if (transform.tag == "Player" && Block.isBlocking)
		{
			return;
		}
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
		if (transform.tag == "Enemy" && TargetObject.target == this.gameObject) {
			transform.GetComponent<SetTarget> ().NotTargeted ();
			TargetObject.target = null;
		} 
		else if (transform.tag == "Player") 
		{
			StartCoroutine (Fade ());
		}

		deathAnim.SetBool ("Died", true);
		yield return new WaitForSeconds (deathTime);
		Destroy (gameObject);
	}

	public void GainHealth(float healthGain)
	{
		if (health + healthGain < maxHealth)
			health += healthGain;
		else
			health = maxHealth;
	}

	IEnumerator Fade()
	{
		yield return new WaitUntil(FadingIn);
	}
	bool FadingIn()
	{
		deathPanel.color = Color.Lerp(deathPanel.color, new Color(0,0,0, .75f), Time.deltaTime * .5f);
		deathText.color = Color.Lerp(deathText.color, Color.red, Time.deltaTime * .25f);
		timer += 1 / 60.0f;
		if (deathPanel.color == Color.clear)
			return true;
		else
			return false;
	}
}
