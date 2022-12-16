using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using TMPro;
using UnityEngine;

public class PlayerSetup : MonoBehaviourPunCallbacks
{

    [SerializeField] private GameObject playerCamera;
    [SerializeField] private TextMeshProUGUI nombre_jugador;
    
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
        
        Actualizar_Nombre();
        
    }


    private void Actualizar_Nombre()
    {
        nombre_jugador.text = photonView.Owner.NickName;
    }
}
