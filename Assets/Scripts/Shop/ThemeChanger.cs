using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ThemeChanger : MonoBehaviour
{
    public Theme[] info;
    private bool[] StockThemeCheck;

    [SerializeField] private Button buyBttn;
    
    [SerializeField] private Image _themeImage;
    [SerializeField] private Material _fetch;
    [SerializeField] private Material _tree;
    [SerializeField] private Material _leaves;
    public TextMeshProUGUI priceText;
    public TextMeshProUGUI coinsText;

    public int index;

    public int coins;
    static private bool _restart;
    private void Awake()
    {
        if (_restart == true)
        {
            _restart = false;
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
        coins = PlayerPrefs.GetInt("Coin");
        index = PlayerPrefs.GetInt("chosenTheme");
        coinsText.text = coins.ToString();

        StockThemeCheck = new bool[53];
        if (PlayerPrefs.HasKey("StockThemeArray"))
            StockThemeCheck = PlayerPrefsX.GetBoolArray("StockThemeArray");

        else
            StockThemeCheck[0] = true;

        info[index].isChosen = true;

        for (int i = 0; i < info.Length; i++)
        {
            info[i].inStock = StockThemeCheck[i];

            if (i == index)
            {
                
                _themeImage.sprite = info[i].themeImage.sprite;
                SetMaterialPropereties(info[i].fetchMaterial,_fetch);
                SetMaterialPropereties(info[i].treeMaterial,_tree);
                SetMaterialPropereties(info[i].leavesMaterial,_leaves);

            }
                
        }

        priceText.text = "CHOSEN";
        buyBttn.interactable = false;
    }

    private void SetMaterialPropereties(Material from, Material to)
    {
        to.SetColor("_Color",from.GetColor("_Color"));
    }



    public void Save()
    {
        PlayerPrefsX.SetBoolArray("StockThemeArray", StockThemeCheck);
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
            _themeImage.sprite = info[index].themeImage.sprite;
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

            _themeImage.sprite = info[index].themeImage.sprite;

            //player.GetChild(index).gameObject.SetActive(true);
        }
    }
    public void SetChoosenSkin()
    {
        index = PlayerPrefs.GetInt("chosenTheme");
        _themeImage.sprite = info[index].themeImage.sprite;
        SetMaterialPropereties(info[index].fetchMaterial, _fetch);
        SetMaterialPropereties(info[index].treeMaterial, _tree);
        SetMaterialPropereties(info[index].leavesMaterial, _leaves);
        _restart = true;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
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
                StockThemeCheck[index] = true;
                info[index].inStock = true;
                priceText.text = "CHOOSE";
                Save();
            }
        }

        if (buyBttn.interactable && !info[index].isChosen && info[index].inStock)
        {
            PlayerPrefs.SetInt("chosenTheme", index);
            SetChoosenSkin();
            buyBttn.interactable = false;
            priceText.text = "CHOSEN";
        }
    }
}
[System.Serializable]
public class Theme
{
    public int cost;
    public Image themeImage;
    public Material leavesMaterial;
    public Material treeMaterial;
    public Material fetchMaterial;
    public bool inStock;
    public bool isChosen;
}