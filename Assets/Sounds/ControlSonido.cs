using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class ControlSonido : MonoBehaviour
{
    [SerializeField] private AudioMixer audioMixer;

    public void ControlMaestro(bool toggleMusica) 
    {
        if (toggleMusica == true) 
        {
            audioMixer.SetFloat("MasterVolume", Mathf.Log10(1) * 20);
        }
        else 
        {
            audioMixer.SetFloat("MasterVolume", Mathf.Log10(0.0001f) * 20);
        }
    }

    public void ControlSFX(float sliderSFX) 
    {
        audioMixer.SetFloat("SFXVolume", Mathf.Log10(sliderSFX) * 20);
    }

    public void ControlBGM(float sliderBGM)
    {
        audioMixer.SetFloat("BGMVolume", Mathf.Log10(sliderBGM) * 20);
    }
}
