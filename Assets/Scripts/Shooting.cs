using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    public Transform firePoint;
    public GameObject stringerPrefab;
    public float stringerForce = 20f;
    public FixedButton1 firebtn;
    public float fireRate = 15f;
    public float nextTimeToFire = 0f;

    // Update is called once per frame
    void Update()
    {
        if (firebtn.Pressed && nextTimeToFire <= Time.time)
        {
            nextTimeToFire = Time.time + 1 / fireRate;
            Shoot();
        }    
    }
    void Shoot()
    {
        GameObject stringer =  Instantiate(stringerPrefab, firePoint.position, firePoint.rotation); 
        Rigidbody2D rb =  stringer.GetComponent<Rigidbody2D>();
        rb.AddForce(firePoint.up * stringerForce, ForceMode2D.Impulse);
    }
}
