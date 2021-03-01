using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionDetector : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        GameObject hit = other.gameObject;

        //if (PhotonNetwork.IsMasterClient)
        //{
        //    if (hit.CompareTag("DamageBox"))
        //    {
        //        Debug.Log(this.name + " hit " + hit.name);
        //        PlayerControl otherGuy = hit.gameObject.GetComponentInParent<PlayerControl>();
        //        //if (otherGuy.CanTakeDamage())
        //        //{
        //        //    otherGuy.TakeDamage();
        //        //}
        //    }
        //    else
        //    {
        //        PlayerControl me = GetComponentInParent<PlayerControl>();
        //        Debug.Log(this.name + " hit " + hit.name);

        //        //if (me.CanTakeDamage())
        //        //{
        //        //    me.TakeDamage();
        //        //}
        //    }
        //}

        if (hit.CompareTag("DamageBox"))
        {
            Debug.Log(this.name + " hit " + hit.name);
            PlayerControl otherGuy = hit.gameObject.GetComponentInParent<PlayerControl>();
            //if (otherGuy.CanTakeDamage())
            //{
            //    otherGuy.TakeDamage();
            //}
        }
        else
        {
            PlayerControl me = GetComponentInParent<PlayerControl>();
            Debug.Log(this.name + " hit " + hit.name);

            //if (me.CanTakeDamage())
            //{
            //    me.TakeDamage();
            //}
        }
    }
}
