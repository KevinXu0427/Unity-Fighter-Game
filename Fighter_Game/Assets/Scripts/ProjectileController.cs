using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileController : MonoBehaviour
{
    private float moveSpeed = 4.0f;
    public GameObject projectile;
    public GameObject Spwaner;
    
    private void Update()
    {

    }

    public IEnumerator ShootEnergyBall()
    {
        GameObject energyBall = Instantiate(projectile, Spwaner.transform.position, Quaternion.identity);
        yield return new WaitForSeconds(0.5f);
        energyBall.GetComponentInParent<Rigidbody>().velocity = energyBall.transform.right * moveSpeed;
        Destroy(energyBall, 3f);
    }
}
