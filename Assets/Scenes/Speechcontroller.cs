using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using IBM.Watsson.Examples;
using GoogleARCore.Examples.HelloAR;
using UnityEngine.Android;
using UnityEngine.Video;



public class Speechcontroller : MonoBehaviour
{
    public Button SButton;
    public ExampleStreaming watson;
    
    private bool Recording = false;
    public GameObject arCoreSessionPrefab;
    public GameObject ARController;
    private Camera FirstCam;
    HelloARController ARControllerscript;
    
    


    private GameObject newArCoreSessionPrefab;
    private GoogleARCore.ARCoreSession arcoreSession;
    // Start is called before the first frame update
    void Start()
    {
        Recording = false;
        ARControllerscript = ARController.GetComponent<HelloARController>();
        Permission.RequestUserPermission(Permission.Microphone);

    }

    // Update is called once per frame
    void Update()
    {
        watson.Active = Recording;
    }


    public void FingerDown()

    {
        if (SButton.IsInteractable() == true)
        {
            SButton.GetComponent<Image>().color = Color.red;
            Recording = true;

            watson.Active = true;
        }
    }

    public void FingerUp()

    {
        
            SButton.GetComponent<Image>().color = Color.white;
            Recording = false;

            watson.Active = false;
        


    }



   

    public void StartAR()
    {


        newArCoreSessionPrefab = Instantiate(arCoreSessionPrefab, Vector3.zero, Quaternion.identity);
        arcoreSession = newArCoreSessionPrefab.GetComponent<GoogleARCore.ARCoreSession>();
        arcoreSession.enabled = true;
        ARControllerscript = ARController.GetComponent<HelloARController>();
        FirstCam = newArCoreSessionPrefab.GetComponentInChildren<Camera>();
        ARControllerscript.FirstPersonCamera = FirstCam;

        if (FirstCam == null)
        {

           
        }
       
    
    }

    public void QuitApp()
    {

        Application.Quit();
    }
}
