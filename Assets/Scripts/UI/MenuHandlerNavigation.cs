using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuHandlerNavigation : MonoBehaviour
{
    Transform mainMenuCanvas;

    // Start is called before the first frame update
    void Start()
    {
        mainMenuCanvas = GameObject.FindGameObjectWithTag("MainMenuCanvas").transform;
        Debug.Log("MainMenuCanvas is active: " + mainMenuCanvas.gameObject.activeSelf);
        Debug.Log("Number of children in Canvas is " + mainMenuCanvas.childCount);
        for(int i = 0; i < mainMenuCanvas.childCount; i++){
            Transform child = mainMenuCanvas.GetChild(i);
            child.gameObject.SetActive(false);
            Debug.Log("Setting " + child.gameObject.name + " to inactive");
        }
        Debug.Log("MainMenuCanvas is active: " + mainMenuCanvas.gameObject.activeSelf);
    }

    void DisplaySettingsMenu() {
        mainMenuCanvas.GetChild(3).gameObject.SetActive(true);
        Time.timeScale = 0f;
    }

    void HideSettingsMenu() {
        mainMenuCanvas.GetChild(3).gameObject.SetActive(false);
        Time.timeScale = 1f;
    }
}
