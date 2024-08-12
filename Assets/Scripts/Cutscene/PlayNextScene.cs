using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayNextScene : MonoBehaviour
{
    public string nextScene;
    public GameObject skipIntroPrompt;
    bool prompted = false;

    // Start is called before the first frame update
    IEnumerator Start()
    {
        yield return StartCoroutine("NextScene");
    }

    void Update() {
        if (Input.GetKeyDown(KeyCode.Space)) {
            if (!prompted) {
                skipIntroPrompt.SetActive(true);
                prompted = true;
            } else {
                skipIntroPrompt.transform.GetChild(0).gameObject.GetComponent<Text>().text = "Loading";
                SceneManager.LoadScene("BenIngredientGathering");
            }
        }
    }

    IEnumerator NextScene() {
        yield return new WaitForSeconds(41);
        SceneManager.LoadScene("BenIngredientGathering");
    }
}
