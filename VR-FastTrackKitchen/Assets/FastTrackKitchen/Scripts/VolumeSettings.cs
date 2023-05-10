using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class VolumeSettings : MonoBehaviour
{
    [SerializeField] private AudioMixer myMixer;
    [SerializeField] private Slider musicSlider;
    [SerializeField] private Toggle musicToggle;
    [SerializeField] private Slider SFXSlider;
    [SerializeField] private Toggle SFXToggle;

    private void Start()
    {
        if (PlayerPrefs.HasKey("musicVolume") || PlayerPrefs.HasKey("musicToggle") || PlayerPrefs.HasKey("SFXVolume") || PlayerPrefs.HasKey("SFXToggle"))
        {
            LoadVolume();
        }
        else
        {
            SetMusicVolume();
            SetSFXVolume();
        }
    }

    public void SetMusicVolume()
    {
        float volume = musicSlider.value;
        PlayerPrefs.SetFloat("musicVolume", volume);
        if (musicToggle.isOn) // if the musicToggle is on
        {
            musicSlider.value = 1.0f;
            musicSlider.interactable = false;
            volume = Mathf.Log10(0.0001f) * 20; // set the music volume to 0.0001f
            PlayerPrefs.SetInt("musicToggle", 1); // save the state of the musicToggle to 1 (on)
        }
        else // if the musicToggle is off
        {
            musicSlider.interactable = true;
            volume = Mathf.Log10(volume) * 20; // set the music volume to the selected volume
            PlayerPrefs.SetInt("musicToggle", 0); // save the state of the musicToggle to 0 (off)
        }
        myMixer.SetFloat("music", volume);
    }

    public void SetSFXVolume()
    {
        float volume = SFXSlider.value;
        PlayerPrefs.SetFloat("SFXVolume", volume);

        if (SFXToggle.isOn) // if the musicToggle is on
        {
            SFXSlider.value = 1.0f;
            SFXSlider.interactable = false;
            volume = Mathf.Log10(0.0001f) * 20; // set the music volume to 0.0001f
            PlayerPrefs.SetInt("SFXToggle", 1); // save the state of the musicToggle to 1 (on)
        }
        else // if the musicToggle is off
        {
            SFXSlider.interactable = true;
            volume = Mathf.Log10(volume) * 20; // set the music volume to the selected volume
            PlayerPrefs.SetInt("SFXToggle", 0); // save the state of the musicToggle to 0 (off)
        }

        myMixer.SetFloat("SFX", volume);
    }

    private void LoadVolume()
    {
        musicSlider.value = PlayerPrefs.GetFloat("musicVolume");
        SFXSlider.value = PlayerPrefs.GetFloat("SFXVolume");

        // Load the state of the musicToggle from PlayerPrefs
        if (PlayerPrefs.GetInt("musicToggle") != 0)
            musicToggle.isOn = true;
        else
            musicToggle.isOn = false;

        // Load the state of the SFXToggle from PlayerPrefs
        if (PlayerPrefs.GetInt("SFXToggle") != 0)
            SFXToggle.isOn = true;
        else
            SFXToggle.isOn = false;
        SetMusicVolume();
        SetSFXVolume();
    }
}