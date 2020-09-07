using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    public Camera mainCam;
    public Camera miniCam;
    public Transform player;
    public static CameraManager CM;

    private void OnEnable()
    {
        if (CameraManager.CM == null)
        {
            CameraManager.CM = this;
        }
        else
        {
            if (CameraManager.CM != this)
            {
                Destroy(CameraManager.CM.gameObject);
                CameraManager.CM = this;
            }
        }
    }

    void LateUpdate()
    {
        Vector3 newPosition = player.position;
        newPosition.z = miniCam.transform.position.z;
        mainCam.transform.position = newPosition;
        mainCam.transform.position = new Vector3(
                Mathf.Clamp(mainCam.transform.position.x, -56f, 59f), 
                Mathf.Clamp(mainCam.transform.position.y, -38f, 48f),
                mainCam.transform.position.z
                );
        miniCam.transform.position = newPosition;
    }
}
