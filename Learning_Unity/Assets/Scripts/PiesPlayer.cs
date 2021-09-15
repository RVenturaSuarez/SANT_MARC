using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PiesPlayer : MonoBehaviour
{
    public PlayerController playerController;

    private void OnTriggerStay(Collider other)
    {
        playerController.CanJump = true;
    }

    private void OnTriggerExit(Collider other)
    {
        playerController.CanJump = false;
    }
}


