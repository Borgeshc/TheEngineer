using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootProjectile : MonoBehaviour 
{
	public KeyCode keycode;
	public GameObject projectile;
	public GameObject gunBarrel;
	public int damage;
	public float projectileSpeed;
	public float fireFrequency;
	[Space]
	public bool ammoBased;
	public int ammo;
	public bool reload;
	public float reloadSpeed;
	public bool cooldown;
	public bool onCooldown;
	public float cooldownTime;

	[Space]
	public bool heatBased;

	//Projectile Shooting
	bool shooting;
	//Ammo Based
	int currentAmmo;
	bool reloading;

	void Start () 
	{
		currentAmmo = ammo;
	}

	void Update () 
	{
		if (Input.GetKeyDown (keycode)) 
		{
			if(!shooting)
				StartCoroutine(Shoot ());
		}
	}

	IEnumerator Shoot()
	{
		shooting = true;

		if (ammoBased && !reloading || ammoBased && !onCooldown) 
		{
			currentAmmo--;
			GameObject projectileClone = Instantiate (projectile, gunBarrel.transform.position, gunBarrel.transform.rotation) as GameObject;
			yield return new WaitForSeconds (fireFrequency);

			if (currentAmmo <= 0) 
			{
				if (reload)	//Reloading is enabled
					StartCoroutine (Reload ());
			}
		} 
		else if (!ammoBased && !heatBased) 
		{
			GameObject projectileClone = Instantiate (projectile, gunBarrel.transform.position, gunBarrel.transform.rotation) as GameObject;
			yield return new WaitForSeconds (fireFrequency);
		}
		shooting = false;
	}

	IEnumerator Reload()
	{
		reloading = true;
		yield return new WaitForSeconds (reloadSpeed);
		currentAmmo = ammo;
		reloading = false;
	}
}
