using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleMobileAds.Api;
using GoogleMobileAds;
using System;

public class CoinAd : MonoBehaviour
{
    private RewardedAd rewardedAd;
    string adUnitId = "ca-app-pub-3940256099942544/5224354917";
    int _loadtry = 0;
    [SerializeField] private AudioManager _audioManager;
    [SerializeField] private ScoreManager _scoreManager;
    private void Start()
    {
       
    }

    public void ShowRewardVideo()
    {
        LoadRewardVideo();
        if (rewardedAd.IsLoaded())
        {
            rewardedAd.Show();
        }
        else
        {

        }
    }

    public void LoadRewardVideo()
    {
        this.rewardedAd = new RewardedAd(adUnitId);
        _loadtry = 0;
        // Called when an ad request has successfully loaded.
        this.rewardedAd.OnAdLoaded += HandleRewardedAdLoaded;
        // Called when an ad request failed to load.
        this.rewardedAd.OnAdFailedToLoad += HandleRewardedAdFailedToLoad;
        // Called when an ad is shown.
        this.rewardedAd.OnAdOpening += HandleRewardedAdOpening;
        // Called when an ad request failed to show.
        this.rewardedAd.OnAdFailedToShow += HandleRewardedAdFailedToShow;
        // Called when the user should be rewarded for interacting with the ad.
        this.rewardedAd.OnUserEarnedReward += HandleUserEarnedReward;
        // Called when the ad is closed.
        this.rewardedAd.OnAdClosed += HandleRewardedAdClosed;

        

        // Create an empty ad request.
        AdRequest request = new AdRequest.Builder().Build();
        // Load the rewarded ad with the request.
        this.rewardedAd.LoadAd(request);

    }








    public void HandleRewardedAdLoaded(object sender, EventArgs args)
    {

        _loadtry = 0;
        _audioManager.StopBackgroundMusic();
        Debug.Log("HandleRewardedAdLoaded event received");
    }

    public void HandleRewardedAdFailedToLoad(object sender, AdFailedToLoadEventArgs args)
    {
        _loadtry += 1;
        if (_loadtry<=3)
        {
            LoadRewardVideo();
        }
        else
        {
            rewardedAd.Destroy();
        }
    }

    public void HandleRewardedAdOpening(object sender, EventArgs args)
    {
        Debug.Log("HandleRewardedAdOpening event received");
    }

    public void HandleRewardedAdFailedToShow(object sender, AdErrorEventArgs args)
    {
        rewardedAd.Destroy();
    }

    public void HandleRewardedAdClosed(object sender, EventArgs args)
    {
        _audioManager.PlayBackgroundMusic();
        Debug.Log("HandleRewardedAdClosed event received");
    }

    public void HandleUserEarnedReward(object sender, Reward args)
    {
        string type = args.Type;
        double amount = args.Amount;
        Debug.Log(
            "HandleRewardedAdRewarded event received for "
                        + amount.ToString() + " " + type);
        _scoreManager.IncrementX50Coin();
    }
}