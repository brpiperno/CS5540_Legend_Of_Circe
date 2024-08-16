using TMPro;
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
    public TextMeshProUGUI battlesLostText;

    public GameObject AboutTheTeamCard;

    private static int battlesLost;

    private void Awake()
    {
        volumeSlider.onValueChanged.AddListener(UpdateVolume);
        volumeSlider.minValue = 0;
        volumeSlider.value = PlayerPrefs.GetFloat("volume", 1.0f);
        volumeSlider.maxValue = 1;
        volumeValueText.text = string.Format("{0:P0}", volume);

        mouseSensitivitySlider.minValue = 0.2f;
        mouseSensitivitySlider.maxValue = 2.0f;
        mouseSensitivity = PlayerPrefs.GetFloat("mouseSensitivity", 1.0f);
        mouseSensitivitySlider.value = mouseSensitivity;
        mouseSensitivityValueText.text = mouseSensitivity.ToString("0.0");
        mouseSensitivitySlider.onValueChanged.AddListener(UpdateMouseSensitivity);

        battlesLost = PlayerPrefs.GetInt("battlesLost", 0);
    }

    private void Start()
    {
        if (cmr == null)
        {
            cmr = Camera.main;
        }
        if (battlesLostText != null)
        {
            battlesLostText.text = "Confrontations\nlost: " + battlesLost.ToString();
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
        SceneManager.LoadScene(1);
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
        PlayerPrefs.SetFloat("mouseSensitivity", newSensitivity);
        //TODO : update thirdpersoncontroller object if there is one
    }

    public void UpdateVolume(float newVolume)
    {
        volume = newVolume;
        volumeSlider.value = newVolume;
        volumeValueText.text = string.Format("{0:P0}", volume);
        AudioListener.volume = volume;
        PlayerPrefs.SetFloat("volume", volume);
    }

    public static void ClearBattlesLost()
    {
        PlayerPrefs.SetInt("battlesLost", 0);
    }

    public static void LostBattle()
    {
        battlesLost++;
        PlayerPrefs.SetInt("battlesLost", battlesLost);
    }
}
