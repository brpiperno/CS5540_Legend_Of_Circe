using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFaint : MonoBehaviour
{
    public float fallSpeed = 1f;

    void Start() {
    }

    // Update is called once per frame
    void Update()
    {
        if (BattleManager.opponentIsDying) {
            transform.Rotate(new Vector3(-90, 0, 0) * Time.deltaTime * fallSpeed);
            if (transform.eulerAngles.x < 270) {
                BattleManager.opponentIsDying = false;
            }
        }
    }
}
