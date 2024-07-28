using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum MoveType
{
    Shield, //increases the target's defense modifiers up one stage
    Transformation, //sets the target's next move to be a damaging move
    Enhancement, //shifts the target's emotion value up
    Paralysis, //causes the target to skip their next turn
    Pharmaka, //causes the user to win the game
    Damage, //shifts the targets emotion values down
    Null //movetype created only for targets of Paralysis.
}
