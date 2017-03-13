using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillManager : MonoBehaviour 
{
	public Text skillPointsText;
	public GameObject notEnough;
	int skillPoints;
	bool canBuy;

	void Start()
	{
		skillPoints = PlayerPrefs.GetInt ("SkillPoints");
		skillPointsText.text = "Skill Points: " + skillPoints;
	}

	public void GainSkillPoint()
	{
		skillPoints++;
		PlayerPrefs.SetInt ("SkillPoints", skillPoints);
		skillPointsText.text = "Skill Points: " + skillPoints;
	}

	void Update()
	{
		if (DevMode.devMove) 
		{
			if (Input.GetKeyDown (KeyCode.R)) 
			{
				PlayerPrefs.SetInt ("SkillPoints", 0);
				skillPoints = PlayerPrefs.GetInt ("SkillPoints");
				skillPointsText.text = "Skill Points: " + skillPoints;
			}
		}
	}

	public void SpendSkillPoints(int amount)
	{
		if (skillPoints - amount >= 0)
		{
			canBuy = true;
			skillPoints -= amount;
			PlayerPrefs.SetInt ("SkillPoints", skillPoints);
			skillPointsText.text = "Skill Points: " + skillPoints;
			StartCoroutine (CanBuy ());
		} 
		else 
		{
			StartCoroutine (NotEnough ());
		}
	}

	public void ChangeIconColor(Image skillIcon)
	{
		if (canBuy)
		{
			print ("Changed COlored");
			skillIcon.color = Color.white;
		}
	}

	public void UnlockText(Text skillPointsText)
	{
		if(canBuy)
		skillPointsText.text = "3/3";
	}

	IEnumerator NotEnough()
	{
		notEnough.SetActive (true);
		yield return new WaitForSeconds (2);
		notEnough.SetActive (false);
	}

	IEnumerator CanBuy()
	{
		yield return new WaitForSeconds (.5f);
		canBuy = false;
	}
}
