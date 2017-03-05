using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour 
{
	public static bool canMove;
	public float speed;
	public float rotationSpeed;
	Vector3 input;
	Vector3 targetRotation;

	CharacterController cc;
	Animator anim;

	void Start () 
	{
		cc = GetComponent<CharacterController> ();
		anim = GetComponent<Animator> ();
	}

	void FixedUpdate()
	{
		if (canMove && !SwingSword.isSwinging && !Charge.isCharging && !Block.isBlocking) 
		{
			input = new Vector3 (Input.GetAxis ("Horizontal"), 0, Input.GetAxis ("Vertical"));
			if (input.sqrMagnitude > 1f) 
			{
				input.Normalize ();
			}

			if (input != Vector3.zero) 
			{
				targetRotation = Quaternion.LookRotation (input).eulerAngles;
				anim.SetBool ("IsWalking", true);
				cc.SimpleMove (transform.forward * speed * Time.deltaTime);
			} else
				anim.SetBool ("IsWalking", false);

			transform.rotation = Quaternion.Slerp (transform.rotation, Quaternion.Euler (targetRotation.x,
				Mathf.Round (targetRotation.y / 45) * 45, targetRotation.z), Time.deltaTime * rotationSpeed);
		}
	}
}
