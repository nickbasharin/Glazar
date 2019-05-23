using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GatchinaMap : MonoBehaviour {

	public Text rus;
	public Text eng;
	public GameObject getOutUI;
	public bool mapopened;
	int pavelNumber;
	public GameObject[] images;

	AudioSource playingAudio;

	void Start () {
		playingAudio = null;
	}
	
	void Update () {
		
	}

	public void SetPoint(int point){
		if ((point > 0) && (point < 13)) {
			foreach (GameObject g in images) {
				g.SetActive (false);
			}

			images [point - 1].SetActive (true);
		}
	}

	public void SetSubs(string rusText, string engText){
		rus.text = rusText;
		eng.text = engText;

	}

	public void OnOffGetOutPanel(bool onoff){
		getOutUI.SetActive (onoff);

	}

	public void MapOpened(bool o){
		mapopened = o;
	}

	public void PlayerManager(AudioSource audio, int pavelN){
		if (playingAudio == null) {
			playingAudio = audio;
			playingAudio.Play();
		} else {
			if (!playingAudio.Equals (audio)) {
				playingAudio.Stop ();
				playingAudio = audio;
				playingAudio.Play();

			}
		}
		pavelNumber = pavelN;
	}

	public void PlayPavel(){
		if (playingAudio != null) {
			playingAudio.Play ();
		}
	}
	public void StopPavel(){
			if (playingAudio != null) {
			playingAudio.Stop ();
			}		
	}

	public void PausePavel(){
		if (playingAudio != null) {
			playingAudio.Pause ();
		}
	}

	public int GetPlayingPavel(){
		return pavelNumber;

	}
}
