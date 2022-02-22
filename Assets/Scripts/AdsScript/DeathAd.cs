using GoogleMobileAds.Api;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class DeathAd : MonoBehaviour
{
    private RewardedInterstitialAd rewardedInterstitialAd;
    string adUnitId = "ca-app-pub-1586520169160767/6371255088";
    int _loadtry = 0;
    [SerializeField] private AudioManager _audioManager;
    [SerializeField] private GameManager _gameManager;

    private void adLoadCallback(RewardedInterstitialAd ad, AdFailedToLoadEventArgs args)
    {
        if (args == null)
        {
            _loadtry = 0;
            _audioManager.StopBackgroundMusic();
            Debug.Log("RewardedInterstitialAd Loaded");
            rewardedInterstitialAd = ad;

            rewardedInterstitialAd.OnAdFailedToPresentFullScreenContent += HandleAdFailedToPresent;
            rewardedInterstitialAd.OnAdDidPresentFullScreenContent += HandleAdDidPresent;
            rewardedInterstitialAd.OnAdDidDismissFullScreenContent += HandleAdDidDismiss;
            rewardedInterstitialAd.OnPaidEvent += HandlePaidEvent;
        }
    }
    public void ShowRewardedInterstitialAd()
    {
        if (PlayerPrefs.GetInt("ad") == 1)
        {
            _audioManager.PlayBackgroundMusic();
            _gameManager._deathAd = true;
           
        }
        else
        {
            // Create an empty ad request.
            AdRequest request = new AdRequest.Builder().Build();
            // Load the rewarded ad with the request.
            RewardedInterstitialAd.LoadAd(adUnitId, request, adLoadCallback);

            if (rewardedInterstitialAd != null)
            {

                rewardedInterstitialAd.Show(userEarnedRewardCallback);
            }
        }

    }

    private void userEarnedRewardCallback(Reward reward)
    {
        _audioManager.PlayBackgroundMusic();
        _gameManager._deathAd = true;
        rewardedInterstitialAd.OnAdFailedToPresentFullScreenContent -= HandleAdFailedToPresent;
        rewardedInterstitialAd.OnAdDidPresentFullScreenContent -= HandleAdDidPresent;
        rewardedInterstitialAd.OnAdDidDismissFullScreenContent -= HandleAdDidDismiss;
        rewardedInterstitialAd.OnPaidEvent -= HandlePaidEvent;
    }
    private void HandleAdFailedToPresent(object sender, AdErrorEventArgs args)
    {
        _loadtry += 1;
        if (_loadtry <= 3)
        {
            AdRequest request = new AdRequest.Builder().Build();
            RewardedInterstitialAd.LoadAd(adUnitId, request, adLoadCallback);
        }
        else
        {
            rewardedInterstitialAd.Destroy();
        }
    }

    private void HandleAdDidPresent(object sender, EventArgs args)
    {
        MonoBehaviour.print("Rewarded interstitial ad has presented.");
    }

    private void HandleAdDidDismiss(object sender, EventArgs args)
    {
        MonoBehaviour.print("Rewarded interstitial ad has dismissed presentation.");
    }

    private void HandlePaidEvent(object sender, AdValueEventArgs args)
    {
        MonoBehaviour.print(
            "Rewarded interstitial ad has received a paid event.");
    }
}
