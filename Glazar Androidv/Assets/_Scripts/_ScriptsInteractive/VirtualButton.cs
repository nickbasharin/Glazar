using UnityEngine;
using System.Collections;

public class VirtualButton : MonoBehaviour {

	int buttonMask;
	float camRayLength = 1000f;
	public GameObject galka;
	public LayerMask button;


	void Awake () {
		buttonMask = 	button;

	}

	void FixedUpdate () {

		foreach(  Touch touch in Input.touches ){
			if (touch.phase == TouchPhase.Began) {
				move (touch);

			}
		}

	}

	void move(Touch touch){
		Ray camRay = Camera.main.ScreenPointToRay (Input.mousePosition);
		RaycastHit floorHit;
		if  (Physics.Raycast (camRay, out floorHit, camRayLength, buttonMask)){
			galka.SetActive (!galka.activeSelf);
		}
	}
}
