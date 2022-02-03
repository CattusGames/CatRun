using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class SkinChanger : MonoBehaviour
{
    public Skin[] info;
    private bool[] StockCheck;

    public Button buyBttn;
    public TextMeshProUGUI priceText;
    public TextMeshProUGUI coinsText;
    private SkinnedMeshRenderer _meshRenderer;
    public int index;

    public int coins;

    private void Awake()
    {
        _meshRenderer = gameObject.GetComponent<SkinnedMeshRenderer>();
        coins = PlayerPrefs.GetInt("Coin");
        index = PlayerPrefs.GetInt("chosenSkin");
        coinsText.text = coins.ToString();

        StockCheck = new bool[53];
        if (PlayerPrefs.HasKey("StockArray"))
            StockCheck = PlayerPrefsX.GetBoolArray("StockArray");

        else
            StockCheck[0] = true;

        info[index].isChosen = true;

        for (int i = 0; i < info.Length; i++)
        {
            info[i].inStock = StockCheck[i];
            if (i == index)
                _meshRenderer.material = info[i].material;
        }

        priceText.text = "CHOSEN";
        buyBttn.interactable = false;
    }

    public void Save()
    {
        PlayerPrefsX.SetBoolArray("StockArray",StockCheck);
    }

    public void ScrollRight()
    {
        if (index < info.Length)
        {
            index++;

            if (info[index].inStock && info[index].isChosen)
            {
                priceText.text = "CHOSEN";
                buyBttn.interactable = false;
            }
            else if (!info[index].inStock)
            {
                priceText.text = info[index].cost.ToString();
                buyBttn.interactable = true;
            }
            else if (info[index].inStock && !info[index].isChosen)
            {
                priceText.text = "CHOOSE";
                buyBttn.interactable = true;
            }
                _meshRenderer.material = info[index].material;
            // Можно записать так: player.GetChild(index-1).gameObject.SetActive(false);

            //player.GetChild(index).gameObject.SetActive(true);
        }
    }

    public void ScrollLeft()
    {
        if (index > 0)
        {
            index--;

            if (info[index].inStock && info[index].isChosen)
            {
                priceText.text = "CHOSEN";
                buyBttn.interactable = false;
            }
            else if (!info[index].inStock)
            {
                priceText.text = info[index].cost.ToString();
                buyBttn.interactable = true;
            }
            else if (info[index].inStock && !info[index].isChosen)
            {
                priceText.text = "CHOOSE";
                buyBttn.interactable = true;
            }

            _meshRenderer.material = info[index].material;

            //player.GetChild(index).gameObject.SetActive(true);
        }
    }

    public void BuyButtonAction()
    {
        if (buyBttn.interactable && !info[index].inStock)
        {
            if (coins > int.Parse(priceText.text))
            {
                coins -= int.Parse(priceText.text);
                coinsText.text = coins.ToString();
                PlayerPrefs.SetInt("Coin", coins);
                StockCheck[index] = true;
                info[index].inStock = true;
                priceText.text = "CHOOSE";
                Save();
            }
        } 

        if (buyBttn.interactable && !info[index].isChosen && info[index].inStock) 
        {
            PlayerPrefs.SetInt("chosenSkin", index);
            buyBttn.interactable = false;
            priceText.text = "CHOSEN";
        }
    }
}


[System.Serializable]
public class Skin
{
    public int cost;
    public Material material;
    public bool inStock;
    public bool isChosen;
}