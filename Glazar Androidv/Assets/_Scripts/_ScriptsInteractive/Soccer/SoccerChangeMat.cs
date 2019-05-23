using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoccerChangeMat : MonoBehaviour
{

    public Material tshort;
    public Texture tshortT;
    public Texture tshortT2;
    public Material shorti;
    public Texture shortiT;
    public Texture shortiT2;
    public Material face;
    public Texture faceT;
    public Texture faceT2;
    public Material arms;
    public Texture armsT;
    public Texture armsT2;


    bool s;
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SetMat1()
    {
        if (!s)
        {
            tshort.SetTexture("_MainTex", tshortT);
            tshort.SetTexture("_DetailAlbedoMap", tshortT);
            shorti.SetTexture("_MainTex", shortiT);
            shorti.SetTexture("_DetailAlbedoMap", shortiT);
            face.SetTexture("_MainTex", faceT);
          //  face.SetTexture("_DetailAlbedoMap", faceT);
            arms.SetTexture("_MainTex", armsT);
         //   arms.SetTexture("_DetailAlbedoMap", armsT);

            s = true;
        }
        else {
            tshort.SetTexture("_MainTex", tshortT2);
            tshort.SetTexture("_DetailAlbedoMap", tshortT2);
            shorti.SetTexture("_MainTex", shortiT2);
            shorti.SetTexture("_DetailAlbedoMap", shortiT2);
            face.SetTexture("_MainTex", faceT2);
         //   face.SetTexture("_DetailAlbedoMap", faceT2);
            arms.SetTexture("_MainTex", armsT2);
        //    arms.SetTexture("_DetailAlbedoMap", armsT2);
            s = false;

        }
    }
}
