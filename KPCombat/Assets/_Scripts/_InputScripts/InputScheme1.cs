using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputScheme1 : InputSchemeBase
{
    protected override void OnFingerDown(int fingerID, Vector2 inputPos)
    {
        CharacterInputController.Instance.OnAttackPressed();
    }

    protected override void OnFingerUpNotOnStartPos(int fingerID, Vector2 inputPos)
    {
        Debug.Log("finger up on not start pos");

        CharacterInputController.Instance.OnChargeReleased();
    }

    protected override void OnFingerUpOnStartPos(int fingerID, Vector2 inputPos)
    {
        CharacterInputController.Instance.OnAttackReleased();
    }

    /*protected override void OnLeftSwipe()
    {
        CharacterInputController.Instance.OnJumpLeftPressed();
    }

    protected override void OnRightSwipe()
    {
        CharacterInputController.Instance.OnRightJumpPressed();
    }*/
}
