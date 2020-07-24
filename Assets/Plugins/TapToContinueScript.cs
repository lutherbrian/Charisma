using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TapToContinueScript : MonoBehaviour
{
    Animator UIAnimation;
    
    // Start is called before the first frame update
    void Start()
    {
        UIAnimation = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayEnterAnimation()
    {
        StartCoroutine(PlayEnterAnimationR());
    }


    public  IEnumerator PlayEnterAnimationR()
    {
        yield return new WaitForSeconds(5.0f);
        UIAnimation.Play("TapEnter2");
        
      
    }

    public void PlayExitAnimation()
    {

        UIAnimation.Play("TapExit");
    }
}
