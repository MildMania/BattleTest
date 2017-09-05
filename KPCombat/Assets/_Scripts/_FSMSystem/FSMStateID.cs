using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum FSMStateID
{
    NULL_STATE,
    IDLE,
    STAND,
    MOVE,
    MELEE_CHARGE,
    MELEE_ATTACK,
    TOOK_DAMAGE,
    KNOCK_BACK,
    RECOVER,
    PUSHED_BACK,
    DEAD,
    SUPER_STRIKE,
}