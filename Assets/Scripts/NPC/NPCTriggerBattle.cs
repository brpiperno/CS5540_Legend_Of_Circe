using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.UIElements;

[RequireComponent(typeof(Collider))]
public class NPCTriggerBattle : MonoBehaviour
{
    private Collider detectPlayer;
    public string playerTag = "Player";
    private bool playerWithinRange = false;
    public GameObject triggerBattlePanel;
    public string NPCName;

    // Start is called before the first frame update
    void Start()
    {
        detectPlayer = GetComponent<Collider>();
        if (triggerBattlePanel == null)
        {
            Debug.Log("NPCTriggerBattle: Battle Trigger Text was not set for GameObject: " + name);
        }
        triggerBattlePanel.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag(playerTag))
        {
            //Turn on Message and start accepting input
            playerWithinRange = true;
            //triggerBattleText.text = "Press 'Space' to talk with " + NPCName;
            triggerBattlePanel.SetActive(true);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (playerWithinRange && Input.GetButton("Jump"))
        {
            LevelManager.StartBattle();
        }
    }


    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag(playerTag))
        {
            playerWithinRange = false;
            triggerBattlePanel.SetActive(false);
        }
    }
}
