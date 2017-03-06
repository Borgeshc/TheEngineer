using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetObject : MonoBehaviour 
{
	public static GameObject target;
	public LayerMask layermask;
	public Texture2D cursorTexture;
	RaycastHit hit;
	GameObject goldTarget;

	Ray newRay;
	void Start()
	{
		Cursor.SetCursor(cursorTexture, new Vector2(cursorTexture.width /2, cursorTexture.height / 2), CursorMode.Auto);
	}

	void Update () 
	{
		 newRay = Camera.main.ScreenPointToRay (Input.mousePosition);
		if (Physics.Raycast (Camera.main.ScreenPointToRay (Input.mousePosition), out hit, 1000, layermask)) 
		{
			if (hit.collider.tag == "Enemy") 
			{
					if (target != null) 
					{
						target.GetComponent<SetTarget> ().NotTargeted ();
						target = null;
					}
					hit.transform.GetComponent<SetTarget> ().Targeted ();
					target = hit.transform.gameObject;

			} 
			else if (hit.collider.tag == "Enviornment" || hit.collider.tag == "Ground") 
			{
				if (target != null) 
				{
					target.GetComponent<SetTarget> ().NotTargeted ();
					target = null;
				}
			}

			if (hit.collider.tag == "Item") 
			{
				if (Input.GetKeyDown (KeyCode.Mouse0) && hit.transform.name == "GoldItem") 
				{
					hit.transform.GetComponent<PickUpGold> ().PickUp ();
				}
					
			}
		}
	}
}
