using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class bossFightCamera : MonoBehaviour
{
    public CinemachineVirtualCamera bossFightC;
    public CinemachineVirtualCamera defaultCamera;

    
    public void StartBossFightCamera()
    {
        bossFightC.Priority = 10;
        defaultCamera.Priority = 0;   
    }

    public void BackToPlayerCamera()
    {
        bossFightC.Priority = 0;
        defaultCamera.Priority = 10;
    }
}
