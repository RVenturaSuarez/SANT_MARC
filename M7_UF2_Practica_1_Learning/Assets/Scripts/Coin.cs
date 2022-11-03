using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    
    void Update()
    {
        transform.Rotate(Vector3.right * 20 * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            GameManager.instance.currentLevelManager.IncreaseCounterCoins();
            Destroy(gameObject);
        }
    }
}
