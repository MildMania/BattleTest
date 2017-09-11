using System.Collections.Generic;

public static class BattleCalculator
{
    public static void CalculateBattleResult(ref BattleResult br)
    {
        object message = br;

        CheckIfReflected(ref br);

        if (br.IsDamageReflected)
            return;

        CalculateBaseDamage(ref br);

        if (br.DamageAmount > 0)
            br.IsDamageGiven = true;

        CheckIfAbsorbed(ref br);
    }

	static void CalculateBaseDamage(ref BattleResult br)
	{
		br.DamageAmount += br.Attacker.BaseDamage;
	}

    static void CheckIfAbsorbed(ref BattleResult br)
    {
        if (br.Damagable.AbsorbedAttackTypeList.Contains(br.Attacker.AttackType))
            br.IsDamageGiven = false;
    }

    static void CheckIfReflected(ref BattleResult br)
    {
        if (br.Damagable.ReflectedAttackTypeList.Contains(br.Attacker.AttackType))
            br.IsDamageReflected = true;
    }
}
