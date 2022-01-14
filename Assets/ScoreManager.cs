using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    private int _score;
    [SerializeField]public Text _scoreText, _highScoreText, _coinText;

    private Animation _scoreTextAnim,_highScoreAnim, _coinTextAnim;

    [HideInInspector]
    public int score = 0;

    void Start()
    {
        _scoreTextAnim = _scoreText.gameObject.GetComponent<Animation>();     //Initializes socreTextAnim
        _coinTextAnim = _coinText.gameObject.GetComponent<Animation>();     //Initializes CoinTextAnim
        _coinText.text = PlayerPrefs.GetInt("Coin", 0).ToString();     //Writes out the number of Coins to the screen

        _highScoreText.text = "HS: " + PlayerPrefs.GetInt("Score").ToString();

        _scoreText.text = _score.ToString();
        var recentScore = (int)gameObject.transform.position.y;
        if (_score < recentScore)
        {
            _score = recentScore;
        }
    }

    public void IncrementScore()
    {
        if (FindObjectOfType<GameManager>()._gameIsOver == false)       //If the game is not over

        _scoreText.text = (++score).ToString();      //Increments the 'scoretext' text as well as the score variable's value and writes it out to the screen
        _scoreTextAnim.Play();       //Plays scoreTextAnim
        FindObjectOfType<AudioManager>().ScoreSound();      //Plays scoreSound
    }

    public void IncrementCoin()
    {
        if (FindObjectOfType<GameManager>()._gameIsOver == false)       //If the game is not over
        {
            PlayerPrefs.SetInt("Coin", PlayerPrefs.GetInt("Coin", 0) + 1);        //Increases the number of Coins
            _coinText.text = PlayerPrefs.GetInt("Coin", 0).ToString();     //Writes out the number of Coins to the screen
            _coinTextAnim.Play();       //Plays CoinTextAnim
            FindObjectOfType<AudioManager>().CoinSound();      //Plays CoinSound
        }
    }

    public void DecrementCoin(int decreaseValue)
    {
        PlayerPrefs.SetInt("Coin", PlayerPrefs.GetInt("Coin", 0) - decreaseValue);        //Decreases the number of Coins by decreaseValue
        _coinText.text = PlayerPrefs.GetInt("Coin", 0).ToString();     //Writes out the number of Coins to the screen
    }
}
