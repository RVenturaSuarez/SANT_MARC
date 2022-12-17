using System;
using System.Collections;
using System.Collections.Generic;
using EasyUI.Toast;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using TMPro;
using UnityEngine.SceneManagement;
using Hashtable = ExitGames.Client.Photon.Hashtable;
using Random = UnityEngine.Random;

public class LaunchManager : MonoBehaviourPunCallbacks
{
    [Header("---- PANELS ----"), Space(10)] 
    [SerializeField] private GameObject panel_enter_game;
    [SerializeField] private GameObject panel_conection_status;
    [SerializeField] private GameObject panel_createOrJoinRoom;
    [SerializeField] private GameObject lobbyRoomPanel;

    [SerializeField] private List<GameObject> panels_list = new List<GameObject>();


    [Header("---- OBJECTS PANEL ENTER GAME ----"),Space(10)]
    [SerializeField] private TMP_InputField playerName_inputField;
    
    [Header("---- OBJECTS PANEL LOBBY ----"),Space(10)]
    [SerializeField] private TextMeshProUGUI usuarios_conectados_info_txt;
    [SerializeField] private GameObject content_ListaUsuarios_conectados;
    [SerializeField] private Item_info_UsuarioConectado _item_info_UsuarioConectado;
    [SerializeField] private MeshRenderer playerUIRenderer;
    [SerializeField] private GameObject startGame_btn;

    // Private variables
    private int currentIndexListSkins;
    private int lengthArraySkins;
    
    
    private void Awake()
    {
        PhotonNetwork.AutomaticallySyncScene = true;
    }

    private void Start()
    {
        OcultarTodosLosPaneles();
        panel_enter_game.SetActive(true);
        lengthArraySkins = GameManager.instance.ArraySkins.Length;
    }

    
    
    [PunRPC]
    public void Actualizar_Lista_Usuarios()
    {
        LimpiarListaUsuarios();
        
        
        // Recorremos toda la lista de usuarios capturados del JSON y creamos un botón en la UI por cada uno.
        foreach (var player in PhotonNetwork.PlayerList)
        {
            Item_info_UsuarioConectado itemListaUsuariosConectados;
            itemListaUsuariosConectados = Instantiate(_item_info_UsuarioConectado, content_ListaUsuarios_conectados.transform);
            itemListaUsuariosConectados.nickname_usuario = player.NickName;
            itemListaUsuariosConectados.name = player.NickName;
        }

        usuarios_conectados_info_txt.text = $"Usuarios conectados {PhotonNetwork.CurrentRoom.PlayerCount}/{PhotonNetwork.CurrentRoom.MaxPlayers}";
        
        if ((PhotonNetwork.CurrentRoom.PlayerCount == PhotonNetwork.CurrentRoom.MaxPlayers) && PhotonNetwork.IsMasterClient)
        {
            startGame_btn.SetActive(true);
        }
        else
        {
            startGame_btn.SetActive(false);
        }
    }
    
    /// <summary>
    /// Método que destruye todos los elementos del content del scroll view para luego poder cargar la info nueva
    /// </summary>
    private void LimpiarListaUsuarios()
    {
        foreach (Transform child in content_ListaUsuarios_conectados.transform)
        {
            Destroy(child.gameObject);
        }
    }


    public void NextSkin()
    {
        // Recorremos toda la lista de usuarios capturados del JSON y creamos un botón en la UI por cada uno.
        foreach (var player in PhotonNetwork.PlayerList)
        {
            if (player.Equals(PhotonNetwork.LocalPlayer))
            {
                currentIndexListSkins = (currentIndexListSkins + 1) % lengthArraySkins;
                playerUIRenderer.material = GameManager.instance.ArraySkins[currentIndexListSkins];
                
                // Seteamos la propiedad del player para indicar el skin seleccionado
                player.CustomProperties["skinIndex"] = currentIndexListSkins;
                GameManager.instance.SelectedSkinPlayer = GameManager.instance.ArrayAvailableSkinsPlayers[currentIndexListSkins];
            }
        }
    }
    
    public void BackSkin()
    {
        // Recorremos toda la lista de usuarios capturados del JSON y creamos un botón en la UI por cada uno.
        foreach (var player in PhotonNetwork.PlayerList)
        {
            if (player.Equals(PhotonNetwork.LocalPlayer))
            {
                if (currentIndexListSkins -1 < 0)
                {
                    currentIndexListSkins = lengthArraySkins - 1;
                }
                else
                {
                    currentIndexListSkins--;
                }
                
                playerUIRenderer.material = GameManager.instance.ArraySkins[currentIndexListSkins];
                
                // Seteamos la propiedad del player para indicar el skin seleccionado
                player.CustomProperties["skinIndex"] = currentIndexListSkins;
                GameManager.instance.SelectedSkinPlayer = GameManager.instance.ArrayAvailableSkinsPlayers[currentIndexListSkins];
            }
        }
    }



    /// <summary>
    /// Método de ejemplo para tratar tratar las propiedades de un jugador y mostrarlas por LOG
    /// </summary>
    public void LogCustomProperties()
    {
        foreach (var player in PhotonNetwork.PlayerList)
        {
            if (player.CustomProperties.ContainsKey("skinIndex"))
            {
                object skinValue;
                if (player.CustomProperties.TryGetValue("skinIndex", out skinValue))
                {
                    Debug.Log(skinValue.ToString());
                }
            }
            else
            {
                Debug.Log("No existe la propiedad");
            }
        }
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
        roomOptions.MaxPlayers = 2;

        PhotonNetwork.CreateRoom(randomRoomName, roomOptions);

    }
    

    #region Override Photon

    public override void OnConnectedToMaster()
    {
        Debug.Log("Conectando a los servidores de Photon con el usuario " + PhotonNetwork.NickName + " " + PhotonNetwork.LocalPlayer.NickName);
        OcultarTodosLosPaneles();
        panel_createOrJoinRoom.SetActive(true);
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
        Debug.Log(PhotonNetwork.NickName + " se ha conectado a " + PhotonNetwork.CurrentRoom.Name);
        
        // Creamos la propiedad skinIndex para el player que se acaba de conectar para luego poder actualizarla
        // ya que indicara que skin ha escogido para ir al juego
        Hashtable player_skin_propertie = new Hashtable();
        player_skin_propertie.Add("skinIndex",0);
        PhotonNetwork.LocalPlayer.CustomProperties = player_skin_propertie;

        
        lobbyRoomPanel.SetActive(true);
        photonView.RPC("Actualizar_Lista_Usuarios", RpcTarget.All);
        
    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        Debug.Log(newPlayer.NickName + " se ha conectado a " + PhotonNetwork.CurrentRoom.Name);
        Debug.Log("Numero de players: " + PhotonNetwork.CurrentRoom.PlayerCount);
    }

    #endregion


    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        photonView.RPC("Actualizar_Lista_Usuarios", RpcTarget.All);
    }

    public void LeaveRoom()
    {
        OcultarTodosLosPaneles();
        PhotonNetwork.LeaveRoom();
        panel_createOrJoinRoom.SetActive(true);
    }

    [PunRPC]
    public void LoadGameScene_PunRPC()
    {
        PhotonNetwork.CurrentRoom.IsOpen = false;
        PhotonNetwork.CurrentRoom.IsVisible = false;
        PhotonNetwork.LoadLevel("GameScene");
    }

    private void OcultarTodosLosPaneles()
    {
        foreach (GameObject panel in panels_list)
        {
            panel.SetActive(false);
        }
    }
    
}
