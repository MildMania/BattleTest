using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputTestScript : MonoBehaviour
{
    private void Update()
    {
        CharacterInputController.Instance.OnDashPressed();
    }
}
