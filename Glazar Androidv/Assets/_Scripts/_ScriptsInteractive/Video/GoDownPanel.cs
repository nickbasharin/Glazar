using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoDownPanel : MonoBehaviour {

    float move, startPos;
    bool godown;
    public bool startCor;
    public float speed;
    public float timeToButtons = 2.5f;
    float stopMoveCordDown, stopMoveCordUp;
    // Use this for initialization
    void Start()
    {
        move = 0f;
        startPos = transform.localPosition.y;
        //  godown = true;
        stopMoveCordUp = 0;
        stopMoveCordDown = -48;
    }

    void Update()
    {
        if (!startCor) { StartCoroutine(StartDown()); startCor = true; }

        if (godown)
        {
            move = move + speed * Time.deltaTime;
            gameObject.transform.localPosition = new Vector3(transform.localPosition.x, startPos - move, transform.localPosition.z);
            if (transform.localPosition.y < stopMoveCordDown) godown = false;
            if ((transform.localPosition.y > stopMoveCordUp)&&(speed<0)) godown = false;

        }
    }


    public void GoInfoPanel() {
        godown = true;
        stopMoveCordDown = -77;
        if (speed > 0)
        {
            speed = -100;
        }
        else speed = 100;
    }

    IEnumerator StartDown()
    {
        yield return new WaitForSeconds(timeToButtons);
        godown = true;
    }
}
