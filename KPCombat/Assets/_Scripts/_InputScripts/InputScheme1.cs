using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputScheme1 : InputSchemeBase
{
    protected override void OnFingerDown(int fingerID, Vector2 inputPos)
    {
        CharacterInputController.Instance.OnAttackPressed();
    }

    protected override void OnFingerUpOnStartPos(int fingerID, Vector2 inputPos)
    {
        CharacterInputController.Instance.OnAttackReleased();
    }
}
