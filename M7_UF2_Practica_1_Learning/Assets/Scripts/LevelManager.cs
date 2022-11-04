using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    
    [Header("---- LEVEL 1 ----")]
    [SerializeField] private GameObject check_icon_cube_green;
    [SerializeField] private GameObject check_icon_cube_red;
    [SerializeField] private GameObject check_icon_cube_coins;

    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip clip_Victoria;
    [SerializeField] private AudioClip clip_Pickup_moneda;


    public GameObject portal;
    public bool isGreenCubeOK;
    public bool isRedCubeOK;
    
    public TMP_Text text_counter_coins;
    public int maxCoins;
    public int currentCoins;

    
    [Header("---- LEVEL FINISH ----")]
    public TMP_Text timer_txt;
    private float totalTime;
    private int minutes, seconds, cents;

    

    private void Start()
    {
        if (SceneManager.GetActiveScene().name.Equals("Level_1"))
        {
            Cursor.lockState = CursorLockMode.Locked; // Evitamos que el cursor salga de la pantalla de Play si no pulsamos espacio
            GameManager.instance.currentLevelManager = this;
            GameManager.instance.ResetAll();
            GameManager.instance.coins = currentCoins;
            text_counter_coins.text = "<color=orange>Coins " + currentCoins + "/" + maxCoins + "</color>";
        } else if (SceneManager.GetActiveScene().name.Equals("Level_Finish"))
        {
            
            Cursor.lockState = CursorLockMode.None;
            totalTime = GameManager.instance.timeToCompleteGame;

            minutes = (int) (totalTime / 60);
            seconds = (int) (totalTime - minutes * 60);
            cents = (int) ((totalTime - (int) totalTime) * 100f);

            timer_txt.text = string.Format("{0:00}:{1:00}:{2:00}", minutes, seconds, cents);
        }
        
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
            audioSource.PlayOneShot(clip_Victoria);
        } else
        {
            portal.SetActive(false);
        }
    }

    public void IncreaseCounterCoins()
    {
        currentCoins += 1;
        audioSource.PlayOneShot(clip_Pickup_moneda);
        GameManager.instance.coins = currentCoins;
        text_counter_coins.text = "<color=orange>Coins " + currentCoins + "/" + maxCoins + "</color>";
        CheckStateObjetives();
    }
    
    
    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public void ExitGame()
    {
        Application.Quit();
    }
    
}
