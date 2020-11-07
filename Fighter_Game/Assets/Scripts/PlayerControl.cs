using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    static Animator anim;
    private float movementSpeed = 1.0f;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
        // Move Left/Right
        Vector3 moveDir = Vector3.zero;
        moveDir.x = Input.GetAxis("Horizontal");
        transform.position += moveDir * movementSpeed * Time.deltaTime;

        // Left
        if (Input.GetAxis("Horizontal") > 0)
        {
            anim.SetBool("isWalkingForward", true);
        }
        else
        {
            anim.SetBool("isWalkingForward", false);
        }

        // Right
        if (Input.GetAxis("Horizontal") < 0)
        {
            anim.SetBool("isWalkingBackward", true);
        }
        else
        {
            anim.SetBool("isWalkingBackward", false);
        }

        // Jumping 
        if (Input.GetButtonDown("Jump"))
        {
            anim.SetTrigger("isJumping");
        }

    }
}
