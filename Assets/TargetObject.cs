using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetObject : MonoBehaviour 
{
	public static GameObject target;
	RaycastHit hit;
	void Update () 
	{
		if (Input.GetMouseButtonDown (0)) 
		{
			if (Physics.Raycast (Camera.main.ScreenPointToRay (Input.mousePosition), out hit, 1000)) 
			{
				if (hit.collider.tag == "Enemy") 
				{
					if (target != null) 
					{
						target.GetComponent<SetTarget> ().NotTargeted ();
						target = null;
					}
					hit.transform.GetComponent<SetTarget> ().Targeted ();
					target = hit.transform.gameObject;
				} 
				else if (hit.collider.tag == "Enviornment" || hit.collider.tag == "Ground") 
				{
					if (target != null) 
					{
						target.GetComponent<SetTarget> ().NotTargeted ();
						target = null;
					}
				}
			}
		}
	}
}
