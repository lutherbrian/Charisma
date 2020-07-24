using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class VideoAudio : MonoBehaviour
{
    VideoPlayer show;

    public float volume = 0.5f;
    
    // Start is called before the first frame update
    void Start()
    {
        show = this.GetComponent<VideoPlayer>();
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

   public void volumeup()
    {
        show.SetDirectAudioVolume (0, volume);
    }

    public void volumeDown()
    {
        show.SetDirectAudioVolume(0, 0.05f);
    }

    public void increaseVolume()

    {
        volume = (volume += 0.5f);

        show.SetDirectAudioVolume(0, volume);
    }

}
