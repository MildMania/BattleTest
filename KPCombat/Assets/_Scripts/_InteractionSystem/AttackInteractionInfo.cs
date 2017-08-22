
public class AttackInteractionInfo : InteractionInfo
{
    public AttackerBase Attacker { get; private set; }

    public DirectionEnum AttackDirection { get; private set; }
    public AttackTypeEnum AttackType { get; private set; }

	public bool IsFirstDamage { get; private set; }

	public bool IsDamageGiven { get; private set; }
	public bool IsCritHit { get; private set; }
	public bool IsAttackReflected { get; private set; }
	public bool IsAttackAbsorbed { get; private set; }

	public float DamageAmount { get; private set; }

	public float KnockBackGridAmout { get; set; }
    public float KnockBackSpeed { get; set; }

	public AttackInteractionInfo(BattleResult br)
    {
        Attacker = br.Attacker;

        AttackDirection = br.Attacker.AttackDirection;
        AttackType = br.Attacker.AttackType;

        IsDamageGiven = br.IsDamageGiven;
        DamageAmount = br.DamageAmount;

        KnockBackSpeed = 10.0f;
    }

}