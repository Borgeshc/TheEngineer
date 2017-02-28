using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour 
{
	GameObject player;
	public float speed;
	public float stoppingDistance;

	public int damage;
	public float attackFrequency;

	Health playerHealth;
	bool attacking;
	Health myHealth;
	Animator anim;

	void Start()
	{
		player = GameObject.FindGameObjectWithTag ("Player");
		playerHealth = player.GetComponent<Health> ();
		myHealth = GetComponent<Health> ();
		anim = GetComponentInChildren<Animator> ();
	}

	void Update()
	{
		if (player.activeInHierarchy && myHealth.health > 0)
		{
			transform.LookAt (player.transform);
			if (Vector3.Distance (transform.position, player.transform.position) > stoppingDistance) {
				transform.Translate (Vector3.forward * speed * Time.deltaTime);
			} 
			else 
			{
				if (!attacking) 
				{
					attacking = true;
					StartCoroutine (Attack ());
				}
			}
		}
	}

	IEnumerator Attack()
	{
		anim.SetBool ("Attack", true);
		player.GetComponent<Health> ().TookDamage (damage);
		yield return new WaitForSeconds (attackFrequency);
		attacking = false;
	}
}
