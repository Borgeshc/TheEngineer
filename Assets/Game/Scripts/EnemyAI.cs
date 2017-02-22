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

	Animator anim;

	void Start()
	{
		player = GameObject.FindGameObjectWithTag ("Player");
		playerHealth = player.GetComponent<Health> ();
		anim = GetComponentInChildren<Animator> ();
	}

	void Update()
	{
		if (player.activeInHierarchy) 
		{
			transform.LookAt (player.transform);
			if (Vector3.Distance(transform.position, player.transform.position) > stoppingDistance) {
				transform.Translate (Vector3.forward * speed * Time.deltaTime);
			}
		}
	}
}
