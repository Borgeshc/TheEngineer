using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BloodDrain : MonoBehaviour 
{
	public GameObject bloodDrainObject;
	public int damage;
	public float attackFreq;
	LineRenderer line;
	public Animator anim;
	bool liningAnim;
	bool attacking;
	AudioSource source;

	void Start()
	{
		source = bloodDrainObject.GetComponent<AudioSource> ();
		line = bloodDrainObject.GetComponent<LineRenderer> ();
		line.enabled = false;
	}
	void Update () 
	{
		if (Input.GetKey (KeyCode.Mouse0) && TargetObject.target != null) 
		{
			MovementScript.canMove = false;
			transform.LookAt (TargetObject.target.transform);
			anim.SetBool ("BloodDrain", true);
			if (!liningAnim) 
			{
				liningAnim = true;
				StartCoroutine(LineWithAnim());
			}
			line.enabled = true;
			line.SetPosition (0, bloodDrainObject.transform.position);
			line.SetPosition (1, TargetObject.target.transform.position + new Vector3(0,TargetObject.target.transform.localScale.y *.5f, 0));
			if (!source.isPlaying) 
			{
				source.Play ();
			}

			if (!attacking) 
			{
				attacking = true;
				StartCoroutine (Attack (TargetObject.target));
			}
		}
		else 
		{
			MovementScript.canMove = true;
			anim.SetBool ("BloodDrain", false);
			line.enabled = false;
		}
	}

	IEnumerator LineWithAnim()
	{
		yield return new WaitForSeconds (.5f);
		liningAnim = false;
	}


	IEnumerator Attack(GameObject enemy)
	{
		enemy.GetComponent<Health> ().TookDamage (damage);
		yield return new WaitForSeconds (attackFreq);
		attacking = false;
	}
}
