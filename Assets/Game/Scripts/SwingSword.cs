using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SwingSword : MonoBehaviour 
{
	public KeyCode keyCode;
	public float animationWaitTime;
	public AudioClip[] swishSounds;
	public float swishSoundFreq;
	public float furyPerAttack;
	public int damage;
	public float furyGain;
	public ResourceManager resourceManager;


	Animator anim;
	bool attacking;
	int animationType;

	CharacterController cc;
	public float speed;
	Transform target;
	Vector3 lookPosition;
	Quaternion rotation;

	public AudioSource source;
	bool playingSound;

	float turnSpeed = 10;
	bool damaging;
	AbilityManager abilityManager;
	bool isSwinging;
	bool dealingDamage;

	void Start()
	{
		abilityManager = GetComponent<AbilityManager> ();
		cc = GetComponent<CharacterController> ();
		speed = GetComponent<Movement> ().speed;
		anim = GetComponent<Animator> ();
	}
	void FixedUpdate () 
	{
		if (Input.GetKeyDown (keyCode) && !abilityManager.abilityInProgress) 
		{
			abilityManager.abilityInProgress = true;
			isSwinging = true;
		}
		if (abilityManager.abilityInProgress && isSwinging) {
			if (TargetObject.target != null) {
				target = TargetObject.target.transform;

				lookPosition = target.transform.position - transform.position;
				lookPosition.y = 0;
				rotation = Quaternion.LookRotation (lookPosition);
				transform.rotation = Quaternion.Slerp (transform.rotation, rotation, Time.deltaTime * turnSpeed);
			}

			if (!attacking) {
				Movement.canMove = false;
				if (TargetObject.target != null && Vector3.Distance (transform.position, TargetObject.target.transform.position) > 1 && !Charge.isCharging) {

					anim.SetBool ("IsWalking", true);
					cc.SimpleMove (transform.forward * speed * Time.deltaTime);
				} else {
					anim.SetBool ("IsWalking", false);

					attacking = true;
					animationType = Random.Range (1, 4);

					if (!playingSound) {
						playingSound = true;
						StartCoroutine (PlaySound ());
					}
						
					StartCoroutine (Attack ());
					if (!dealingDamage) {
						dealingDamage = true;
						StartCoroutine (DealDamage ());
					}
				}
			}
		
		} 
		else 
		{
			if(!Movement.canMove)
			Movement.canMove = true;
			
		}

		if (Input.GetKeyUp (keyCode)) {
			abilityManager.abilityInProgress = false;

			isSwinging = false;
		}
	}

	IEnumerator Attack()
	{
		anim.SetInteger ("SwingSword", animationType);
		yield return new WaitForSeconds (animationWaitTime);

		anim.SetInteger ("SwingSword", 0);
		attacking = false;
	}

	IEnumerator DealDamage()
	{

		yield return new WaitForSeconds (animationWaitTime * .7f);
			if(TargetObject.target != null)
			TargetObject.target.GetComponent<Health> ().TookDamage (damage);
		yield return new WaitForSeconds (animationWaitTime * .3f);

		dealingDamage = false;
	}

	IEnumerator PlaySound()
	{
		yield return new WaitForSeconds (.3f);

			source.clip = swishSounds [Random.Range (0, swishSounds.Length)];
			source.Play ();

		resourceManager.GainResource (furyGain);
		yield return new WaitForSeconds (swishSoundFreq);
		playingSound = false;
	}
}
