using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransformAnim : MonoBehaviour {

	public Animator trans;
	public AudioSource tranSound;
/*	public Animator ded;
	public AudioSource dedSound;*/

	public float speed;
	bool stop;
	//float userSpeed;
	// Use this for initialization
	void Start () {
		stop = true;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void GoRobot(){
		if (trans.speed == 0) {
			SetSpeed (2.0f);
		}
		else trans.SetBool ("tr", true);
		tranSound.Play ();

	}

	public void GoStop(){
		if (stop) {
			SetSpeed (0.0f);
			tranSound.Pause ();
	
		} else {
			SetSpeed (2.0f);
			tranSound.Play ();
		}
		stop = !stop;

	}

	public void GoCar(){
		trans.SetBool ("tr", false);
	}

	public void SetSpeed(float spee){
		trans.speed = spee;
	//	ded.speed = spee;
	//	if (spee != 0) speed = spee;
	}

	public void GoDed(){

	/*	if (!ded.GetBool("tr")) dedSound.Play ();
		ded.SetBool ("tr", true);*/

	}
	/*public void StopDed(){
		SetSpeed (0.0f);
		dedSound.Pause ();

	}*/


}
