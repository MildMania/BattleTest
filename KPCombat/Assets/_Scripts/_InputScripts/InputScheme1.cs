using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputScheme1 : InputSchemeBase
{
    protected override void OnFingerDown(int fingerID, Vector2 inputPos)
    {
        CharacterInputController.Instance.OnChargePressed();
    }

    protected override void OnFingerUpNotOnStartPos(int fingerID, Vector2 inputPos)
    {
        CharacterInputController.Instance.OnChargeReleased();
        CharacterInputController.Instance.OnShieldDownPressed();
    }

    protected override void OnFingerUpOnStartPos(int fingerID, Vector2 inputPos)
    {
        CharacterInputController.Instance.OnAttackReleased();
    }

    protected override void OnUpSwipe()
    {
        CharacterInputController.Instance.OnDashPressed();
    }

    protected override void OnDownSwipe()
    {
        CharacterInputController.Instance.OnShieldUpPressed();
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
