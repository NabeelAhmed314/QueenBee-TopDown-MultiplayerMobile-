using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stringer : MonoBehaviour
{
    public GameObject hitEffect;
    private void Start()
    {
        Destroy(gameObject, 5f);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        GameObject effect = Instantiate(hitEffect, transform.position, Quaternion.identity);
        Destroy(effect, effect.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).length + 0f);
        Destroy(gameObject);
    }
}
