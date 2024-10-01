using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DataManager : MonoBehaviour
{
    public static DataManager Instance;

    [Header("Coin Texts")]
    [SerializeField] private Text[] coinTexts;
    private int coins;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }

        coins = PlayerPrefs.GetInt("coins",0);
    }


  
    void Start()
    {
        UpdateCoinsTexts();


    }

    void Update()
    {
        
    }

    private void UpdateCoinsTexts()
    {
        foreach (Text coinText in coinTexts)
        {
            coinText.text = coins.ToString();
        }
    }

    public void AddCoins(int amount)
    {
        coins += amount;

        UpdateCoinsTexts();

        PlayerPrefs.SetInt  ("coins", coins);
    }


}
