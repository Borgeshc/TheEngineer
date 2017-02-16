using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour 
{
	GameObject target;
	float speed;
	int damage;
	bool applyingDamage;

	void Update()
	{
		transform.LookAt (target.transform);
		transform.Translate (transform.forward * speed * Time.deltaTime);
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

	public void ApplyVariables(GameObject _enemy, int _damage, float _speed)
	{
		target = _enemy;
		speed = _speed;
		damage = _damage;
	}
}
