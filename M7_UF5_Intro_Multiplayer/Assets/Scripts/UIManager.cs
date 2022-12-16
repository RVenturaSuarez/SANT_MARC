using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class UIManager : MonoBehaviourPunCallbacks
{

    [SerializeField] private TextMeshProUGUI log_txt;

    void Start()
    {
        photonView.RPC("Actualizar_Log_TXT", RpcTarget.All);
    }


    [PunRPC]
    private void Actualizar_Log_TXT()
    {
        log_txt.text = "Jugadores conectados: " + PhotonNetwork.PlayerList.Length + "\n";

        foreach (Player playerConectado in PhotonNetwork.PlayerList)
        {
            log_txt.text += $"[{playerConectado.NickName}]\n";
        }
    }

    
}
