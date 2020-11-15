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
        //PhotonNetwork.LocalPlayer.SetCustomProperties(playerCustomProperties);
        //PhotonNetwork.MasterClient.SetCustomProperties(playerCustomProperties);

        //playerName.text = PhotonNetwork.LocalPlayer.NickName;
        //playerName2.text = PhotonNetwork.MasterClient.NickName;

        foreach (var player in PhotonNetwork.PlayerList)
        {
            if (player.IsMasterClient && player.IsLocal)
            {
                player.SetCustomProperties(playerCustomProperties);
                playerName2.text = player.NickName;

            }
            else
            {
                player.SetCustomProperties(playerCustomProperties);
                playerName.text = player.NickName;
            }
        }

        //masterPlayerReadyText.text = "Ready: " + (bool)playerCustomProperties["PlayerReady"];
    }

    public void Update()
    {
        StartCoroutine(SetCP());
        StartCoroutine(ShowCP());

        //if (PhotonNetwork.PlayerList[0].CustomProperties.ContainsKey("PlayerReady"))
        //{
        //    masterPlayerReadyText.text = "Ready: " + (bool)PhotonNetwork.PlayerList[0].CustomProperties["PlayerReady"];
        //}
        //else if (PhotonNetwork.PlayerList[1].CustomProperties.ContainsKey("PlayerReady"))
        //{
        //    LocalPlayerReadyText.text = "Ready: " + (bool)PhotonNetwork.PlayerList[1].CustomProperties["PlayerReady"];
        //}
        
        
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
        foreach (var player in PhotonNetwork.PlayerList)
        {
            if (player.IsMasterClient && player.IsLocal)
            {
                player.CustomProperties = playerCustomProperties;
                Debug.Log("MasterClient clicked");

            }
            else
            {
                player.CustomProperties = playerCustomProperties;
                Debug.Log("LocalPlayer ready clicked");

            }

        }


        //if (PhotonNetwork.IsMasterClient)
        //{
        //    PhotonNetwork.MasterClient.CustomProperties = playerCustomProperties;
        //    Debug.Log("MasterClient clicked");

        //}
        //else
        //{
        //    PhotonNetwork.LocalPlayer.CustomProperties = playerCustomProperties;
        //    Debug.Log("LocalPlayer ready clicked");

        //}

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

    private IEnumerator SetCP()
    {
        while (PhotonNetwork.IsConnected)
        {
            //if (PhotonNetwork.IsMasterClient)
            //{
            //    playerCustomProperties["PlayerReady"] = playerReady;
            //    PhotonNetwork.MasterClient.SetCustomProperties(playerCustomProperties);
            //}
            //else
            //{
            //    playerCustomProperties["PlayerReady"] = playerReady;
            //    PhotonNetwork.LocalPlayer.SetCustomProperties(playerCustomProperties);
            //}

            foreach (var player in PhotonNetwork.PlayerList)
            {
                if (player.IsMasterClient && player.IsLocal)
                {

                    playerCustomProperties["PlayerReady"] = playerReady;
                    player.SetCustomProperties(playerCustomProperties);

                }
                else
                {
                    playerCustomProperties["PlayerReady"] = playerReady;
                    player.SetCustomProperties(playerCustomProperties);
                }
            }

            //playerCustomProperties["PlayerReady"] = playerReady;
            //PhotonNetwork.LocalPlayer.SetCustomProperties(playerCustomProperties);

            yield return new WaitForSeconds(2f);
        }

        yield break;
    }

    private IEnumerator ShowCP()
    {
        while (PhotonNetwork.IsConnected)
        {
            bool ready = false;

            //if (PhotonNetwork.IsMasterClient)
            //{
            //    ready = (bool)PhotonNetwork.MasterClient.CustomProperties["PlayerReady"];
            //    masterPlayerReadyText.text = "Ready: " + ready;
            //    Debug.Log("MasterClient ready: " + ready);

            //}
            //else
            //{
            //    if (PhotonNetwork.LocalPlayer == null)
            //    {
            //        Debug.Log("LocalPlayer is missing.");
            //    }
            //    ready = (bool)PhotonNetwork.LocalPlayer.CustomProperties["PlayerReady"];
            //    LocalPlayerReadyText.text = "Ready: " + ready;

            //    Debug.Log("LocalPlayer ready: " + ready);
            //}


            foreach (var player in PhotonNetwork.PlayerList)
            {
                if (player.IsMasterClient && player.IsLocal)
                {
                    ready = (bool)player.CustomProperties["PlayerReady"];
                    masterPlayerReadyText.text = "Ready: " + ready;
                    Debug.Log("MasterClient ready: " + ready);

                }
                else
                {
                    ready = (bool)player.CustomProperties["PlayerReady"];
                    LocalPlayerReadyText.text = "Ready: " + ready;
                    Debug.Log("LocalPlayer ready: " + ready);
                }
            }

            //bool ready = (bool)PhotonNetwork.LocalPlayer.CustomProperties["PlayerReady"];
            //Debug.Log("Player ready: " + ready);

            yield return new WaitForSeconds(4f);
        }

        yield break;
    }
}
