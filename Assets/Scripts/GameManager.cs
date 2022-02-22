using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject _player, _main, _settings, _paused, _end,_preEnd, _coinBuy, _shop,_skins,_themes,_startGame, _audioMuteImage,_musicMuteImage,_vibrationMuteImage;
    public GameObject _endButton;
    private AudioManager _audioManager;
    [SerializeField] private Button _skinButton;
    [SerializeField] private Button _themesButton;
    [SerializeField] private SkinChanger _skinChanger;
    private ScoreManager _scoreManager;
    private PlayerController _playerController;
    public TextMeshProUGUI  _highScoreText;
    [HideInInspector] public bool _gameIsOver = false;
    [HideInInspector] public bool _start = false;
    [HideInInspector] public bool _deathAd = false;
    private bool _pause;

    // Start is called before the first frame update
    void Start()
    {
        
        _playerController = _player.GetComponent<PlayerController>();
        _audioManager = _player.GetComponent<AudioManager>();
        _scoreManager = _player.GetComponent<ScoreManager>();
        StartPanelActivation();
        AudioCheck();
        MusicCheck();
        VibrationCheck();
    }
    public void DisableAllPanel()
    {
        _playerController._touchActive = false;
        _main.SetActive(false);
        _settings.SetActive(false);
        _paused.SetActive(false);
        _end.SetActive(false);
        _coinBuy.SetActive(false);
        _shop.SetActive(false);
        _preEnd.SetActive(false);
    }
    public void StartPanelActivation()
    {
        _pause = false;
        _playerController._touchActive = true;
        _main.SetActive(true);
        _settings.SetActive(false); 
        _paused.SetActive(false);
        _startGame.SetActive(false);
        _end.SetActive(false);
        _coinBuy.SetActive(false);
        _shop.SetActive(false);
        _preEnd.SetActive(false);
    }

    public void EndPanelActivation()
    {
        _playerController._touchActive = false;
        _gameIsOver = true;
        HighScoreCheck();
        _main.SetActive(false);
        _settings.SetActive(false);
        _startGame.SetActive(false);
        _paused.SetActive(false);
        _end.SetActive(true);
        _coinBuy.SetActive(false);
        _shop.SetActive(false);
        _preEnd.SetActive(false);
    }
    public void PreEndPanelActivation()
    {
        _playerController._touchActive = false;
        _main.SetActive(false);
        _settings.SetActive(false);
        _startGame.SetActive(false);
        _paused.SetActive(false);
        _end.SetActive(false);
        _coinBuy.SetActive(false);
        _shop.SetActive(false);
        _preEnd.SetActive(true);

    }
    public void ThemePanelActivation()
    {
        
        _skins.SetActive(false);
        _themes.SetActive(true);
    }
    public void SkinPanelActivation()
    {
        _themes.SetActive(false);
        _skins.SetActive(true);
    }
    public void ShopPanelActivation()
    {
        if (_pause == true)
        {
            _themesButton.interactable = false;
        }
        else
        {
            _themesButton.interactable = true;
        }
        _playerController._touchActive = false;
        _main.SetActive(false);
        _shop.SetActive(true);
        _settings.SetActive(false);
        _paused.SetActive(false);
        _end.SetActive(false);
        _coinBuy.SetActive(false);
    }
    public void SettingsPanelActivation()
    {
        _playerController._touchActive = false;
        _main.SetActive(false);
        _settings.SetActive(true);
        _paused.SetActive(false);
        _end.SetActive(false);
        _coinBuy.SetActive(false);
        _shop.SetActive(false);
    }
    public void PausedPanelActivation()
    {
        _playerController._touchActive = false;
        _start = false;
        _pause = true;
        _main.SetActive(false);
        _settings.SetActive(false);
        _paused.SetActive(true);
        _startGame.SetActive(false);
        _end.SetActive(false);
        _coinBuy.SetActive(false);
        _shop.SetActive(false);
        _preEnd.SetActive(false);
    }
    public void StartGamePanelActivation()
    {
        _playerController._touchActive = true;
        _start = true;
        _main.SetActive(false);
        _settings.SetActive(false);
        _paused.SetActive(false);
        _startGame.SetActive(true);
        _end.SetActive(false);
        _coinBuy.SetActive(false);
        _shop.SetActive(false);
        _preEnd.SetActive(false);
    }
    public void CoinBuyPanelActivation()
    {
        _playerController._touchActive = false;
        _main.SetActive(false);
        _settings.SetActive(false);
        _paused.SetActive(false);
        _end.SetActive(false);
        _coinBuy.SetActive(true);
        _shop.SetActive(false);
    }

        public void HighScoreCheck()
    {
        if (_scoreManager._score > PlayerPrefs.GetInt("HighScore", 0))
        {
            PlayerPrefs.SetInt("HighScore", _scoreManager._score);
        }
        _highScoreText.text = PlayerPrefs.GetInt("HighScore", 0).ToString();
    }

    public void AudioCheck()
    {
        if (PlayerPrefs.GetInt("Audio", 0) == 0)
        {
            _audioMuteImage.SetActive(false);
            _audioManager._soundIsOn = true;
        }
        else
        {
            _audioMuteImage.SetActive(true);
            _audioManager._soundIsOn = false;
        }
    }
    public void MusicCheck()
    {
        if (PlayerPrefs.GetInt("Music", 0) == 0)
        {
            _musicMuteImage.SetActive(false);
            _audioManager._musicIsOn = true;
            _audioManager.PlayBackgroundMusic();
        }
        else
        {
            _musicMuteImage.SetActive(true);
            _audioManager._musicIsOn = false;
            _audioManager.StopBackgroundMusic();
        }
    }
    public void VibrationCheck()
    {
        if (PlayerPrefs.GetInt("Vibration", 0) == 0)
        {
            _vibrationMuteImage.SetActive(false);
            _audioManager._vibrationIsOn = true;
        }
        else
        {
            _vibrationMuteImage.SetActive(true);
            _audioManager._vibrationIsOn = false;
        }
    }
    public void ExitCoinBuyPanel()
    {
        if (_gameIsOver == false)
        {
            StartPanelActivation();
        }
        else
        {
            EndPanelActivation();
        }
    }
    public void RestartButton()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void BackSettingsButton()
    {
        if (_start==true)
        {
            _settings.SetActive(false);
            _end.SetActive(true);
        }
        else
        {
            StartPanelActivation();
        }
    }
    public void BackShopButton()
    {
        if (_pause == true)
        {
            StartGamePanelActivation();
            _skinChanger.SetChoosenSkin();
        }
        else
        {
            _skinChanger.SetChoosenSkin();
            StartPanelActivation();
        }
    }
    public void AudioButton()
    {
        if (PlayerPrefs.GetInt("Audio", 0) == 0)
            PlayerPrefs.SetInt("Audio", 1);
        else
            PlayerPrefs.SetInt("Audio", 0);
        AudioCheck();
    }
    public void MucicButton()
    {
        if (PlayerPrefs.GetInt("Music", 0) == 0)
            PlayerPrefs.SetInt("Music", 1);
        else
            PlayerPrefs.SetInt("Music", 0);
        MusicCheck();
    }
    public void VibrationButton()
    {
        if (PlayerPrefs.GetInt("Vibration", 0) == 0)
            PlayerPrefs.SetInt("Vibration", 1);
        else
            PlayerPrefs.SetInt("Vibration", 0);
        VibrationCheck();
    }
}
