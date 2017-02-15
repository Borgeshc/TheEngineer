using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour 
{
	public float attackRange;
	public float attackFreq;

	GameObject target;
	bool enemyInRange;
	bool hasTarget;
	bool attacking;
	RaycastHit hit;
	LineRenderer line;

	void Start()
	{
		line = GetComponent<LineRenderer> ();
		line.enabled = false;
	}

	void OnTriggerStay(Collider other)
	{
		print (other);
		if (other.tag == "Enemy") 
		{
			enemyInRange = true;
			if (!hasTarget) 
			{
				hasTarget = true;
				target = other.gameObject;
				print ("I have a target. The target is " + target);
			}
		} 
		else 
		{
			enemyInRange = false;
		}
	}

	void OnTriggerExit(Collider other)
	{
		if (target == other.gameObject) 
		{
			hasTarget = false;
			line.enabled = false;
			print ("I dont have a target");
		}
	}

	void Update()
	{
		if (target == null || target.activeInHierarchy == false) 
		{
			hasTarget = false;
			line.enabled = false;
		}

		if (hasTarget) 
		{
			transform.LookAt (target.transform);

			line.enabled = true;
			line.SetPosition (0, transform.position);
			line.SetPosition (1, target.transform.position);

			if(Physics.Raycast(transform.position, transform.forward, out hit, attackRange))
			{
				if (!attacking && hasTarget) 
				{
					attacking = true;
					StartCoroutine (Attack ());
				}
			}
		}
	}

	IEnumerator Attack()
	{
		print ("Attacking");
		yield return new WaitForSeconds (attackFreq);
		attacking = false;
	}
}
