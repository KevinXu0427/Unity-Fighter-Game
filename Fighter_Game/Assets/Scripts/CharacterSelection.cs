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

    public PhotonView PV;
    
    public bool masterPlayerReady = false;
    public bool localPlayerReady = false;
    public bool loadedScene = false;

    public void Awake()
    {
        PV = GetComponent<PhotonView>();
        
        characters[0].SetActive(true);
        characterName.text = characters[0].name;

        localPlayerReady = false;
        masterPlayerReady = false;
    }

    public void Update()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            Check();
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

        if (!PV.IsMine)
        {
            localPlayerReady = true;

            PV.RPC("RPC_PlayerReadyCheck", RpcTarget.AllBuffered, true);
        }
        else
        {
            masterPlayerReady = true;
        }
    }

    void Check()
    {
        if (localPlayerReady == true && masterPlayerReady == true && loadedScene == false)
        {
            loadedScene = true;

            //PV.RPC("LoadGameScene", RpcTarget.AllBuffered);
            bool temp = PhotonNetwork.AutomaticallySyncScene;
            PhotonNetwork.LoadLevel(2);
        }
    }


    [PunRPC]
    void RPC_PlayerReadyCheck(bool value)
    {
        localPlayerReady = value;
    }

    //[PunRPC]
    //void LoadGameScene()
    //{
    //    PhotonNetwork.LoadLevel("Game");
    //}


}

