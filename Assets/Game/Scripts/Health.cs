using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour 
{
	public Animator anim;
	public float deathTime;
	public float maxHealth;
	public bool hasHealthBar;
	public Image healthBar;
	public float health;
	public Image deathPanel;
	public Text deathText;
	float timer;
	bool hitEffect;
	[HideInInspector]
	public bool isDead;
	CapsuleCollider collision;
	public AudioClip[] hitEffectSounds;
	public AudioSource source;
	public float experienceWorth;

	ExperienceManager xpManager;

	void Start()
	{
		health = maxHealth;
		xpManager = GameObject.Find ("GameManager").GetComponent<ExperienceManager> ();
		collision = GetComponent<CapsuleCollider> ();
	}

	public void TookDamage(int damage)
	{
		if (transform.tag == "Player" && Block.isBlocking) {
			return;
		} 

		health -= damage;		

		if (hasHealthBar)
			healthBar.fillAmount = (health / maxHealth);

		if (health <= 0) 
		{
			anim.SetBool ("Died", true);
			StartCoroutine(Died ());
		}

		if (transform.tag == "Enemy") 
		{
			if (!hitEffect) 
			{
				source.clip = hitEffectSounds [Random.Range (0, hitEffectSounds.Length)];
				source.Play ();
				hitEffect = true;
				anim.SetBool ("Hit", true);
				StartCoroutine (HitEffect ());
			}
		}
	}

	IEnumerator Died()
	{
		isDead = true;

		if (transform.tag == "Enemy") 
		{
			xpManager.GainExperience (experienceWorth);
			anim.SetLayerWeight (0, 0);
			anim.SetLayerWeight (2, 0);
			anim.SetLayerWeight (3, 0);
			collision.enabled = false;
			if(TargetObject.target == this.gameObject)
			{
				transform.GetComponent<SetTarget> ().NotTargeted ();
				TargetObject.target = null;
			}
		} 
		else if (transform.tag == "Player") 
		{
			StartCoroutine (Fade ());
		}

		yield return new WaitForSeconds (deathTime);
		Destroy (gameObject);
	}

	public void GainHealth(float healthGain)
	{
		if (health + healthGain < maxHealth) 
		{
			health += healthGain;
			healthBar.fillAmount = (health / maxHealth);
		} 
		else 
		{
			health = maxHealth;
			healthBar.fillAmount = (health / maxHealth);
		}
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

	IEnumerator HitEffect()
	{
		yield return new WaitForSeconds (.3f);
		anim.SetBool ("Hit", false);
		hitEffect = false;
	}
}
