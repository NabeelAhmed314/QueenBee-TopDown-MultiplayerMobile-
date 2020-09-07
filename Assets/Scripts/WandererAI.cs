using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WandererAI : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float rotSpeed = 100f;
    public Animator animator;

    private bool isWandring;
    private bool isRotLeft;
    private bool isRotRight;
    private bool isWalking;

    // Start is called before the first frame update
    void Start()
    {
        isWandring = isRotLeft = isRotRight = isWalking = false;
    }

// Update is called once per frame
void Update()
    {
        animator.SetBool("isWalking", isWalking);
        if(isWandring == false)
        {
            StartCoroutine(Wander());
        }
        if (isRotLeft)
        {
            transform.Rotate(new Vector3(0, 0, 1) * Time.deltaTime * -rotSpeed);
        }
        if (isRotRight)
        {
            transform.Rotate(new Vector3(0,0,1) * Time.deltaTime * rotSpeed);
        }
        if(isWalking)
        {
            transform.position += transform.up * moveSpeed * Time.deltaTime;
        }
        
    }

    IEnumerator Wander()
    {
        int rotTime = Random.Range(1,3);
        int rotWait = Random.Range(1, 4);
        int rotLorR = Random.Range(0, 3);
        int walkWait = Random.Range(1, 4);
        int walkTime= Random.Range(1, 5);

        isWandring = true;

        yield return new WaitForSeconds(walkWait);
        isWalking = true;
        yield return new WaitForSeconds(walkTime);
        isWalking = false;
        yield return new WaitForSeconds(rotWait);
        if(rotLorR == 1)
        {
            isRotRight = true;
            yield return new WaitForSeconds(rotTime);
            isRotRight = false;
        }
        if(rotLorR == 2)
        {
            isRotLeft = true;
            yield return new WaitForSeconds(rotTime);
            isRotLeft = false;
        }
        isWandring = false;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Stinger"))
        {
            ScoreManager.Kills++;
            ScoreManager.Enemies--;
            Destroy(this.gameObject);
        }
    }
}
