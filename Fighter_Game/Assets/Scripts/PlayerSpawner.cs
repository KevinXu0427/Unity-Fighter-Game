using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpawner : MonoBehaviour
{
    public GameObject[] characterPrefabs;
    public Transform[] spawnPoints;


    void Start()
    {
        int selectedCharacter = PlayerPrefs.GetInt("SelectedCharacter");
        GameObject prefab = characterPrefabs[selectedCharacter];

        PhotonNetwork.Instantiate(prefab.name,Vector3.zero, Quaternion.identity, 0);

        //if (PhotonNetwork.IsMasterClient)
        //{
        //    PhotonNetwork.Instantiate(prefab.name, spawnPoints[0].position, Quaternion.identity, 0);
        //}
        //else
        //{
        //    PhotonNetwork.Instantiate(prefab.name, spawnPoints[1].position, Quaternion.identity, 0);
        //}
    }
    
}
