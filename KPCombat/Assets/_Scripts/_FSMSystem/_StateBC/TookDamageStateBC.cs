using UnityEngine;

public class TookDamageStateBC : FSMBehaviourController
{
    public FSMTransitionBehaviour FSMTransitionBehaviour;

    public KnockBackBehaviour KnockBackBehaviour;

    public AttackInteractionInfo AttackInteractionInfo { get; set; }

    protected override void InitFSMBC()
    {
        StateID = FSMStateID.TOOK_DAMAGE;
    }

    public override void Execute()
    {
        KnockBackBehaviour.KnockBackGridCount = AttackInteractionInfo.KnockBackGridAmout;
        FSMTransitionBehaviour.DOFSMTransition(FSMStateID.KNOCK_BACK);

        FireOnExecutionCompleted();
    }
}
