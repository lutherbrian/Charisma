using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using CharismaSdk.Example;

public class CountDownTimer : MonoBehaviour
{
	int Countdown = 300;
	float CountUp = 0;

    public GameObject Charisma;
    SliderScript ScriptSlider;
    bool neverDone = true;
	
	Slider Slider;
    public GameObject ClockTexture;
	// Start is called before the first frame update
	void Start()
	{
		

		Slider = GetComponent<Slider>();
        
        
	}

	// Update is called once per frame
	void Update()
	{
        if (neverDone == true)
        {
            if (Slider.value == 1.0f)

            {
                neverDone = false;
                Charisma.GetComponent<ExampleScript>().ChangeScene();
                ScriptSlider.GetComponent<SliderScript>().ExitAnimation();
                ClockTexture.GetComponent<ClockScript>().ExitClock();
                

            }
        }
	}


	public void StartTimer()

	{
        Debug.Log("Clock");
        StartCoroutine(CountDownStart());
	}

	IEnumerator CountDownStart()

	{
		while (Countdown > 0)

		{
			

			yield return new WaitForSeconds(1f);

			Countdown--;
			CountUp += .0117f;
			Slider.value = CountUp;

		}
	}
}
