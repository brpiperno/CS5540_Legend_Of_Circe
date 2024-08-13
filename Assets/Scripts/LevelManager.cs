using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public static int numEnemiesDefeated = 0;
    //BattleManager battleManager;

    public static void StartBattle() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        ToggleMovement(false);
    }

    public static void BattleWon() {

        /*if (GameObject.FindGameObjectsWithTag("BattleManager") > 0) {
            GameObject battleManagerGO = GameObject.FindGameObjectWithTag("BattleManager");
            battleManager = battleManagerGO.GetComponent<BattleManager>();
            battleManager.
        } else {
            throw new ArgumentException("Cannot call BattleWon in a non-battle scene. This error may also happen if there is no" + 
                "object tagged BattleManager in the battle");
        }*/
        numEnemiesDefeated += 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        ToggleMovement(true);
    }

    public static void BattleLost() {
        Debug.Log("Inside LevelManager");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
        ToggleMovement(true);
    }

    private static void ToggleMovement(bool enabled)
    {
        ThirdPersonController[] controllers = GameObject.FindObjectsOfType<ThirdPersonController>();
        foreach (ThirdPersonController controller in controllers)
        {
            controller.enabled = enabled;
        }
    }
}
