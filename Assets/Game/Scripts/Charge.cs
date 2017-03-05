using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Charge : MonoBehaviour 
{
	public static bool isCharging;
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

	Vector3 lookPosition;
	Quaternion rotation;

	float turnSpeed = 10;

	void Start () 
	{
		anim = GetComponent<Animator> ();
	}

	void FixedUpdate () 
	{
		if (TargetObject.target != null && Input.GetKey (keyCode) && !SwingSword.isSwinging) 
		{	
			isCharging = true;
			anim.SetBool ("Charge", true);
			if (Vector3.Distance (transform.position, TargetObject.target.transform.position) > .5f) 
			{
				if (!spawning) 
				{
					spawning = true;
					StartCoroutine (SpawnFlame ());
				}
				Movement.canMove = false;

				lookPosition = TargetObject.target.transform.position - transform.position;
				lookPosition.y = 0;
				rotation = Quaternion.LookRotation (lookPosition);
				transform.rotation = Quaternion.Slerp (transform.rotation, rotation, Time.deltaTime * turnSpeed);

				GetComponent<CharacterController> ().SimpleMove (transform.forward * chargeSpeed * Time.deltaTime);
			}
		} 
		else 
		{
			isCharging = false;
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
