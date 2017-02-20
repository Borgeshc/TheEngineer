﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RTSCamera : MonoBehaviour 
{
	public float dragSpeed = 2;
	private Vector3 dragOrigin;

	public bool cameraDragging = true;

	public float outerLeft = -10f;
	public float outerRight = 10f;
	public float outerUp = 10f;
	public float outerDown = 10f;


	void Update()
	{



		Vector2 mousePosition = new Vector2(Input.mousePosition.x, Input.mousePosition.y);

		float left = Screen.width * 0.2f;
		float right = Screen.width - (Screen.width * 0.2f);
		float down = Screen.height * 0.2f;
		float up = Screen.height - (Screen.height * 0.2f);

		if(mousePosition.x < left)
			cameraDragging = true;
		else if(mousePosition.x > right)
			cameraDragging = true;

		if (mousePosition.y < down)
			cameraDragging = true;
		else if (mousePosition.y > up)
			cameraDragging = true;

		if (cameraDragging) 
		{

			if (Input.GetMouseButtonDown(0))
			{
				dragOrigin = Input.mousePosition;
				return;
			}

			if (!Input.GetMouseButton(0)) return;

			Vector3 pos = Camera.main.ScreenToViewportPoint(Input.mousePosition - dragOrigin);
			Vector3 move = new Vector3(-pos.x * dragSpeed, 0, -pos.z * dragSpeed);

			if (move.x > 0f)
			{
				if(this.transform.position.x < outerRight)
				{
					transform.Translate(move, Space.World);
				}
			}
			else{
				if(this.transform.position.x > outerLeft)
				{
					transform.Translate(move, Space.World);
				}
			}

			if (move.z > 0f)
			{
				if(this.transform.position.z < outerUp)
				{
					transform.Translate(move, Space.World);
				}
			}
			else{
				if(this.transform.position.z > outerDown)
				{
					transform.Translate(move, Space.World);
				}
			}
		}
	}

 
}

//Automatic Screen Edge Movement
/*
public float scrollZone = 5;
public float scrollSpeed = 2;

public float xMax = 8;
public float xMin = 0;
public float yMax = 10;
public float yMin = 3;
public float zMax = 8;
public float zMin = 0;

private Vector3 desiredPositon;

void Start()
{
	desiredPositon = transform.position;
}

void Update()
{
	float x = 0, y = 0, z = 0;
	float speed = scrollZone * Time.deltaTime;

	if (Input.mousePosition.x < Screen.width)
		x -= speed;
	else if (Input.mousePosition.x > Screen.width - scrollZone)
		x += speed;

	if (Input.mousePosition.y < scrollZone)
		z -= speed;
	else if (Input.mousePosition.y > Screen.height - scrollZone)
		z += speed;

	y += -Input.GetAxis ("Mouse ScrollWheel") * (scrollSpeed * 2);

	Vector3 move = new Vector3 (x, y, z) + desiredPositon;
	move.x = Mathf.Clamp (move.x, xMin, xMax);
	move.y = Mathf.Clamp (move.y, yMin, yMax);
	move.z = Mathf.Clamp (move.z, zMin, zMax);
	desiredPositon = move;

	if (Input.GetMouseButton(0)) 
	{
		transform.position = Vector3.Lerp (transform.position, desiredPositon, 0.02f);
	}
}
*/
