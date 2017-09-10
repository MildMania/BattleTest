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
    MELEE_CHARGE_EXIT,

    SHIELD_UP,
    SHIELDED_STANCE,
    SHIELDED_PUSHED_BACK,
    SHIELDED_MELEE_ATTACK,
    SHIELDED_TOOK_DAMAGE,
    SHIELDED_MOVE,
    SHIELDED_KNOCK_BACK,

    DASH,
    DASH_EXIT,
    
    SHIELD_DOWN,
}