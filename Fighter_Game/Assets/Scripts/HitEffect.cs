using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitEffect : MonoBehaviour
{
    public GameObject projectile;
    public GameObject[] Spwaners;

    public void PlayHitEffect()
    {
        GameObject hitEffect = Instantiate(projectile, Spwaners[Random.Range(0, Spwaners.Length)].transform.position, Quaternion.identity);
        Destroy(hitEffect, 1f);

    }
}
