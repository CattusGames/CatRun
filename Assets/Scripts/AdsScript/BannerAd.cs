using GoogleMobileAds.Api;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BannerAd : MonoBehaviour
{

    private BannerView _bannerAd;
    private string _appid = "ca-app-pub-1586520169160767/9600053655";
    // Start is called before the first frame update
    void Awake()
    {
        if (PlayerPrefs.GetInt("ad")==1)
        {
            gameObject.SetActive(false);
        }
        else
        {
            MobileAds.Initialize(initStatus => { });
            RequestBanner();
        }

    }

    private void RequestBanner()
    {
        _bannerAd = new BannerView(_appid, AdSize.Banner, AdPosition.Bottom);
        AdRequest request = new AdRequest.Builder().Build();
        _bannerAd.LoadAd(request);
    }
}
