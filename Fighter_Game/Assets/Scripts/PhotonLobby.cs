using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UI;

public class PhotonLobby : MonoBehaviourPunCallbacks, ILobbyCallbacks
{
    public string PlayerName;
    public string roomName;
    public GameObject roomListPrefab;
    public Transform roomsPanel;

    public GameObject lobbyGO;
    public GameObject roomGO;
    public Transform playersPanel;
    public GameObject playerListPrefab;
    public GameObject startButton;
    

    private AppSettings version = new AppSettings()
    {
        AppVersion = "1.0",
        AppIdRealtime = "f0ca9ec0-ac45-41ad-9255-55421b492c7b"
    };

    private void Start()
    {
        // Connects to Master photon server.
        PhotonNetwork.ConnectUsingSettings(version);
    }

    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        base.OnRoomListUpdate(roomList);
        RemoveRoomList();
        foreach (RoomInfo room in roomList)
        {
            ListRoom(room);
        }
    }

    void RemoveRoomList()
    {
        for (int i = roomsPanel.childCount - 1; i >= 0; --i)
        {
            Destroy(roomsPanel.GetChild(i).gameObject);
        }
    }

    void ListRoom(RoomInfo room)
    {
        if (room.IsOpen && room.IsVisible)
        {
            GameObject tempList = Instantiate(roomListPrefab, roomsPanel);
            RoomButton tempButton = tempList.GetComponent<RoomButton>();
            tempButton.roomName = room.Name;
            tempButton.roomSize = room.MaxPlayers;
            tempButton.SetRoom();
        }
    }



    public override void OnConnectedToMaster()
    {
        Debug.Log("Player has connected to the Photon master server.");
        PhotonNetwork.AutomaticallySyncScene = true;
    }

    public void CreateRoom()
    {
        Debug.Log("Trying to create a new room.");
        RoomOptions roomOps = new RoomOptions() { IsVisible = true, IsOpen = true, MaxPlayers = 2 };
        PhotonNetwork.CreateRoom(roomName, roomOps);
    }

    public override void OnJoinedRoom()
    {
        base.OnJoinedRoom();
        Debug.Log("Joined a room.");

        lobbyGO.SetActive(false);
        roomGO.SetActive(true);
        if (PhotonNetwork.IsMasterClient)
        {
            startButton.SetActive(true);
        }
        ClearPlayerList();
        ListPlayer();
        
    }

    void ClearPlayerList()
    {
        for (int i = playersPanel.childCount - 1; i >= 0; --i)
        {
            Destroy(playersPanel.GetChild(i).gameObject);
        }
    }

    void ListPlayer()
    {
        if (PhotonNetwork.InRoom)
        {
            foreach(Player player in PhotonNetwork.PlayerList)
            {
                GameObject tempList = Instantiate(playerListPrefab, playersPanel);
                Text tempText = tempList.transform.GetChild(0).GetComponent<Text>();
                tempText.text = player.NickName;
            }
        }
    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        base.OnPlayerEnteredRoom(newPlayer);
        ClearPlayerList();
        ListPlayer();
    }

    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        Debug.Log("Tried to create a new room but failed. There must already be a room with the same name.");
    }

    public void onRoomNameChanged(string nameIn)
    {
        roomName = nameIn;
    }

    public void onPlayerNameChanged(string nameIn)
    {
        PhotonNetwork.NickName = nameIn;
        PlayerPrefs.SetString("PlayerName", nameIn);
    }

    public void JointLobbyOnClick()
    {
        if (!PhotonNetwork.InLobby)
        {
            PhotonNetwork.JoinLobby();
        }
        else
        {
            Debug.Log("inLobby");
        }
    }

    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        base.OnPlayerLeftRoom(otherPlayer);
        ClearPlayerList();
        ListPlayer();
    }

    public void StartGame()
    {
        Debug.Log("Matching is ready to begin");
        PhotonNetwork.LoadLevel("CharacterSelectionMenu");
    }
}
