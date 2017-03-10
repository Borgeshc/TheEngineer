using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CleaveDamage : MonoBehaviour 
{
	public int cleaveDamage;

	void OnTriggerEnter(Collider other)
	{
		if (other.tag == "Enemy") 
		{
			other.GetComponent<Health> ().TookDamage(cleaveDamage);
		}
	}
}
