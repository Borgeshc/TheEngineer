using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ExperienceManager : MonoBehaviour 
{
	public Image experienceBar;
	public float xpRequiredPerLevel;
	public int currentLevel;
	public GameObject levelUpEffect;
	Health health;

	void Start()
	{
		health = GameObject.Find ("Player").GetComponent<Health>();
	}

	public void GainExperience(float experience)
	{
		if ((experienceBar.fillAmount + (experience / xpRequiredPerLevel)) > 1) 
		{
			currentLevel++;
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
