using Photon.Pun;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class Movement : MonoBehaviourPun
{
    [SerializeField] private float movementSpeed = 0.0f;

    private CharacterController controller = null;

    private void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        //if (photonView.IsMine)
        //{
        //    TakeInput();
        //}
        TakeInput();

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
}
