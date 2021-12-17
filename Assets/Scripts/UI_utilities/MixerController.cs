using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class MixerController : MonoBehaviour
{
    [SerializeField] private AudioMixer mixer;
    [SerializeField] float startVal;
    private void Start()
    {
        mixer.SetFloat("MusicVol", Mathf.Log10(startVal) * 20);
    }
    public void SetMusicVolume(float sliderVal)
    {
        mixer.SetFloat("MusicVol", Mathf.Log10(sliderVal) * 20);
    }

    public void SetSFXVolume(float sliderVal)
    {
        mixer.SetFloat("SFXVol", Mathf.Log10(sliderVal) * 20);
    }
}
