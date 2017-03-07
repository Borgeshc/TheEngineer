using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetObject : MonoBehaviour 
{
	public static GameObject target;
	public LayerMask layermask;
	public Texture2D cursorMain;
	public Texture2D cursorAttack;
	public Texture2D cursorLoot;

	public GameObject vendorObject;
	RaycastHit hit;
	GameObject goldTarget;

	Ray newRay;
	void Start()
	{
		Cursor.SetCursor(cursorMain, new Vector2(cursorMain.width /2, cursorMain.height / 2), CursorMode.Auto);
	}

	void Update () 
	{
		 newRay = Camera.main.ScreenPointToRay (Input.mousePosition);
		if (Physics.Raycast (Camera.main.ScreenPointToRay (Input.mousePosition), out hit, 1000, layermask)) 
		{

			if (hit.collider.tag == "Enemy") 
			{
				Cursor.SetCursor (cursorAttack, new Vector2 (cursorAttack.width / 2, cursorAttack.height / 2), CursorMode.Auto);
				if (target != null) {
					target.GetComponent<SetTarget> ().NotTargeted ();
					target = null;
				}
				hit.transform.GetComponent<SetTarget> ().Targeted ();
				target = hit.transform.gameObject;
			}

			if (hit.collider.tag == "Item") 
			{
				Cursor.SetCursor (cursorLoot, new Vector2 (cursorLoot.width / 2, cursorLoot.height / 2), CursorMode.Auto);
				if (Input.GetKeyDown (KeyCode.Mouse0) && hit.transform.name == "GoldItem") {
					hit.transform.GetComponent<PickUpGold> ().PickUp ();
				}
			}
		}
        else
            Cursor.SetCursor(cursorMain, new Vector2(cursorMain.width / 2, cursorMain.height / 2), CursorMode.Auto);
    }
}
