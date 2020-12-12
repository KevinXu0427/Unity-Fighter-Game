using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpawner : MonoBehaviour
{
    public GameObject[] characterPrefabs;
    public Transform[] spawnPoints;
    private int selectedCharacter = 0;

    private PhotonView PV;
    void Start()
    {
        PV = GetComponent<PhotonView>();
        //selectedCharacter = PlayerPrefs.GetInt("SelectedCharacter");

        //if (PV.IsMine)
        //{
        //    PV.RPC("RPC_Instantiate", RpcTarget.AllBuffered, selectedCharacter, 0);
        //}
        ////else
        ////{
        ////    PV.RPC("RPC_Instantiate", RpcTarget.AllBuffered, selectedCharacter + 2, 1);
        ////}


        int selectedCharacter = PlayerPrefs.GetInt("SelectedCharacter");

        if (PhotonNetwork.IsMasterClient)
        {
            GameObject prefab = characterPrefabs[selectedCharacter];

            PhotonNetwork.Instantiate(prefab.name, spawnPoints[0].position, Quaternion.identity, 0);
            //PV.RPC("RPC_Instantiate", RpcTarget.AllBuffered, 0, 0);

        }
        else
        {
            GameObject prefab = characterPrefabs[selectedCharacter+2];

            PhotonNetwork.Instantiate(prefab.name, spawnPoints[1].position, Quaternion.identity, 0);
            //PV.RPC("RPC_Instantiate", RpcTarget.AllBuffered, 1, 1);

        }

    }

    [PunRPC]
    void RPC_Instantiate(int character, int spawnPoint)
    {
        GameObject prefab = characterPrefabs[character];
        PhotonNetwork.Instantiate(prefab.name, spawnPoints[spawnPoint].position, Quaternion.identity, 0);
    }
    
}
