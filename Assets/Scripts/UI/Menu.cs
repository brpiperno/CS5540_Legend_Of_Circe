using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{

    private static bool isPaused = false;
    public GameObject pauseMenu;
    public bool keepMouseUnlocked = false;

    public static float volume = 0.5f;
    public static float mouseSensitivity = 1.0f;

    public Slider volumeSlider;
    public TextMeshProUGUI volumeValueText;
    public Slider mouseSensitivitySlider;
    public TextMeshProUGUI mouseSensitivityValueText;

    public CameraSettings cameraSettings;

    private void Awake()
    {
        volumeSlider.onValueChanged.AddListener(UpdateVolume);
        volumeSlider.minValue = 0;
        volumeSlider.value = volume;
        volumeSlider.maxValue = 1;
        volumeValueText.text = string.Format("{0:P1}", volume);

        mouseSensitivitySlider.minValue = 0.2f;
        mouseSensitivitySlider.maxValue = 2.0f;
        mouseSensitivitySlider.value = mouseSensitivity;
        mouseSensitivityValueText.text = mouseSensitivity.ToString("0.0");
        mouseSensitivitySlider.onValueChanged.AddListener(UpdateMouseSensitivity);
    }

    private void Start()
    {
        if (PlayerPrefs.HasKey("MouseSensitivity")) {
            mouseSensitivitySlider.value = PlayerPrefs.GetFloat("MouseSensitivity");
        }
        if (PlayerPrefs.HasKey("Volume")) {
            volumeSlider.value = PlayerPrefs.GetFloat("Volume");
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            TogglePause();
        }
    }

    public static void LoadNextlevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public static void LoadPreviousLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public static void LoadMainMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void TogglePause()
    {
        isPaused = !isPaused;
        Time.timeScale = isPaused ? 0f : 1f;
        pauseMenu.SetActive(isPaused);

        Cursor.visible = keepMouseUnlocked || isPaused; 
        Cursor.lockState = keepMouseUnlocked || isPaused ? CursorLockMode.None : CursorLockMode.Locked;
    }

    public static void Quit()
    {
        Application.Quit();
    }

    public void UpdateMouseSensitivity(float newSensitivity)
    {
        mouseSensitivity = newSensitivity;
        mouseSensitivitySlider.value = newSensitivity;
        mouseSensitivityValueText.text = newSensitivity.ToString("0.0");
        PlayerPrefs.SetFloat("MouseSensitivity", mouseSensitivity);
        if (cameraSettings != null) {
            cameraSettings.UpdateSensitivity(newSensitivity);
        }
    }

    public void UpdateVolume(float newVolume)
    {
        volume = newVolume;
        volumeSlider.value = newVolume;
        volumeValueText.text = string.Format("{0:P1}", volume);
        AudioListener.volume = volume;
        PlayerPrefs.SetFloat("Volume", newVolume);
    }
}
