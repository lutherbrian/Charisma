using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClockHands : MonoBehaviour
{

    Animator animatorclock;
    // Start is called before the first frame update
    void Start()
    {
        animatorclock = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void StartHands()

    {

        animatorclock.SetBool("Start?", true);
       
    }
}
