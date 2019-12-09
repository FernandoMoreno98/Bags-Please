using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;

public class Settings : MonoBehaviour
{
    public AudioMixer audiomixermusic;
    public AudioMixer audiomixereffects;

    public void ReturnMainMenu()
    {
        FindObjectOfType<AudioManager>().Play("BotonSalida");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }

    public void SetMusicVolume(float volume)
    {
        audiomixermusic.SetFloat("MusicVolume", volume);
        //FindObjectOfType<AudioManager>().setVolumeMusic(volume);
    }

    public void SetEffectsVolume(float volume)
    {
        audiomixereffects.SetFloat("EffectsVolume", volume);
        //FindObjectOfType<AudioManager>().setVolumeEffects(volume);
    }
}
