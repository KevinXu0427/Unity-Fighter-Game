using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UI;

public class NetworkManager : MonoBehaviourPunCallbacks
{
    [SerializeField] private GameObject findOpponentPanel = null;
    [SerializeField] private GameObject waitingStatusPanel = null;
    [SerializeField] private Text waitingStatusText = null;
    [SerializeField] private Text playerCountsText = null;

    private AppSettings version = new AppSettings()
    {
        AppVersion = "1.0",
        AppIdRealtime = "f0ca9ec0-ac45-41ad-9255-55421b492c7b"
    };

    private bool isConnecting = false;
    private const int MaxPlayerPerRoom = 2;

    private ExitGames.Client.Photon.Hashtable playerCustomProperties = new ExitGames.Client.Photon.Hashtable();

    private void Awake()
    {
        PhotonNetwork.AutomaticallySyncScene = true;
    }

    public void FindOpponent()
    {
        isConnecting = true;

        findOpponentPanel.SetActive(false);
        waitingStatusPanel.SetActive(true);

        waitingStatusText.text = "Searching...";
        playerCountsText.text = PhotonNetwork.PlayerList.Length + " Player(s) Connected.";

        if (PhotonNetwork.IsConnected)
        {
            playerCountsText.text = PhotonNetwork.PlayerList.Length + " Player(s) Connected.";
            PhotonNetwork.JoinRandomRoom();
        }
        else
        {
            PhotonNetwork.ConnectUsingSettings(version);
        }
    }

    public override void OnConnectedToMaster()
    {
        Debug.Log("Connected to Master");

        if (isConnecting)
        {
            PhotonNetwork.JoinRandomRoom();
        }

        //StartCoroutine(SetCP());
        //StartCoroutine(ShowCP());
    }

    public override void OnDisconnected(DisconnectCause cause)
    {
        waitingStatusPanel.SetActive(false);
        findOpponentPanel.SetActive(true);

        Debug.Log($"Disconnected due to: {cause}");
    }

    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        Debug.Log("No clients are waiting for an opponent, creating a new room");

        PhotonNetwork.CreateRoom(null, new RoomOptions { MaxPlayers = MaxPlayerPerRoom });
    }

    public override void OnJoinedRoom()
    {
        Debug.Log("Client successfully joined a room");

        int playerCount = PhotonNetwork.CurrentRoom.PlayerCount;

        if (playerCount != MaxPlayerPerRoom)
        {
            waitingStatusText.text = "Waiting for opponent";
            playerCountsText.text = PhotonNetwork.PlayerList.Length + " Player(s) Connected.";
            Debug.Log("Client is waiting for an opponent");
        }
        else
        {
            waitingStatusText.text = "Opponent Found";
            playerCountsText.text = PhotonNetwork.PlayerList.Length + " Player(s) Connected.";
            Debug.Log("Matching is ready to begin");
        }
    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        if (PhotonNetwork.CurrentRoom.PlayerCount == MaxPlayerPerRoom)
        {
            PhotonNetwork.CurrentRoom.IsOpen = false;

            waitingStatusText.text = "Opponent Found";
            Debug.Log("Matching is ready to begin");
            
            PhotonNetwork.LoadLevel("CharacterSelectionMenu");
        }
    }

    private IEnumerator SetCP()
    {
        while(PhotonNetwork.IsConnected)
        {
            playerCustomProperties["PlayerReady"] = false;
            PhotonNetwork.LocalPlayer.SetCustomProperties(playerCustomProperties);

            yield return new WaitForSeconds(0.5f);
        }

        yield break;
    }

    private IEnumerator ShowCP()
    {
        while (PhotonNetwork.IsConnected)
        {
            bool ready = (bool)PhotonNetwork.LocalPlayer.CustomProperties["PlayerReady"];
            Debug.Log("Player ready: " + ready);
            yield return new WaitForSeconds(0.5f);
        }

        yield break;
    }
}
