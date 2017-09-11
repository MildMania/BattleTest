using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharPushableIC : PushableICBase
{
    public CharPushedBackStateBC PushedBackStateBC;

    public FSMTransitionBehaviour FSMBehaviour;

    public KnockBackBehaviour KnockBackBehaviour;

    protected override void OnReactionCompleted(Reaction reaction)
    {
        base.OnReactionCompleted(reaction);

        PushInteractionInfo pi = ((PushableBase)_targetReaction).CurPushInfo;

        KnockBackBehaviour.KnockBackDirection = pi.PushDirection;
        KnockBackBehaviour.KnockBackGridCount = pi.PushAmount;
        KnockBackBehaviour.KnockBackGridCountPerSec = pi.PushSpeed;

        PushedBackStateBC.PushBackAnimState = Constants.CHAR_KNOCKBACK_ANIM_STATE;

        FSMBehaviour.DOFSMTransition(FSMStateID.PUSHED_BACK);
    }
}
