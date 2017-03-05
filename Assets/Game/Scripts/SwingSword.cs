using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwingSword : MonoBehaviour 
{
	public static bool isSwinging;
	public KeyCode keyCode;
	public float animationWaitTime;

	Animator anim;
	bool attacking;
	int animationType;

	public BoxCollider weaponDamage;
	CharacterController cc;
	float speed;
	Transform target;
	Vector3 lookPosition;
	Quaternion rotation;

	float turnSpeed = 10;

	void Start()
	{
		cc = GetComponent<CharacterController> ();
		speed = GetComponent<Movement> ().speed;
		anim = GetComponent<Animator> ();
		weaponDamage.enabled = false;
	}
	void FixedUpdate () 
	{
		if (Input.GetKey (keyCode) && !Block.isBlocking && !Charge.isCharging) 
		{
			if (TargetObject.target != null) 
			{
				target = TargetObject.target.transform;

				lookPosition = target.transform.position - transform.position;
				lookPosition.y = 0;
				rotation = Quaternion.LookRotation (lookPosition);
				transform.rotation = Quaternion.Slerp (transform.rotation, rotation, Time.deltaTime * turnSpeed);
			}

			if (!attacking) 
			{
				Movement.canMove = false;
				if (TargetObject.target != null && Vector3.Distance (transform.position, TargetObject.target.transform.position) > 1) 
				{

					anim.SetBool ("IsWalking", true);
					cc.SimpleMove (transform.forward * speed * Time.deltaTime);
				} 
				else 
				{
					anim.SetBool ("IsWalking", false);

					attacking = true;
					isSwinging = true;
					weaponDamage.enabled = true;
					animationType = Random.Range (1, 4);
					anim.SetInteger ("SwingSword", animationType);
					StartCoroutine (Attack ());
				}
			}
		} 
		else 
		{
			if(!Movement.canMove)
			Movement.canMove = true;
			
			isSwinging = false;
			weaponDamage.enabled = false;
		}
	}

	IEnumerator Attack()
	{
		yield return new WaitForSeconds (animationWaitTime);
		anim.SetInteger ("SwingSword", 0);
		attacking = false;
	}
}
