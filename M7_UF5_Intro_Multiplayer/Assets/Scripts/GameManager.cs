using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    
    [SerializeField] private Material[] arraySkins;
    [SerializeField] private GameObject[] arrayAvailableSkinsPlayers;
    [SerializeField] private GameObject selectedSkinPlayer;
    public Material[] ArraySkins
    {
        get => arraySkins;
        set => arraySkins = value;
    }
    
    public GameObject[] ArrayAvailableSkinsPlayers
    {
        get => arrayAvailableSkinsPlayers;
        set => arrayAvailableSkinsPlayers = value;
    }


    public GameObject SelectedSkinPlayer
    {
        get => selectedSkinPlayer;
        set => selectedSkinPlayer = value;
    }
    
    private void Awake()
    {
        if (instance != null) 
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    private void Start()
    {
        // Seleccionamos por defecto el primer elemento de la lista de skins del player
        selectedSkinPlayer = arrayAvailableSkinsPlayers[0];
    }
}
