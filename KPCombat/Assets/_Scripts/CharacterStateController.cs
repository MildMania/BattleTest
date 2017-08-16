using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CharStateEnum
{
    None,
    Running,
    Attacking,
}

public class CharacterStateController : MonoBehaviour
{
    public CharacterMovement MovementController;

    public CharStateEnum CurState { get; private set; }

    private void Awake()
    {
        
    }

    private void OnDestroy()
    {
        
    }

    void StartListeningEvents()
    {
        CharacterInputController.OnInput += OnInput;
    }

    void StopListeningEvents()
    {
        CharacterInputController.OnInput -= OnInput;
    }

    void OnInput(InputEnum inputType)
    {
        if (inputType == InputEnum.Attack
            && CurState == CharStateEnum.Running)
            Attack();
    }

    public void Move()
    {
        MovementController.Move();
    }

    public void Attack()
    {

    }


}
