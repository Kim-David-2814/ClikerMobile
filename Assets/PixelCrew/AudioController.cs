using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioController : MonoBehaviour
{
    public AudioSource _musicSFX;
    public AudioSource _soundSFX;

    public Sprite _musicOff;
    public Sprite _musicOn;
    public Sprite _soundOff;
    public Sprite _soundOn;

    public Image _musicButton;
    public Image _soundButton;

    private void Awake()
    {
        if (PlayerPrefs.HasKey("MusicVolume"))
        {
            _musicSFX.volume = PlayerPrefs.GetFloat("MusicVolume");
        }

        if (PlayerPrefs.HasKey("SoundVolume"))
        {
            _soundSFX.volume = PlayerPrefs.GetFloat("SoundVolume");
        }
    }

    private void Start()
    {
        if (_musicSFX.volume >= 0.1f)
        {
            _musicButton.GetComponent<Image>().sprite = _musicOn;
        }
        else
        {
            _musicButton.GetComponent<Image>().sprite = _musicOff;
        }

        if (_soundSFX.volume >= 0.1f)
        {
            _soundButton.GetComponent<Image>().sprite = _soundOn;
        }
        else
        {
            _soundButton.GetComponent <Image>().sprite = _soundOff;
        }
    }

    public void OnClickMusicButton()
    {
        if (_musicSFX.volume >= 0.1f)
        {
            _musicSFX.volume = 0;
            _musicButton.GetComponent<Image>().sprite = _musicOff;
        }
        else
        {
            _musicSFX.volume = 0.5f;
            _musicButton.GetComponent <Image>().sprite = _musicOn;
        }

        PlayerPrefs.SetFloat("MusicVolume", _musicSFX.volume);
    }

    public void OnClickSoundButton()
    {
        if (_soundSFX.volume >= 0.1f)
        {
            _soundSFX.volume = 0;
            _soundButton.GetComponent<Image>().sprite = _soundOff;
        }
        else
        {
            _soundSFX.volume = 0.5f;
            _soundButton.GetComponent<Image>().sprite = _soundOn;
        }

        PlayerPrefs.SetFloat("SoundVolume", _soundSFX.volume);
    }
}
