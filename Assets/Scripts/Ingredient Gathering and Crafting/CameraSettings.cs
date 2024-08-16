using UnityEngine;
using Cinemachine;

public class CameraSettings : MonoBehaviour
{
    CinemachineFreeLook camera0;
    private static float mouseSensitivity;

    // Start is called before the first frame update
    void Start()
    {
        camera0 = gameObject.GetComponent<CinemachineFreeLook>();
        UpdateSensitivity();
    }

    public void UpdateSensitivity() {
        mouseSensitivity = Menu.mouseSensitivity;
        // Converts a value between 0.2 to 2 to a value between 25 to 400, linearly
        camera0.m_XAxis.m_MaxSpeed = Mathf.Lerp(25f, 300f, (mouseSensitivity - 0.2f)/1.8f);
        camera0.m_YAxis.m_MaxSpeed = Mathf.Lerp(0.25f, 4, (mouseSensitivity - 0.2f)/1.8f);
    }
}