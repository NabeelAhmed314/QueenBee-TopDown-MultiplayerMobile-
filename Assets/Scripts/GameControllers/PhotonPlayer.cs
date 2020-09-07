using System.Collections;
using System.Collections.Generic;
using System.IO;
using Photon.Pun;
using UnityEngine;

public class PhotonPlayer : MonoBehaviour
{
    private PhotonView PV;
    public GameObject myAvatar;

    // Start is called before the first frame update
    void Start()
    {
        PV = GetComponent<PhotonView>();
        int spawnPicker = Random.Range(0, GameSetup.GS.spawnPoints.Length);
        if (PV.IsMine)
        {
            myAvatar = PhotonNetwork.Instantiate("Player", 
                GameSetup.GS.spawnPoints[spawnPicker].position, GameSetup.GS.spawnPoints[spawnPicker].rotation, 0);
            myAvatar.GetComponent<PlayerMovementTopDownMouse>().joystick = FindObjectsOfType<Joystick>()[0];
            myAvatar.GetComponent<PlayerMovementTopDownMouse>().rotateButton = FindObjectsOfType<FixedButton1>()[0];
            myAvatar.GetComponent<PlayerMovementTopDownMouse>().cam = FindObjectsOfType<Camera>()[0];
            myAvatar.GetComponent<Shooting>().firebtn = FindObjectsOfType<FixedButton1>()[1];
            CameraManager.CM.player = myAvatar.transform;
        }
    }
}
