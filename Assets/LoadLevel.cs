﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadLevel : MonoBehaviour 
{
	public string level;

	void OnTriggerEnter(Collider other)
	{
		if (other.tag == "Player") 
		{
			SceneManager.LoadScene (level);
		}
	}
}