using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour 
{
	public static bool isBlocking;
	public KeyCode keyCode;
	public GameObject shieldEffect;

	Animator anim;

	void Start () 
	{
		anim = GetComponent<Animator> ();
	}

	void Update () 
	{
		if (Input.GetKey (keyCode) && !SwingSword.isSwinging) 
		{
			Movement.canMove = false;
			isBlocking = true;
			shieldEffect.SetActive (true);
			anim.SetBool ("Block", true);
		} 
		else 
		{
			if (!Movement.canMove)
				Movement.canMove = true;
			
			isBlocking = false;
			shieldEffect.SetActive (false);
			anim.SetBool ("Block", false);
		}
	}
}
