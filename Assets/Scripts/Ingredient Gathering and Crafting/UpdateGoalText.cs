using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpdateGoalText : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        int numEnemiesLeft = 3 - LevelManager.numEnemiesDefeated;
        GetComponent<Text>().text = numEnemiesLeft.ToString() + " people left to encounter";
    }
}
