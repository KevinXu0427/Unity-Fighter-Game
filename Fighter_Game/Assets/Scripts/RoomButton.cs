using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RoomButton : MonoBehaviour
{
    public Text roomNameText;
    public Text roomSizeText;

    public string roomName;
    public int roomSize;

    public void SetRoom()
    {
        roomNameText.text = roomName;
        roomSizeText.text = roomSize.ToString();
    }


    public void JoinRoomOnClick()
    {
        PhotonNetwork.JoinRoom(roomName);
    }

}
