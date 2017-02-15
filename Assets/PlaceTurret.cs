using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaceTurret : MonoBehaviour 
{
	public GameObject turret;
	public int maxTurrets;
	int placedTurrets;
	bool placingTurret;

	void Update () 
	{
		if (Input.GetKeyDown (KeyCode.E) && placedTurrets < maxTurrets)
		{
			if (!placingTurret) 
			{
				placingTurret = true;
				placedTurrets++;
				GameObject newTurret = Instantiate (turret, transform.position, transform.rotation) as GameObject;
				newTurret.transform.position = new Vector3 (newTurret.transform.position.x, newTurret.transform.localScale.y * .5f, transform.forward.z + 2);
				placingTurret = false;	
			}
		}
	}
}
