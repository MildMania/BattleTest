using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleResult
{
    public AttackerBase Attacker;
    public DamagableBase Damagable;

    public bool IsDamageGiven { get; set; }

    public float DamageAmount { get; set; }

    public BattleResult(AttackerBase attacker, DamagableBase damagable)
    {
        Attacker = attacker;
        Damagable = damagable;
    }
}
