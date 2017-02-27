using UnityEngine;
using System.Collections;

public class MovementScript : MonoBehaviour
{
	public static bool canMove;
	public static bool canRotate;
	public Animator anim;
	public float speed = 10.0f;
	CharacterController cc;
	public Transform gameObjectToRotate;

	float sensitivity = 2;
	Vector3 middleOfScreen;

	
	void Start()
	{
		canMove = true;
		canRotate = true;
		//Cursor.visible = false;
		cc = GetComponent<CharacterController>();
		middleOfScreen = new Vector3(Screen.width/2, Screen.height/2, 0f);
	}

	void Update()
	{
		if (canRotate) 
		{
			Vector3 camVec = Input.mousePosition - middleOfScreen;
			Vector3 flipped = new Vector3 (camVec.x, 0f, camVec.y);
			gameObjectToRotate.LookAt (flipped);
		}
		if (Input.GetKey (KeyCode.LeftShift) || !canMove) 
		{
			anim.SetBool ("IsWalking", false);
			cc.Move (Vector3.zero);
		} 
		else 
		{
			if (Input.mousePosition.x > middleOfScreen.x + (Screen.width * .3f)
			   || Input.mousePosition.y > middleOfScreen.y + (Screen.height * .3f)
			   || Input.mousePosition.x < middleOfScreen.x + (Screen.width * -.3f)
			   || Input.mousePosition.y < middleOfScreen.y + (Screen.height * -.3f)) 
			{
				anim.SetBool ("IsWalking", true);
				cc.Move (transform.forward * speed * Time.deltaTime);
			} else 
			{
				anim.SetBool ("IsWalking", false);
				cc.Move (Vector3.zero);
			}
		}
	}	
}
