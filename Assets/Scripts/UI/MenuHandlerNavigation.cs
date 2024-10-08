using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuHandlerNavigation : MonoBehaviour
{
    //Transform mainMenuCanvas;

    // Start is called before the first frame update
    /*void Start()
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
    }*/

    public void DisplaySettingsMenu() {
        transform.Find("SettingsMenu").gameObject.SetActive(true);
        Time.timeScale = 0f;
    }

    public void HideSettingsMenu() {
        transform.Find("SettingsMenu").gameObject.SetActive(false);
        Time.timeScale = 1f;
    }

    public void Quit() {
        Menu.Quit();
    }
}
