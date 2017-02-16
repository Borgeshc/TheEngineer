using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaceTurret : MonoBehaviour 
{
	public LayerMask layerMask;
	public GameObject turret;
	public int maxTurrets;
	int placedTurrets;
	bool placingTurret;
	public bool canPlace;
	public List<GameObject> activeTurrets;
	RaycastHit hit;


	void Update () 
	{
		if (Input.GetKeyDown (KeyCode.E))
		{
			canPlace = !canPlace;
		}

		if (canPlace && Input.GetMouseButtonDown(0)) 
		{
			if(Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit, 100, layerMask))
			{
				if(hit.collider.name == "Ground")
					{
						if (placedTurrets < maxTurrets && !placingTurret) 
						{
							placingTurret = true;
							placedTurrets++;
							GameObject newTurret = Instantiate (turret, transform.position, transform.rotation) as GameObject;
							activeTurrets.Add (newTurret);
							newTurret.transform.position = new Vector3 (hit.point.x, newTurret.transform.localScale.y * .05f, hit.point.z);
							placingTurret = false;	
						} 
						else if (activeTurrets.Count == maxTurrets && !placingTurret) 
						{
							//Overrite and destroy the old turret
							Destroy (activeTurrets [0]);
							activeTurrets.Remove (activeTurrets [0]);
							placedTurrets--;

							//Spawn the new turret
							placingTurret = true;
							placedTurrets++;
							GameObject newTurret = Instantiate (turret, transform.position, transform.rotation) as GameObject;
							activeTurrets.Add (newTurret);
							newTurret.transform.position = new Vector3 (hit.point.x, newTurret.transform.localScale.y * .05f, hit.point.z);
							placingTurret = false;	
						}

				}
			}
		}
	}
}
