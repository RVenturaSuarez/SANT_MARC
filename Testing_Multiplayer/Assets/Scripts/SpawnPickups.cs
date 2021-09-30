using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;
using Random = UnityEngine.Random;

public class SpawnPickups : MonoBehaviour
{
    public static SpawnPickups instancia;
    public GameObject pickup_prefab;
    public int number_of_pickup_level;
    public bool picks_instanciados;
    
    public float minX;
    public float maxX;
    public float minY;
    public float maxY;

    private void Awake()
    {
        if (instancia == null)
        {
            instancia = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);   
        }
    }

    private void Start()
    {
        if (!picks_instanciados)
        {
            InstantiatePickups(number_of_pickup_level);
            picks_instanciados = true;
        }
    }


    private void InstantiatePickups(int number_of_pickups)
    {
        for (int i = 0; i < number_of_pickups; i++)
        {
            Vector2 randomPosition = new Vector2(Random.Range(minX, maxX), Random.Range(minY, maxY));
            PhotonNetwork.Instantiate(pickup_prefab.name, new Vector3(randomPosition.x, 1.5f, randomPosition.y), Quaternion.identity);    
        }
    }
}
