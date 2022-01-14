using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioSource _backgroundMusic, _coinSound, _scoreSound, _deathSound, _buttonClickSound, _skinSwitchSound, _notEnoughTokenSound;

    [HideInInspector]
    public bool soundIsOn = true;       //GameManager script might modify this value

    //Functions are called by other scripts when it is necessary

    public void StopBackgroundMusic()
    {
        _backgroundMusic.Stop();
    }

    public void PlayBackgroundMusic()
    {
        if (soundIsOn)
            _backgroundMusic.Play();
    }

    public void CoinSound()
    {
        if (soundIsOn)
            _coinSound.Play();
    }

    public void ScoreSound()
    {
        if (soundIsOn)
            _scoreSound.Play();
    }

    public void DeathSound()
    {
        if (soundIsOn)
            _deathSound.Play();
    }

    public void ButtonClickSound()
    {
        if (soundIsOn)
            _buttonClickSound.Play();
    }

    public void NotEnoughTokenSound()
    {
        if (soundIsOn)
            _notEnoughTokenSound.Play();
    }

    public void SkinSwitchSound()
    {
        if (soundIsOn)
            _skinSwitchSound.Play();
    }
}
