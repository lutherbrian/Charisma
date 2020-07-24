using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClockScript : MonoBehaviour
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

    public void EnterClock()

    {

        animatorclock.Play("clockTexture");
    }

    public void ExitClock()

    {

        animatorclock.Play("clockTextureR");
    }
}
