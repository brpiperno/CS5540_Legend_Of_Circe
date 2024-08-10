using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayNextScene : MonoBehaviour
{
    public string nextScene;
    // Start is called before the first frame update
    IEnumerator Start()
    {
        yield return StartCoroutine("NextScene");
    }

    IEnumerator NextScene() {
        yield return new WaitForSeconds(41);
        SceneManager.LoadScene("BenIngredientGathering");
    }
}
