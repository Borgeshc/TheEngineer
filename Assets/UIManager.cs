using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour 
{
	public KeyCode inventoryKeyCode;
	public GameObject inventory;
	public AudioClip inventorySound;
	public Text goldText;

	AudioSource source;

	void Start () 
	{
		source = GetComponent<AudioSource> ();
	}

	void Update () 
	{
		if (inventory.activeInHierarchy) 
		{
			goldText.text = "" + PlayerPrefs.GetInt("Gold");
		}
		if (Input.GetKeyDown (inventoryKeyCode)) 
		{
			source.clip = inventorySound;
			source.Play ();
			inventory.SetActive (!inventory.activeSelf);
		}
	}
}
