using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vendor : MonoBehaviour 
{
	public GameObject vendor;
	public GameObject player;

	void Start()
	{
	}

	void Update()
	{
	}
	void OnTriggerEnter(Collider other)
	{
		if (other.tag == "Player") 
		{
			vendor.SetActive (true);
		}
	}

	void OnTriggerExit(Collider other)
	{
		if (other.tag == "Player") 
		{
			vendor.SetActive (false);
		}
	}

}
