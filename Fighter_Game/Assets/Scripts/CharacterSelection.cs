using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterSelection : MonoBehaviour
{
    public GameObject[] characters;
    public int selectedCharacter = 0;
    public Text characterName;
    public Text playerName;
    public Text playerName2;
    public Text masterPlayerReadyText;
    public Text LocalPlayerReadyText;

    private ExitGames.Client.Photon.Hashtable playerCustomProperties = new ExitGames.Client.Photon.Hashtable();
    private bool playerReady = false;

    public void Start()
    {
        characters[0].SetActive(true);
        characterName.text = characters[0].name;
        
        playerCustomProperties["PlayerReady"] = playerReady;
        PhotonNetwork.PlayerList[0].CustomProperties = playerCustomProperties;
        PhotonNetwork.PlayerList[1].CustomProperties = playerCustomProperties;

        //masterPlayerReadyText.text = "Ready: " + (bool)playerCustomProperties["PlayerReady"];
    }

    public void Update()
    {
        playerName.text = PhotonNetwork.PlayerList[0].NickName;
        if (PhotonNetwork.PlayerList[0].CustomProperties.ContainsKey("PlayerReady"))
        {
            masterPlayerReadyText.text = "Ready: " + (bool)PhotonNetwork.PlayerList[0].CustomProperties["PlayerReady"];
        }

        playerName2.text = PhotonNetwork.PlayerList[1].NickName;
        if (PhotonNetwork.PlayerList[1].CustomProperties.ContainsKey("PlayerReady"))
        {
            LocalPlayerReadyText.text = "Ready: " + (bool)PhotonNetwork.PlayerList[1].CustomProperties["PlayerReady"];
        }
        
        
    }

    public void NextCharacter()
    {
        characters[selectedCharacter].SetActive(false);
        selectedCharacter = (selectedCharacter + 1) % characters.Length;
        characters[selectedCharacter].SetActive(true);
        characterName.text = characters[selectedCharacter].name;
    }

    public void PreviousCharacter()
    {
        characters[selectedCharacter].SetActive(false);
        selectedCharacter--;
        if (selectedCharacter < 0)
        {
            selectedCharacter += characters.Length;
        }
        characters[selectedCharacter].SetActive(true);
        characterName.text = characters[selectedCharacter].name;

    }

    public void StartGame()
    {
        Debug.Log(PhotonNetwork.LocalPlayer.NickName);
        PlayerPrefs.SetInt("SelectedCharacter", selectedCharacter);
        playerReady = true ;
        playerCustomProperties["PlayerReady"] = playerReady;

        if (PhotonNetwork.IsMasterClient)
        {
            PhotonNetwork.MasterClient.CustomProperties = playerCustomProperties;
        }
        else
        {
            PhotonNetwork.LocalPlayer.CustomProperties = playerCustomProperties;
        }

        //if (AllPlayersReady())
        //{
        //    playerName.text = "All Ready";
        //}

    }

    private bool AllPlayersReady()
    {

       foreach (var photonPlayer in PhotonNetwork.PlayerList)
       {
            if (photonPlayer.CustomProperties.ContainsKey("PlayerReady"))
            {
                bool temp = (bool)photonPlayer.CustomProperties["PlayerReady"];
                if (!temp)
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

       return true;
    }
    
}
