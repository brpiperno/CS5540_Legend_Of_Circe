using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeCollection : MonoBehaviour
{
    public Transform player;
    public int rangeToCollectIngredient = 1;
    public GameObject treePrompt;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        treePrompt = GameObject.FindGameObjectWithTag("TreePrompt");
    }

    // Update is called once per frame
    void Update()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);
        if (distanceToPlayer < rangeToCollectIngredient) {
            treePrompt.SetActive(true);
        }
    }
}
