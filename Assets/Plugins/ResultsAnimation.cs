using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResultsAnimation : MonoBehaviour
{
    Animator BackGroundAnimations;
    public bool ischildSpeaking;
    public bool Desktop;
    
    // Start is called before the first frame update
    void Start()
    {
        BackGroundAnimations = GetComponent<Animator>();
        
    }

  public void PlayEnterBack()
    {
        
       StartCoroutine(PlayEnterBackR());
       
     
    }

    public void playExitBack()
    {
        if (Desktop == true)
        {
            BackGroundAnimations.Play("ResultsExitDesktop");
        }
        else
        {
            BackGroundAnimations.Play("ResultsExit");
        }
    }




    public IEnumerator PlayEnterBackR()
    {
       
            yield return new WaitForSeconds(0.1f);

        if (Desktop == true)
        {
            BackGroundAnimations.Play("ResultsEnterDesktop");

        }
        else
        {
            BackGroundAnimations.Play("ResultsEnter");
        }
            
            StartCoroutine(PlayExitBackR());

            if (ischildSpeaking == true)
            {

                StartCoroutine(PlayExitBackR());
                ischildSpeaking = false;

            }

        

       
    }



    public IEnumerator PlayExitBackR()
    {
        yield return new WaitForSeconds(4f);

        if (Desktop == true)
        {
            BackGroundAnimations.Play("ResultsExitDesktop");
        }
        else
        {
            BackGroundAnimations.Play("ResultsExit");
        }


    }

  


}
