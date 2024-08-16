using System.Collections;
using System.Collections.Generic;
using TMPro;
//using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{

    private static bool isPaused = false;
    public GameObject pauseMenu;
    public bool keepMouseUnlocked = false;

    public static float volume;
    public static float mouseSensitivity;

    public Slider volumeSlider;
    public TextMeshProUGUI volumeValueText;
    public Slider mouseSensitivitySlider;
    public TextMeshProUGUI mouseSensitivityValueText;
    public Camera cmr;
    public TextMeshProUGUI enemiesDefeatedCount;

    public CameraSettings cameraSettings;
    private static int enemiesDefeated;

    public GameObject AboutTheTeamCard;

    private void Awake()
    {
        volumeSlider.onValueChanged.AddListener(UpdateVolume);
        volumeSlider.minValue = 0;
        volumeSlider.value = PlayerPrefs.GetFloat("volume", 1.0f);
        volumeSlider.maxValue = 1;
        volumeValueText.text = string.Format("{0:P1}", volume);

        mouseSensitivitySlider.minValue = 0.2f;
        mouseSensitivitySlider.maxValue = 2.0f;
        mouseSensitivity = PlayerPrefs.GetFloat("mouseSensitivity", 1.0f);
        mouseSensitivitySlider.value = mouseSensitivity;
        mouseSensitivityValueText.text = mouseSensitivity.ToString("0.0");
        mouseSensitivitySlider.onValueChanged.AddListener(UpdateMouseSensitivity);
        enemiesDefeated = PlayerPrefs.GetInt("enemiesDefeated", 0);
    }

    private void Start()
    {
        if (PlayerPrefs.HasKey("MouseSensitivity")) {
            mouseSensitivitySlider.value = PlayerPrefs.GetFloat("MouseSensitivity");
        }
        if (cmr == null)
        {
            cmr = Camera.main;
        }
        if (PlayerPrefs.HasKey("Volume")) {
            volumeSlider.value = PlayerPrefs.GetFloat("Volume");
        }
        if (enemiesDefeatedCount != null)
        {
            enemiesDefeatedCount.text = "Enemies Defeated: " + enemiesDefeated.ToString();
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

    public void ToggleTeamMenuCard()
    {
        AboutTheTeamCard.SetActive(!AboutTheTeamCard.activeSelf);
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
        //TODO : update thirdpersoncontroller object if there is one
    }

    public void UpdateVolume(float newVolume)
    {
        volume = newVolume;
        volumeSlider.value = newVolume;
        volumeValueText.text = string.Format("{0:P0}", volume);
        AudioListener.volume = volume;
        PlayerPrefs.SetFloat("Volume", volume);
    }

    public static void EnemyDefeated()
    {
        enemiesDefeated++;
        PlayerPrefs.SetInt("enemiesDefeated", enemiesDefeated);
    }

    public static void clearGameProgress()
    {
        enemiesDefeated = 0;
        PlayerPrefs.SetInt("enemiesDefeated", enemiesDefeated);
    }

}
