using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using System.IO;


public enum CharacterAnimEnum : int
{
    None,
    Idle,
    Walk,
    Melee_Attack1,
    Melee_Attack2,
    Melee_Attack3,
    Melee_Charge,
    Knockback,
}

[System.Serializable]
public class CharacterAnimInfo
{
    public CharacterAnimEnum AnimationType;
    public string StateName;
}

public class CharacterAnimController : MMSpriteAnimatorBase
{
    public List<CharacterAnimInfo> AnimInfoList;

    protected override string GetAnimStateName(int animEnum)
    {
        try
        {
            return AnimInfoList.Single(val => (int)val.AnimationType == animEnum).StateName;
        }
        catch
        {
            return "";
        }
    }
}
