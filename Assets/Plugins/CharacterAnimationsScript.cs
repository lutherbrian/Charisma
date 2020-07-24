using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class CharacterAnimationsScript : MonoBehaviour
{

    Animator animator;

    public int animatorint;

    public GameObject TV;
    public GameObject TVLight;
    public GameObject Slider;
    public GameObject Clock;
    public GameObject ClockTexture;
    AudioSource cry;

    // Start is called before the first frame update
    void Start()
    {
        cry = GetComponent<AudioSource>();
        animator = GetComponent<Animator>();
        TV = GameObject.FindGameObjectWithTag("TV");
        TVLight = GameObject.FindGameObjectWithTag("TVLight");
        Slider = GameObject.FindGameObjectWithTag("Slider");
        Clock = GameObject.FindGameObjectWithTag("Clock");
        ClockTexture = GameObject.FindGameObjectWithTag("ClockTexture");
    }

    // Update is called once per frame
    void Update()
    {
        
        animator.SetInteger("CharacterInt", animatorint);

    }

    public void UpdateAnimation(int Updateint)

    {
        

    }

    public void SwitchToIdol()

    {

        animatorint = 2;
    }

    public void StartShow()
    {

        
        

        TVLight.GetComponent<Light>().intensity = 1.0f;
        Slider.GetComponent<SliderScript>().EnterAnimation();
        Slider.GetComponent<CountDownTimer>().StartTimer();
        ClockTexture.GetComponent<ClockScript>().EnterClock();
        Clock.GetComponent<ClockHands>().StartHands();
        TV.GetComponent<VideoPlayer>().Play();

    }

    public void Volumeup()
    {

        TV.GetComponent<VideoAudio>().increaseVolume();
        
    }
    public void PlayCrySound()
    {

        cry.Play();
    }
}
