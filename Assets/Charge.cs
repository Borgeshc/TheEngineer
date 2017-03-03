using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Charge : MonoBehaviour 
{
	public KeyCode keyCode;
	GameObject target;
	bool selectingTarget;
	public Texture2D cursorSelect;

	void Start () 
	{
		
	}

	void Update () 
	{
		if (Input.GetKeyDown (keyCode)) 
		{
			selectingTarget = !selectingTarget;
		}

		if (selectingTarget)
			Cursor.SetCursor (cursorSelect);
		else
			Cursor.SetCursor (null);
	}
}
