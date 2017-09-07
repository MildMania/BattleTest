using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleResult
{
    public AttackerBase Attacker { get; private set; }
    public DamagableBase Damagable { get; private set; }

    public bool IsDamageGiven { get; set; }
    public bool IsDamageReflected { get; set; }

    public float DamageAmount { get; set; }

    public BattleResult(AttackerBase attacker, DamagableBase damagable)
    {
        Attacker = attacker;
        Damagable = damagable;
    }
}
