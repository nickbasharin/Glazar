using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Preloader : MonoBehaviour {
    // Use this for initialization
    private SpriteRenderer img;
    private TextMesh txt;
    private Animation anm;
	bool load;
	WWW www1;

	void Start () {
        img = gameObject.GetComponentInChildren<SpriteRenderer>();
        txt = gameObject.GetComponentInChildren<TextMesh>();
        anm = gameObject.GetComponentInChildren<Animation>();
        RefreshOff();
    }
	
	// Update is called once per frame
	void Update () {
		
		if ((load)&&(www1!=null)) 		txt.text = "Загрузка "+ Mathf.RoundToInt(www1.progress*100)+"%";

	}
    public void Loading() {
        img.gameObject.SetActive(true);
        txt.gameObject.SetActive(false);
        anm.enabled = true;

    }

    public void RefreshOff() {
        img.gameObject.SetActive(false);
        txt.gameObject.SetActive(false);
        anm.enabled = false;
    }
    public void Loaded() {
        Destroy(gameObject);
    }
    public void CantLoad() {
        img.gameObject.SetActive(false);
        load = false;
        txt.text = "Ошибка загрузки";
        txt.gameObject.SetActive(true);
        anm.enabled = false;
    }

	public void LoadPercent(WWW w){        
		if (!txt.gameObject.activeSelf) txt.gameObject.SetActive(true);
		www1 = w;
		load = true;
	}
}
