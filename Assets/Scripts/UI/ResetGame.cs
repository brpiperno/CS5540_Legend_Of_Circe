using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetGame : MonoBehaviour
{

    private void Awake()
    {
        Menu.ClearBattlesLost();
        Menu.LoadMainMenu();
    }
}
