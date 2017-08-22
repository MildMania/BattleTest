using System.Collections.Generic;
using System.Linq;

public enum EnemyAnimEnum
{
    None,
    Idle,
    Move,
    Melee_Attack,
    TookDamage_Left,
    TookDamage_Right,
    KnockBack,
}

[System.Serializable]
public class EnemyAnimInfo
{
    public EnemyAnimEnum AnimationType;
    public string StateName;
}

public class EnemyAnimController : MMSpriteAnimatorBase
{
    public List<EnemyAnimInfo> AnimInfoList;


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
