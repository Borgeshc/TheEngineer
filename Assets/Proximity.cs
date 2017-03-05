using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Proximity : MonoBehaviour 
{
	void OnTriggerStay(Collider other)
	{
		if (other.tag == "Player") 
		{
			GetComponentInParent<ProximityAI> ().InRange(other);
		}
	}
}
