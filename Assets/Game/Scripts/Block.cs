using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Block : MonoBehaviour 
{
	public static bool isBlocking;
	public KeyCode keyCode;
	public GameObject shieldEffect;
	public Image blockingMeter;

	Animator anim;
	bool exhausted;

	void Start () 
	{
		anim = GetComponent<Animator> ();
		blockingMeter.enabled = false;
	}

	void Update () 
	{
		if (Input.GetKey (keyCode) && !SwingSword.isSwinging && !exhausted && !Charge.isCharging) 
		{
			blockingMeter.enabled = true;
			blockingMeter.fillAmount -= (Time.deltaTime * .5f);

			if (blockingMeter.fillAmount <= 0)
				exhausted = true;
			
			Movement.canMove = false;
			isBlocking = true;
			shieldEffect.SetActive (true);
			anim.SetBool ("Block", true);
		} 
		else 
		{

			if (!Movement.canMove)
				Movement.canMove = true;
			
				if (blockingMeter.fillAmount >= 1) 
				{
					blockingMeter.enabled = false;
					exhausted = false;
				}
			
			isBlocking = false;
			shieldEffect.SetActive (false);
			anim.SetBool ("Block", false);

			blockingMeter.fillAmount += Time.deltaTime;
		}
	}
}
