using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    public static GameManager instance;
    public LevelManager currentLevelManager;
    public int coins;
    public float timeToCompleteGame;
    public bool gameOver = true;
    
    
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void Update()
    {
        if (!gameOver)
        {
            timeToCompleteGame += Time.deltaTime;
        }

    }

    public void ResetAll()
    {
        coins = 0;
        timeToCompleteGame = 0f;
        gameOver = false;
    }
    
}
