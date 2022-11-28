using System;
using System.Collections;
using System.Collections.Generic;
using EasyUI.Toast;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using TMPro;
using Random = UnityEngine.Random;

public class LaunchManager : MonoBehaviourPunCallbacks
{
    [Header("---- PANELS ----"), Space(10)] 
    [SerializeField] private GameObject panel_enter_game;
    [SerializeField] private GameObject panel_conection_status;
    [SerializeField] private GameObject panel_lobby;
    [SerializeField] private List<GameObject> panels_list = new List<GameObject>();


    [Header("---- OBJECTS PANEL ENTER GAME ----"),Space(10)]
    [SerializeField] private TMP_InputField playerName_inputField;

    private void Awake()
    {
        PhotonNetwork.AutomaticallySyncScene = true;
    }

    private void Start()
    {
        OcultarTodosLosPaneles();
        panel_enter_game.SetActive(true);
    }


    public void ConnectToPhotonServer()
    {
        // Verificamos que nuestro input field tenga texto
        if (string.IsNullOrEmpty(playerName_inputField.text))
        {
            Toast.Show("Necesitas introducir datos");
        }
        else
        {
            // Cambiamos el nickname de PhotonNetwork por el valor de texto del input field
            PhotonNetwork.NickName = playerName_inputField.text;
            PhotonNetwork.ConnectUsingSettings();
            // Mostramos al usuario el siguiente panel despues de establecer la conexión
            OcultarTodosLosPaneles();
            panel_conection_status.SetActive(true);
        }
    }

    // Método que trata de conectar al usuario a una Room aleatoria, en caso de no existir llamaremos
    // al método override OnJoinRandomFailed
    public void JoinRandomRoom()
    {
        PhotonNetwork.JoinRandomRoom();
    }
    
    private void CreateAndJoinRoom()
    {
        string randomRoomName = "Room " + Random.Range(0, 10000);

        RoomOptions roomOptions = new RoomOptions();
        roomOptions.IsOpen = true;
        roomOptions.IsVisible = true;
        roomOptions.MaxPlayers = 10;

        PhotonNetwork.CreateRoom(randomRoomName, roomOptions);

    }
    

    #region Override Photon

    public override void OnConnectedToMaster()
    {
        Debug.Log("Conectando a los servidores de Photon con el usuario " + PhotonNetwork.NickName);
        OcultarTodosLosPaneles();
        panel_lobby.SetActive(true);
    }

    public override void OnConnected()
    {
        Debug.Log("Conectando a Internet");
    }

    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        base.OnJoinRandomFailed(returnCode, message);
        CreateAndJoinRoom();
    }

    public override void OnJoinedRoom()
    {
        Debug.Log(PhotonNetwork.NickName +  " se ha conectado a " + PhotonNetwork.CurrentRoom.Name);
        PhotonNetwork.LoadLevel("GameScene");
    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        Debug.Log(newPlayer.NickName + " se ha conectado a " + PhotonNetwork.CurrentRoom.Name);
        Debug.Log("Numero de players: " + PhotonNetwork.CurrentRoom.PlayerCount);
    }

    #endregion

    

    private void OcultarTodosLosPaneles()
    {
        foreach (GameObject panel in panels_list)
        {
            panel.SetActive(false);
        }
    }
    
}
