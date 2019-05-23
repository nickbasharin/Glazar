using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;


public class BallToCamera : MonoBehaviour
{

    public float speed;
    public float timer;
    public Animator anim;
    public GameObject soccerPanel;
    public float timeForPanel;
    public float timeForVideo;

    public AudioSource goal;
    public AudioSource udar;
    public GameObject canvMain;
    public GameObject videoGO;

    bool fly;
    float dist;
    Vector3 startPos;
    GameObject cam;
    Sprite loadSprite;
    // Use this for initialization
    void Start()
    {
        startPos = transform.position;
        cam = GameObject.FindGameObjectWithTag("MainCamera");
        StartCoroutine(DownLoadImage());

    }

    // Update is called once per frame
    void Update()
    {
        if (fly)
        {
            transform.LookAt(cam.transform);
            transform.position += transform.forward * speed*Time.deltaTime;
            dist = Vector3.Distance(cam.transform.position, transform.position);
            if (dist < 0.7f)
            {
                SoccerPanel();
            }
        }
    }

    public void GoBall()
    {
        anim.SetBool("goFoot", true);
        StartCoroutine(WaitToFoot());
    }

    IEnumerator WaitToFoot()
    {
        float timeT = 0;
        while (timeT < timer)
        {
            timeT += Time.deltaTime;
            yield return null;
            if (timeT < timer - 0.2f) udar.Play();
        }
        anim.SetBool("goFoot", false);
        BeatTheBall();
    }

    void BeatTheBall()
    {
        fly = true;
    
    }

    void SoccerPanel()
    {
        goal.Play();
        soccerPanel.SetActive(true);
        canvMain.SetActive(false);
        videoGO.SetActive(true);

        transform.position = startPos;
        fly = false;
        StartCoroutine(OffPanelInTime());
    }

    IEnumerator OffPanelInTime()
    {
        yield return new WaitForSeconds(timeForPanel);
        videoGO.GetComponent<VideoPlayer>().Play();
        soccerPanel.SetActive(false);
        yield return new WaitForSeconds(timeForVideo);
        videoGO.SetActive(false);
        canvMain.SetActive(true);

    }


    IEnumerator DownLoadImage()
    {
        RawImage rawI = soccerPanel.GetComponent<RawImage>();
        WWW www = new WWW("http://glazar.pa.infobox.ru/ar/GlazAR/images/soccer.jpg");
        while (!www.isDone)
        {
            yield return null;
        }
        if (string.IsNullOrEmpty(www.error))
        {

            rawI.texture = www.texture;
        }
    }
}