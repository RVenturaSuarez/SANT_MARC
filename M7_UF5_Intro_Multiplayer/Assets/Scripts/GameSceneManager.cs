using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;

public class GameSceneManager : MonoBehaviourPunCallbacks
{
    public override void OnJoinedRoom()
    {
        Debug.Log(PhotonNetwork.NickName + " se ha conectado a " + PhotonNetwork.CurrentRoom.Name);
    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        Debug.Log(newPlayer.NickName + " se ha conectado a " + PhotonNetwork.CurrentRoom.Name +
                  " -- Numero players: " + PhotonNetwork.CurrentRoom.PlayerCount);
    }
}
