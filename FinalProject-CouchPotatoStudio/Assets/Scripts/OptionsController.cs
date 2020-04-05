using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OptionsController : MonoBehaviour
{
    public Slider musicVolume, soundVolume;
    public Toggle invertY, invertX, easyDash;
    public UnityEngine.Audio.AudioMixer mixer;
    public AudioSource testSound;
    private float testCooldown = 0f;
    // Start is called before the first frame update
    void Start()
    {
        soundVolume.value = PlayerPrefs.GetFloat("SoundVolume", 1);
        mixer.SetFloat("SoundVolume", Mathf.Log10(soundVolume.value) * 20);
        musicVolume.value = PlayerPrefs.GetFloat("SoundVolume", 1);
        mixer.SetFloat("SoundVolume", Mathf.Log10(musicVolume.value) * 20);
        invertY.isOn = (PlayerPrefs.GetInt("InvertedY", -1) == -1? true : false);
        invertX.isOn = (PlayerPrefs.GetInt("InvertedX", 1) == 1 ? true : false);
        easyDash.isOn = (PlayerPrefs.GetInt("EasyDash", 0) == 1 ? true : false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateVolume(bool sound)
    {
        if (sound)
        {
            mixer.SetFloat("SoundVolume", Mathf.Log10(soundVolume.value) * 20);
            if (soundVolume.value == 0)
            {
                mixer.SetFloat("SoundVolume", -80);
            }
            if (testCooldown < Time.unscaledTime)
            {
                testCooldown = Time.unscaledTime + testSound.clip.length;
                testSound.Play();
            }
            PlayerPrefs.SetFloat("SoundVolume", soundVolume.value);
        }
        else
        {
            mixer.SetFloat("MusicVolume", Mathf.Log10(musicVolume.value) * 20);
            if (musicVolume.value == 0)
            {
                mixer.SetFloat("MusicVolume", -80);
            }
            PlayerPrefs.SetFloat("MusicVolume", musicVolume.value);
        }
    }

    public void UpdateInvert(bool x)
    {
        if (x)
        {
            if (invertX.isOn)
            {
                PlayerPrefs.SetInt("InvertedX", 1);
            }
            else
            {
                PlayerPrefs.SetInt("InvertedX", -1);
            }
        }
        else
        {
            if (invertY.isOn)
            {
                PlayerPrefs.SetInt("InvertedY", -1);
            }
            else
            {
                PlayerPrefs.SetInt("InvertedY", 1);
            }
        }
    }

    public void UpdateEasyDash()
    {
        if (easyDash.isOn)
        {
            PlayerPrefs.SetInt("EasyDash", 1);
        }
        else
        {
            PlayerPrefs.SetInt("EasyDash", 0);
        }
    }
}
