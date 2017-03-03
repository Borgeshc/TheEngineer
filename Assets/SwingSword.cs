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

	void Start()
	{
		anim = GetComponent<Animator> ();
	}
	void Update () 
	{
		if (Input.GetKey (keyCode) && !Block.isBlocking) 
		{
			if (!attacking) 
			{
				attacking = true;
				isSwinging = true;
				animationType = Random.Range (1, 4);
				print (animationType);
				anim.SetInteger ("SwingSword", animationType);
				StartCoroutine (Attack ());
			}
		} 
		else
			isSwinging = false;
	}

	IEnumerator Attack()
	{
		yield return new WaitForSeconds (animationWaitTime);
		anim.SetInteger ("SwingSword", 0);
		attacking = false;
	}
}
