using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CameraControl : MonoBehaviour
{
    private Camera mainCamera;
    private Transform playerATrans;
    private Vector3 playerAPos = new Vector3();

    private Transform playerBTrans;
    private Vector3 playerBPos = new Vector3();

    private Vector3 cameraMinZ = new Vector3(0f, 0f, -4.67f);
    private Vector3 cameraMaxZ = new Vector3(0f, 0f, -5.7f);
    private Vector3 cameraZpos = new Vector3();

    float val;
    float diff;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    void LateUpdate()
    {
        UpdateCameraPos();
        
    }

    public void SetPlayerAPos(Transform pos)
    {
       playerATrans = pos;
        playerAPos = playerATrans.position;
    }
    public void SetPlayerBPos(Transform pos)
    {
        playerBTrans = pos;
        playerBPos = playerBTrans.position;
    }

    public void UpdateCameraPos()
    {
        var xpos = playerAPos.x + playerBPos.x;
        diff = Mathf.Abs(playerAPos.x - playerBPos.x);
        //Debug.Log(diff); // 10.5
        if (diff > 8f)
        {
            cameraZpos = cameraMaxZ;
        }
        else if (diff < 5f)
        {
            cameraZpos = cameraMinZ;
        }
        else
        {
            val = (diff / 3f);
            cameraZpos = new Vector3(0f, 0f, -3 - val);
        }

        transform.position = new Vector3(xpos * 0.5f, 1.5f, cameraZpos.z);

       
    }
}
