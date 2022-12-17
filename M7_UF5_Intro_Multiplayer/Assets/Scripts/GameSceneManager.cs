using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;

public class GameSceneManager : MonoBehaviourPunCallbacks
{

    public static GameSceneManager instance;
    
    
    private void Awake()
    {
        instance = this;
    }


    private void Start()
    {
        // Revisamos si estamos conectados un servidor de Photon
        if (PhotonNetwork.IsConnected)
        {
            // Revisamos que nuestra variable no sea null
            if (GameManager.instance.SelectedSkinPlayer != null)
            {
                int randomPoint = Random.Range(-4, 5);
                
                // Instanciamos nuestro jugador en una posición random (limitada) del mapa
                PhotonNetwork.Instantiate(GameManager.instance.SelectedSkinPlayer.name, new Vector3(randomPoint, 2, randomPoint),
                    Quaternion.identity);
                
            }
        }
    }
    

    public override void OnJoinedRoom()
    {
        Debug.Log(PhotonNetwork.NickName + " se ha conectado a " + PhotonNetwork.CurrentRoom.Name);
    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        Debug.Log(newPlayer.NickName + " se ha conectado a " + PhotonNetwork.CurrentRoom.Name +
                  " -- Numero players: " + PhotonNetwork.CurrentRoom.PlayerCount);
    }

    
    public override void OnLeftRoom()
    {
        SceneManager.LoadScene("GameLauncherScene");
    }

    
    public void LeaveRoom()
    {
        PhotonNetwork.LeaveRoom();
    }
}
