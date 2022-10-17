using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public class CreateAndJoinRooms : MonoBehaviourPunCallbacks
{
    public InputField createRoom;
    public InputField joinRoom;

    public void CreateRoom(string roomName)
    {
        PhotonNetwork.CreateRoom(createRoom.text);
    }

    public void JoinRoom(string roomName)
    {
        PhotonNetwork.JoinRoom(joinRoom.text);
    }

    public override void OnJoinedRoom()
    {
        PhotonNetwork.LoadLevel("Game");
    }

    // -- Back At It Again
}
