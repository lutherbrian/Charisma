using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class RestartScript : MonoBehaviour
{
    Button RestartBut;
    // Start is called before the first frame update
    void Start()
    {
        
      
    }

    // Update is called once per frame
    void Update()
    {

    }



    public void RestartWindows()
    {

        SceneManager.LoadScene("Windows");
    }

    public void RestartAR()
    {

        SceneManager.LoadScene("HelloAR");
    }
}
