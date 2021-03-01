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
       
        if (Spwaner.tag == "Master")
        {
            GameObject energyBall = Instantiate(projectile, Spwaner.transform.position, Quaternion.identity);
            energyBall.tag = "Master";
            yield return new WaitForSeconds(0.5f);

            energyBall.GetComponentInParent<Rigidbody>().velocity = energyBall.transform.right * moveSpeed;
            Destroy(energyBall, 3f);

        }
        else
        {
            GameObject energyBall = Instantiate(projectile, Spwaner.transform.position, Quaternion.identity);
            energyBall.transform.Rotate(0.0f, 180.0f, 0.0f);
            energyBall.tag = "Local";
            yield return new WaitForSeconds(0.7f);

            energyBall.GetComponentInParent<Rigidbody>().velocity = energyBall.transform.right * moveSpeed;
            Destroy(energyBall, 3f);

        }
    }
}
