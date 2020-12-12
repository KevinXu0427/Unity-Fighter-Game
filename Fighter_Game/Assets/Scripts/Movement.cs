using Photon.Pun;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(CharacterController))]
public class Movement : MonoBehaviourPun
{
    [SerializeField] private float movementSpeed = 0.0f;

    private CharacterController controller = null;
    private Health hp;

    private void Start()
    {
        controller = GetComponent<CharacterController>();
        hp = GetComponent<Health>();
    }

    void Update()
    {
        if (photonView.IsMine)
        {
            TakeInput();
            if (Input.GetKeyDown(KeyCode.B))
            {
                photonView.RPC("RPC_TakeDamage", RpcTarget.All);
            }
        }

    }

    private void TakeInput()
    {
        // only move to left or right
        Vector3 movement = new Vector3
        {
            x = Input.GetAxisRaw("Horizontal"),
            y = 0f,
            z = 0f,
        }.normalized;

        controller.SimpleMove(movement * movementSpeed);

    }

    [PunRPC]
    void RPC_TakeDamage()
    {
        hp.TakeDamage(10);
    }

}
