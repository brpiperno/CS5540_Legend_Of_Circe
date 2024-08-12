using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraSettings : MonoBehaviour
{
    CinemachineFreeLook camera;

    // Start is called before the first frame update
    void Start()
    {
        camera = gameObject.GetComponent<CinemachineFreeLook>();
        float playerMouseSensitivity = PlayerPrefs.GetFloat("MouseSensitivity");
        UpdateSensitivity(playerMouseSensitivity);
    }

    public void UpdateSensitivity(float sliderValue) {
        // Converts a value between 0.2 to 2 to a value between 25 to 400, linearly
        camera.m_XAxis.m_MaxSpeed = Mathf.Lerp(25f, 300f, (sliderValue - 0.2f)/1.8f);
        camera.m_YAxis.m_MaxSpeed = Mathf.Lerp(0.25f, 4, (sliderValue - 0.2f)/1.8f);
    }
}
