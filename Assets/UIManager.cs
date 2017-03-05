using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour 
{
	public KeyCode inventoryKeyCode;
	public GameObject inventory;
	public AudioClip inventorySound;

	AudioSource source;

	void Start () 
	{
		source = GetComponent<AudioSource> ();
	}

	void Update () 
	{
		if (Input.GetKeyDown (inventoryKeyCode)) 
		{
			source.clip = inventorySound;
			source.Play ();
			inventory.SetActive (!inventory.activeSelf);
		}
	}
}
