using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ExperienceManager : MonoBehaviour 
{
	public Image experienceBar;
	public float xpRequiredPerLevel;
	public int currentLevel = 1;
	public Text UIlevel;
	public GameObject levelUpEffect;
	public AudioSource source;
	Health health;

	void Start()
	{
		health = GameObject.Find ("Player").GetComponent<Health>();
		currentLevel = 1;
	}

	public void GainExperience(float experience)
	{
		if ((experienceBar.fillAmount + (experience / xpRequiredPerLevel)) > 1) 
		{
			source.Play ();
			currentLevel++;
			UIlevel.text = "" + currentLevel;
			xpRequiredPerLevel += xpRequiredPerLevel * 1.5f;
			health.GainHealth (health.maxHealth);
			float neededXP = xpRequiredPerLevel - experienceBar.fillAmount;
			experienceBar.fillAmount += neededXP / xpRequiredPerLevel;
			experienceBar.fillAmount = 0;
			experience -= neededXP;
			experienceBar.fillAmount += experience / xpRequiredPerLevel;

			StartCoroutine (LeveledUp ());
		}
		else
			experienceBar.fillAmount += experience / xpRequiredPerLevel;
	}

	IEnumerator LeveledUp()
	{
		levelUpEffect.SetActive (true);
		yield return new WaitForSeconds (3);
		levelUpEffect.SetActive (false);
	}
}
