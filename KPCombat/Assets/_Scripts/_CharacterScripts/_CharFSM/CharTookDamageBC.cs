using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharTookDamageBC : TookDamageStateBC
{
    public FlashBehaviour FlashBehaviour;

    public override void Execute()
    {
        FlashBehaviour.Flash();

        base.Execute();
    }
}
