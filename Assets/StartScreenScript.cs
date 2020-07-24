using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartScreenScript : MonoBehaviour
{
    private Canvas StartCanvas;
    // Start is called before the first frame update
    void Start()
    {
        StartCanvas = this.GetComponent<Canvas>();

    }

    // Update is called once per frame
    void Update()
    {
        
    }

  void StartApplication()
    {
        StartCanvas.enabled = false;
    }
}
