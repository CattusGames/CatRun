using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Purchasing;

public class PurchaseSourse : MonoBehaviour
{
    private int _coin;
    public void OnPurchaseComplete(Product product)
    {
        if (product.definition.id == "coin_1000")
        {
            _coin = 1000;
            PlayerPrefs.SetInt("Coin",PlayerPrefs.GetInt("Coin")+_coin);
        }
        else if (product.definition.id == "noAds")
        {
            PlayerPrefs.SetInt("ad",1);
        }
    }

}
