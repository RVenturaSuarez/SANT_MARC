using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class PlayerSetup : MonoBehaviourPunCallbacks
{

    [SerializeField] private GameObject playerCamera;
    
    void Start()
    {
        // Comprobamos que el prefab que tiene el componente PhotonView está siendo poseido por nosotros
        // Dependiendo si lo está o no activamos el script del playerController y la cámara del jugador
        if (photonView.IsMine)
        {
            transform.GetComponent<PlayerController>().enabled = true;
            playerCamera.GetComponent<Camera>().enabled = true;
        }
        else
        {
            transform.GetComponent<PlayerController>().enabled = false;
            playerCamera.GetComponent<Camera>().enabled = false;
        }
        
    }
    
}
