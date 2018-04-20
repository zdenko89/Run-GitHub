using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class settingsMenu : MonoBehaviour {

    public AudioMixer AudioMixer;

   

    public void SetVolume(float volume)
    {
        Debug.Log(volume);
       AudioMixer.SetFloat("volume", volume);
    }

    void Update()
    {
    }

}
