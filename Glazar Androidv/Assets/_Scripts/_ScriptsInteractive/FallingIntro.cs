using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingIntro : MonoBehaviour {
	public GameObject back;
	public GameObject hands;

	public float fallSpeed;
	float dy;
	float y;
	// Use this for initialization
	void Start () {
		StartCoroutine (vidos ());
		hands.SetActive (false);
		dy = 0;
		y = 0;
	}	
	// Update is called once per frame
	void Update () {
		y += dy;
		transform.rotation = Quaternion.Euler (y, transform.rotation.y, transform.rotation.z);
		if ((y < -90) || (y > 90)) {
			hands.SetActive (true);

			Destroy (back);
			Destroy (this.gameObject);
		}

	}

	IEnumerator vidos(){
		yield return new WaitForSeconds (3.0f);
		fall ();
	}

	public void fall(){
		dy = fallSpeed*Time.deltaTime;
	}
}
