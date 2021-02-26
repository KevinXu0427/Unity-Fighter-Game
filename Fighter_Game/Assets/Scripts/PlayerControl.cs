using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerControl : MonoBehaviourPun
{
    private Animator anim;
    private float movementSpeed = 1.0f;

    private List<string> animlist = new List<string>(new string[] { "Punch1", "Punch2", "Punch3" });
    public int comboNum = 0;
    public float reset= 0f;
    public float resetTime= 0f;

    private ProjectileController energyBall;
    private Health hp;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        energyBall = GetComponent<ProjectileController>();
        hp = GetComponent<Health>();
    }

    // Update is called once per frame
    void Update()
    {
        if (photonView.IsMine)
        {
            TakeInput();

            if (Input.GetKeyDown(KeyCode.B))
            {
                TakeDamage();
            }

        }

        //TakeInput();

    }

    void TakeInput()
    {
        if (Input.GetKeyDown(KeyCode.J) && comboNum < 3)
        {
            anim.SetTrigger(animlist[comboNum]);
            comboNum++;
            reset = 0f;
        }
        if (comboNum > 0)
        {
            reset += Time.deltaTime;
            if (reset > resetTime)
            {
                anim.SetTrigger("Reset");
                comboNum = 0;
            }
            if (comboNum == 3)
            {
                resetTime = 1f;
                comboNum = 0;
            }
            else
            {
                resetTime = 0.8f;
            }

        }
        
        // Move Left/Right
        Vector3 moveDir = Vector3.zero;
        moveDir.x = Input.GetAxis("Horizontal");
        transform.position += moveDir * movementSpeed * Time.deltaTime;
        if (gameObject.tag == "Master")
        {
            transform.rotation = Quaternion.Euler(0, 130, 0);
            
            // Right
            if (Input.GetAxis("Horizontal") > 0)
            {
                anim.SetBool("isWalkingForward", true);
            }
            else
            {
                anim.SetBool("isWalkingForward", false);
            }

            // Left
            if (Input.GetAxis("Horizontal") < 0)
            {
                anim.SetBool("isWalkingBackward", true);
            }
            else
            {
                anim.SetBool("isWalkingBackward", false);
            }
        }
        else if (gameObject.tag == "Local")
        {
            transform.rotation = Quaternion.Euler(0, -60, 0);


            // Left
            if (Input.GetAxis("Horizontal") < 0)
            {
                anim.SetBool("isWalkingForward", true);
            }
            else
            {
                anim.SetBool("isWalkingForward", false);
            }

            // Right
            if (Input.GetAxis("Horizontal") > 0)
            {
                anim.SetBool("isWalkingBackward", true);
            }
            else
            {
                anim.SetBool("isWalkingBackward", false);
            }
        }

        // Jumping 
        if (Input.GetButtonDown("Jump"))
        {
            anim.SetTrigger("isJumping");
        }

        // Kicking
        if (Input.GetKeyDown(KeyCode.K))
        {
            anim.SetTrigger("Kick");
        }

        // Energy Ball Attack
        if (Input.GetKeyDown(KeyCode.U))
        {
            anim.SetTrigger("Energy Ball Attack");
            StartCoroutine(energyBall.ShootEnergyBall());
        }
    }

    public void TakeDamage()
    {
        photonView.RPC("RPC_TakeDamage", RpcTarget.All);
    }

    [PunRPC]
    void RPC_TakeDamage()
    {
        hp.TakeDamage(10);
    }

}
