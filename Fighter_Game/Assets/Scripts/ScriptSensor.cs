using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class ScriptSensor : MonoBehaviour
{
    public enum HitMarker { body, hit };
    public HitMarker hitMarkerType;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag != this.tag)
        {
            //Debug.Log(other.gameObject.name);
            //Debug.Log(other.gameObject.tag);
            if (hitMarkerType == HitMarker.body)
            {
                var otherGuy = other.GetComponentInParent<PlayerControl>();
                if (otherGuy.GetIsAttacking())
                {
                    Debug.Log("Get Hit by " + other.name);
                    otherGuy.SetIsAttacking(false);
                    this.GetComponentInParent<PlayerControl>().TakeDamage();
                }
                else if (other.gameObject.name == "BodyCollider")
                {
                    Debug.Log("Get Hit by enery ball");
                    other.GetComponentInParent<PlayerControl>().TakeDamage();
                    Destroy(this.gameObject);
                }
            }

            if (hitMarkerType == HitMarker.hit)
            {
                var thisGuy = this.GetComponentInParent<PlayerControl>();
                if (thisGuy.GetIsAttacking())
                {
                    Debug.Log("Hit other by " + this.name);
                    thisGuy.SetIsAttacking(false);
                    other.GetComponentInParent<PlayerControl>().TakeDamage();
                }
                else if (this.gameObject.name == "BodyCollider")
                {
                    Debug.Log("Get Hit by enery ball");
                    this.GetComponentInParent<PlayerControl>().TakeDamage();
                    Destroy(other.gameObject);

                }
            }
            
        }



        //    if (PhotonNetwork.IsMasterClient)
        //    {
        //        if (other.tag != this.tag)
        //        {
        //            Debug.Log(other.gameObject.name);
        //            Debug.Log(other.gameObject.tag);
        //            if (hitMarkerType == HitMarker.body)
        //            {
        //                var otherGuy = other.GetComponentInParent<PlayerControl>();
        //                if (otherGuy.GetIsAttacking())
        //                {
        //                    Debug.Log("Get Hit by " + other.name);
        //                    otherGuy.SetIsAttacking(false);
        //                    this.GetComponentInParent<PlayerControl>().TakeDamage();
        //                }
        //                else if (other.gameObject.name == "BodyCollider")
        //                {
        //                    Debug.Log("Get Hit by enery ball");
        //                    other.GetComponentInParent<PlayerControl>().TakeDamage();
        //                    Destroy(this.gameObject);
        //                }
        //            }

        //            if (hitMarkerType == HitMarker.hit)
        //            {
        //                var thisGuy = this.GetComponentInParent<PlayerControl>();
        //                if (thisGuy.GetIsAttacking())
        //                {
        //                    Debug.Log("Hit other by " + this.name);
        //                    thisGuy.SetIsAttacking(false);
        //                    other.GetComponentInParent<PlayerControl>().TakeDamage();
        //                }
        //                else if (this.gameObject.name == "BodyCollider")
        //                {
        //                    Debug.Log("Get Hit by enery ball");
        //                    this.GetComponentInParent<PlayerControl>().TakeDamage();
        //                    Destroy(other.gameObject);

        //                }
        //            }
        //        }
        //    }


    }




}
