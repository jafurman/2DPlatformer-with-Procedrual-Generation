using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class ChangeCameraView : MonoBehaviour
{
    public CinemachineVirtualCamera newCamera;
    public CinemachineVirtualCamera defaultCamera;

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            newCamera.Priority = 10;
            defaultCamera.Priority = 0;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            newCamera.Priority = 0;
            defaultCamera.Priority = 10;
        }

    }
}
