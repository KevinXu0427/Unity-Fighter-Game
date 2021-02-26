using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionDetector : MonoBehaviour
{
    //private GameObject owner;

    //private void OnTriggerEnter(Collider other)
    //{
    //    owner = this.gameObject;

    //    if (owner.tag == "Master" && other.tag == "Local")
    //    {
    //        Debug.Log(owner.name + " hit " + other.name);
    //    }
    //    else if (owner.tag == "Local" && other.tag == "Master")
    //    {
    //        Debug.Log(owner.name + " hit " + other.name);

    //    }

    //}

    private void OnTriggerEnter(Collider other)
    {
        GameObject hit = other.gameObject;

        if (PhotonNetwork.IsMasterClient)
        {
            if (hit.CompareTag("DamageBox"))
            {
                Debug.Log(this.name + " hit " + hit.name);
                PlayerControl otherGuy = hit.gameObject.GetComponentInParent<PlayerControl>();
                if (otherGuy.CanTakeDamage())
                {
                    otherGuy.TakeDamage();
                }
            }
            else
            {
                PlayerControl me = GetComponentInParent<PlayerControl>();
                if (me.CanTakeDamage())
                {
                    me.TakeDamage();
                }
            }
        }
    }
}
