using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputTestScript : MMGameSceneBehaviour
{
    public bool IsActive;

    public float WaitForDashDuration;

    protected override void OnGameStarted()
    {
        base.OnGameStarted();

        StartCoroutine(InputTestProgress());
    }

    IEnumerator InputTestProgress()
    {

        while (true)
        {
            if (IsActive)
            {
                CharacterInputController.Instance.OnAttackPressed();

                yield return new WaitForSeconds(0.06f);

                CharacterInputController.Instance.OnAttackReleased();

                yield return new WaitForSeconds(0.1f);

                CharacterInputController.Instance.OnAttackPressed();

                yield return new WaitForSeconds(0.4f);

                CharacterInputController.Instance.OnChargeReleased();

                CharacterInputController.Instance.OnDashPressed();


                yield return new WaitForSeconds(1);
            }
            else
                yield return null;
        }

    }
}
