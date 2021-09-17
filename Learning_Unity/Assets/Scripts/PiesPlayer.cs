using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PiesPlayer : MonoBehaviour
{
    public PlayerController playerController;

    private void OnTriggerStay(Collider other)
    {
        Debug.Log("Puedo saltar true");
        playerController.CanJump = true;
    }

    private void OnTriggerExit(Collider other)
    {
        Debug.Log("Puedo saltar false");
        playerController.CanJump = false;
    }
}


