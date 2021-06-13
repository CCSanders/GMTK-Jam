using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class ScreenShake : MonoBehaviour
{
    private CinemachineVirtualCamera vcam;
    private float shakeTimer;

    // Start is called before the first frame update
    void Start()
    {
        vcam = GetComponent<CinemachineVirtualCamera>();
    }

    // Update is called once per frame
    void Update()
    {
        if(shakeTimer > 0)
        {
            shakeTimer -= Time.deltaTime;
            if(shakeTimer <= 0)
            {
                CinemachineBasicMultiChannelPerlin shakeComponent = vcam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
                shakeComponent.m_AmplitudeGain = 0f;
            }
        }
    }

    public void ShakeCamera(float intensity, float time)
    {
        CinemachineBasicMultiChannelPerlin shakeComponent = vcam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();

        shakeComponent.m_AmplitudeGain = intensity;
        shakeTimer = time;
    }
}
