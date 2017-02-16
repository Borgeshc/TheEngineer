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

	void Start()
	{
		player = GameObject.Find ("Player");
		playerHealth = player.GetComponent<Health> ();
	}

	void Update()
	{
		if (player.activeInHierarchy && player != null) {
			transform.LookAt (player.transform);
			if (transform.position.z - player.transform.position.z > stoppingDistance) {
				transform.Translate (Vector3.forward * speed * Time.deltaTime);
			}
		}
	}
}
