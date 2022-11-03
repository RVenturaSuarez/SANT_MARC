using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    
    [Header("---- LEVEL 1 ----")]
    [SerializeField] private GameObject check_icon_cube_green;
    [SerializeField] private GameObject check_icon_cube_red;
    [SerializeField] private GameObject check_icon_cube_coins;


    public GameObject portal;
    public bool isGreenCubeOK;
    public bool isRedCubeOK;
    
    public TMP_Text text_counter_coins;
    public int maxCoins;
    public int currentCoins;

    

    private void Start()
    {
        GameManager.instance.currentLevelManager = this;
        GameManager.instance.coins = currentCoins;
        text_counter_coins.text = "<color=orange>Coins " + currentCoins + "/" + maxCoins + "</color>";

    }

    
    /// <summary>
    ///  Método para mostrar los iconos de los objetivos que se han cumplido
    /// </summary>
    public void Check_icons_objetives()
    {
        if (isGreenCubeOK)
        {
            check_icon_cube_green.SetActive(true);
        }
        else
        {
            check_icon_cube_green.SetActive(false);
        }


        if (isRedCubeOK)
        {
            check_icon_cube_red.SetActive(true);
        }
        else
        {
            check_icon_cube_red.SetActive(false);
        }


        if (currentCoins == maxCoins)
        {
            check_icon_cube_coins.SetActive(true);
        }
        
    }
    
    
    /// <summary>
    ///  Método para verificar el estado de los objetivos si se han cumplido
    /// </summary>
    public void CheckStateObjetives()
    {
        Check_icons_objetives();
        
        if (isGreenCubeOK && isRedCubeOK && currentCoins == maxCoins)
        {
            portal.SetActive(true);
        } else
        {
            portal.SetActive(false);
        }
    }

    public void IncreaseCounterCoins()
    {
        currentCoins += 1;
        GameManager.instance.coins = currentCoins;
        text_counter_coins.text = "<color=orange>Coins " + currentCoins + "/" + maxCoins + "</color>";
        CheckStateObjetives();
    }
    
}
