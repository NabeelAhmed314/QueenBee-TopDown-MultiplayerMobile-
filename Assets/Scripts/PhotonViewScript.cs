using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class PhotonViewScript : MonoBehaviour
{
    public PhotonView PV;
    // Start is called before the first frame update
    void Start()
    {
        if (!PV.IsMine)
        {
            transform.GetComponent<PlayerMovementTopDownMouse>().enabled = false;
            transform.GetComponent<Shooting>().enabled = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
