using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerControl : MonoBehaviourPun
{
    public int comboNum;
    public float reset;
    public float resetTime;
    public bool isAttacking;
    public bool localPlayerAttack;


    private Animator anim;
    private float movementSpeed;
    private ProjectileController energyBall;
    private Health hp;

    private List<string> animlist = new List<string>(new string[] { "Punch1", "Punch2", "Punch3" });


    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        energyBall = GetComponent<ProjectileController>();
        hp = GetComponent<Health>();
        isAttacking = false;
        comboNum = 0;
        reset = 0f;
        resetTime = 0f;
        movementSpeed = 1.0f;
        localPlayerAttack = false;

    }

// Update is called once per frame
void Update()
    {
        //if (photonView.IsMine)
        //{
        //    TakeInput();

        //    if (Input.GetKeyDown(KeyCode.B))
        //    {
        //        TakeDamage();
        //    }

        //}

        TakeInput();

    }

    void TakeInput()
    {
        //// Combo attack
        //if (Input.GetKeyDown(KeyCode.J) && comboNum < 3)
        //{
        //    anim.SetTrigger(animlist[comboNum]);
        //    comboNum++;
        //    reset = 0f;
        //    isAttacking = true;
        //}
        //if (comboNum > 0)
        //{
        //    reset += Time.deltaTime;
        //    if (reset > resetTime)
        //    {
        //        anim.SetTrigger("Reset");
        //        comboNum = 0;
        //    }
        //    if (comboNum == 3)
        //    {
        //        resetTime = 1f;
        //        comboNum = 0;
        //    }
        //    else
        //    {
        //        resetTime = 0.8f;
        //    }

        //}

        // Kicking
        if (Input.GetKeyDown(KeyCode.G))
        {
            anim.SetTrigger("Punch1");
            isAttacking = true;
        }
        if (Input.GetKeyDown(KeyCode.H))
        {
            anim.SetTrigger("Punch2");
            isAttacking = true;
        }
        if (Input.GetKeyDown(KeyCode.J))
        {
            anim.SetTrigger("Punch3");
            isAttacking = true;
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
            isAttacking = true;
        }

        // Energy Ball Attack
        if (Input.GetKeyDown(KeyCode.U))
        {
            anim.SetTrigger("Energy Ball Attack");
            StartCoroutine(energyBall.ShootEnergyBall());
        }

    }
    
    public bool GetIsAttacking()
    {
        return isAttacking;
    }
    public void SetIsAttacking(bool val)
    {
        isAttacking = val;
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

    [PunRPC]
    void RPC_PlayerAttackCheck(bool value)
    {
        localPlayerAttack = value;
    }
}
