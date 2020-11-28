using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CSManager : MonoBehaviour
{
    public CSData data;
    private CharacterSelection CS;

    private PhotonView PV;

    void Awake()
    {
        CS = GameObject.Find("Characters").GetComponent<CharacterSelection>();

        data.localPlayerReady = false;
        data.masterPlayerReady = false;
    }

    private void LateUpdate()
    {
        data.localPlayerReady = CS.localPlayerReady;
        data.masterPlayerReady = CS.masterPlayerReady;
        
    }

    

}
