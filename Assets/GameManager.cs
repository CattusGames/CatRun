using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject _main, _settings, _paused, _end, _coinBuy, _shop,_startGame, _muteImage;
    public TextMeshProUGUI  _highScoreText;
    [HideInInspector] public bool _gameIsOver = false;
    [HideInInspector] public bool _start = false;
    // Start is called before the first frame update
    void Start()
    {
        StartPanelActivation();
        AudioCheck();
    }

    // Update is called once per frame
    void Update()
    {


    }
    public void DisableAllPanel()
    {
        _main.SetActive(false);
        _settings.SetActive(false);
        _paused.SetActive(false);
        _end.SetActive(false);
        _coinBuy.SetActive(false);
        _shop.SetActive(false);
    }
    public void StartPanelActivation()
    {
        _main.SetActive(true);
        _settings.SetActive(false); 
        _paused.SetActive(false);
        _startGame.SetActive(false);
        _end.SetActive(false);
        _coinBuy.SetActive(false);
        _shop.SetActive(false);
    }

    public void EndPanelActivation()
    {
        _gameIsOver = true;
        HighScoreCheck();
        _main.SetActive(false);
        _settings.SetActive(false);
        _startGame.SetActive(false);
        _paused.SetActive(false);
        _end.SetActive(true);
        _coinBuy.SetActive(false);
        _shop.SetActive(false);
    }
    public void AdsPanelActivation()
    {

    }
    public void ShopPanelActivation()
    {
        _main.SetActive(false);
        _shop.SetActive(true);
        _settings.SetActive(false);
        _paused.SetActive(false);
        _end.SetActive(false);
        _coinBuy.SetActive(false);
    }
    public void SettingsPanelActivation()
    {

        _main.SetActive(false);
        _settings.SetActive(true);
        _paused.SetActive(false);
        _end.SetActive(false);
        _coinBuy.SetActive(false);
        _shop.SetActive(false);
    }
    public void PausedPanelActivation()
    {
        _start = false;
        _main.SetActive(false);
        _settings.SetActive(false);
        _paused.SetActive(true);
        _startGame.SetActive(false);
        _end.SetActive(false);
        _coinBuy.SetActive(false);
        _shop.SetActive(false);
    }
    public void StartGamePanelActivation()
    {
        _start = true;
        _main.SetActive(false);
        _settings.SetActive(false);
        _paused.SetActive(false);
        _startGame.SetActive(true);
        _end.SetActive(false);
        _coinBuy.SetActive(false);
        _shop.SetActive(false);
    }
    public void CoinBuyPanelActivation()
    {
        _main.SetActive(false);
        _settings.SetActive(false);
        _paused.SetActive(false);
        _end.SetActive(false);
        _coinBuy.SetActive(true);
        _shop.SetActive(false);
    }

        public void HighScoreCheck()
    {
        if (FindObjectOfType<ScoreManager>()._score > PlayerPrefs.GetInt("HighScore", 0))
        {
            PlayerPrefs.SetInt("HighScore", FindObjectOfType<ScoreManager>()._score);
        }
        _highScoreText.text = PlayerPrefs.GetInt("HighScore", 0).ToString();
    }

    public void AudioCheck()
    {
        if (PlayerPrefs.GetInt("Audio", 0) == 0)
        {
            _muteImage.SetActive(false);
            FindObjectOfType<AudioManager>().soundIsOn = true;
            FindObjectOfType<AudioManager>().PlayBackgroundMusic();
        }
        else
        {
            _muteImage.SetActive(true);
            FindObjectOfType<AudioManager>().soundIsOn = false;
            FindObjectOfType<AudioManager>().StopBackgroundMusic();
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
    public void AudioButton()
    {
        if (PlayerPrefs.GetInt("Audio", 0) == 0)
            PlayerPrefs.SetInt("Audio", 1);
        else
            PlayerPrefs.SetInt("Audio", 0);
        AudioCheck();
    }
}
