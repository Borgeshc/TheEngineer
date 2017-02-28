using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

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
	public Animator anim;
	NavMeshAgent nav;

	void Start()
	{
		nav = GetComponent<NavMeshAgent> ();
		player = GameObject.FindGameObjectWithTag ("Player");
		playerHealth = player.GetComponent<Health> ();
		myHealth = GetComponent<Health> ();
	}

	void Update()
	{
		if (myHealth.health > 0 && player != null) {
			if (player.activeInHierarchy && myHealth.health > 0) {
				transform.LookAt (player.transform);
				if (Vector3.Distance (transform.position, player.transform.position) > stoppingDistance) {
					//nav.Move (transform.forward * speed * Time.deltaTime);
					nav.SetDestination (player.transform.position);
					//transform.Translate (Vector3.forward * speed * Time.deltaTime);
				} else {
					print ("Im close");
					if (!attacking) {
						attacking = true;
						StartCoroutine (Attack ());
					}
				}
			}
		} else
			nav.Stop ();
	}

	IEnumerator Attack()
	{
		print ("Im attacking");
		anim.SetBool ("Attack", true);
		player.GetComponent<Health> ().TookDamage (damage);
		yield return new WaitForSeconds (attackFrequency);
		attacking = false;
	}
}
