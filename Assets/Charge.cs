using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Charge : MonoBehaviour 
{
	public KeyCode keyCode;
	public GameObject fire;
	public GameObject fireSpawn;
	public int chargeDistance;
	public float chargeSpeed;
	GameObject target;
	bool selectingTarget;
	public Texture2D cursorSelect;
	Animator anim;
	RaycastHit hit;
	bool spawning;

	void Start () 
	{
		anim = GetComponent<Animator> ();
	}

	void Update () 
	{
		if (TargetObject.target != null && Input.GetKey (keyCode)) 
		{	

			anim.SetBool ("Charge", true);
			transform.LookAt (TargetObject.target.transform);
			if (Vector3.Distance (transform.position, TargetObject.target.transform.position) > 2) 
			{
				if (!spawning) 
				{
					spawning = true;
					StartCoroutine (SpawnFlame ());
				}
				Movement.canMove = false;
				GetComponent<CharacterController> ().SimpleMove (transform.forward * chargeSpeed * Time.deltaTime);
			}
		} 
		else 
		{
			anim.SetBool ("Charge", false);
		}
	}

	IEnumerator SpawnFlame()
	{
		Instantiate (fire, fireSpawn.transform.position, fireSpawn.transform.rotation);
		yield return new WaitForSeconds (.01f);
		spawning = false;
	}
}
