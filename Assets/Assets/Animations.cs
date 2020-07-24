using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CharismaSdk.Example
{
    public class Animations : MonoBehaviour
    {

        private int AnimationClipNum = 0;
        private Animator Animationcotroller;


        private Charisma _charisma;


        // Start is called before the first frame update
        void Start()
        {
            Animationcotroller = GetComponent<Animator>();
            AnimationClipNum = 1;
            PlayAnimation();
        }

        // Update is called once per frame
        void Update()
        {

        }

        void PlayAnimation()
        {
            Animationcotroller.SetInteger("Animation", AnimationClipNum);
        }
    }

}