using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BloodBombProjectile : MonoBehaviour 
{
	public float speed;
	public int damage;
	bool applyingDamage;
	GameObject player;

	void Start()
	{
		player = GameObject.Find ("Player");
	}
	void Update()
	{
		transform.Translate (player.transform.forward * speed * Time.deltaTime);
	}

	void OnTriggerEnter(Collider other)
	{
		if (other.tag == "Enemy") 
		{
			if (!applyingDamage) 
			{
				applyingDamage = true;
				other.GetComponent<Health> ().TookDamage (damage);
				Destroy (gameObject);
			}
		}
	}
}
